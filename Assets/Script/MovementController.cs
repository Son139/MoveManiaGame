using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] int speed;
    float speedMultiplier;

    [Range(1, 10)]
    [SerializeField] float acceleration;

    public static bool btnPressed;

    bool isWallTouch;
    bool isFalling;
    private bool shouldFlip = true; // Biến cờ để điều khiển flip
    public float flipDelay = 0.5f; // Thời gian chờ trước khi kiểm tra va chạm và flip

    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public ParticleController particleCtrl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateRelativeTransform();
        particleCtrl = FindObjectOfType<ParticleController>();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        //isPrevWallTouch = currentWallTouch;
        // Kiểm tra xem nhân vật có đang rơi từ trên xuống không
        //isFalling = rb.velocity.y < 0;

        // Kiểm tra va chạm với tường
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.6f), 0, wallLayer);
        // Nếu nhân vật đang rơi và va chạm với tường và không nên flip, thì mới thực hiện Flip
        if (shouldFlip && isWallTouch)
        {
            Flip();
        }
    }

    public void Flip()
    {
        particleCtrl.PlayParticle(ParticleController.Particles.touch, wallCheckPoint.position);
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    public void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext value)
    {
        if (CameraController.cameraTargetPlayer)
        {
            if (value.started)
            {
                btnPressed = true;
                speedMultiplier = 1f;
            }
            else if (value.canceled)
            {
                btnPressed = false;
                speedMultiplier = 0;
            }
        }
    }

    void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime * acceleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime * acceleration;
            if (speedMultiplier < 0) speedMultiplier = 0;
        }
    }

    // Hàm để cập nhật biến cờ shouldFlip khi đi vào hoặc ra khỏi portal
    public void UpdateShouldFlip(bool value)
    {
        shouldFlip = value;
    }
}
