using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D playerRb;
    MovementController movementController;
    public ParticleController particleController;

    private bool isRespawning = false;
    private void Start()
    {
        checkPointPos = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MovementController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !isRespawning)
        {
            Die();
        }
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    void Die()
    {
        particleController.PlayParticle(ParticleController.Particles.die, transform.position);
        StartCoroutine(Respawn(0.5f));
        HeathManager.instance.LoseLife();
    }

    IEnumerator Respawn(float duration)
    {
        isRespawning = true;
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
        transform.rotation = Quaternion.identity;
        movementController.UpdateRelativeTransform();
        isRespawning = false;
    }
}
