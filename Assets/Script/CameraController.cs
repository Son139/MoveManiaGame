using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Animator camAnimator;

    private Transform player;
    private List<Transform> targets = new List<Transform>();
    private int currentTargetIndex = 0;
    private Vector3 velocity = Vector3.zero;

    private float distanceThreshold = 0.1f;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;

    private float moveDelay = 1f; 
    private float returnDelay = 1f; 

    [Header("Axis Limitation")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject itemObject in itemObjects)
        {
            targets.Add(itemObject.transform);
            Debug.Log(itemObject.transform.position);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (currentTargetIndex < targets.Count)
        {
            if (moveDelay > 0)
            {
                moveDelay -= Time.deltaTime;
            }
            else
            {
                MoveToTarget(targets[currentTargetIndex]);
            }
        }
        else
        {
            if (returnDelay > 0)
            {
                returnDelay -= Time.deltaTime;
            }
            else
            {
                ReturnToPlayer();
            }
        }
    }

    private void MoveToTarget(Transform target)
    {
        Vector3 newPos = new Vector3(Mathf.Clamp(target.position.x, xLimit.x, xLimit.y), target.position.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
        float distanceY = Mathf.Abs(transform.position.y - newPos.y);
        if (distanceY <= distanceThreshold)
        {
            currentTargetIndex++;
            moveDelay = 1f; // Đặt lại thời gian chờ
        }
    }

    private void ReturnToPlayer()
    {
        Vector3 playerPosition = player.position + positionOffset;
        playerPosition = new Vector3(Mathf.Clamp(playerPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(playerPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
        camAnimator.SetInteger("CamTargetItemEnd", 1);
    }
}
