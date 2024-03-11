using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Animation anim;
    Rigidbody2D playerRb;

    MovementController movementController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
        movementController = player.GetComponent<MovementController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
                //player.transform.position = destination.position;
            }
        }
    }

    IEnumerator PortalIn()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.portalIn);
        playerRb.simulated = false;
        anim.Play("Portal In");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        movementController.UpdateShouldFlip(false);
        player.transform.position = destination.position;
        anim.Play("Portal Out");
        AudioManager.instance.PlaySFX(AudioManager.instance.portalOut);
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
        yield return new WaitForSeconds(0.5f);
        movementController.UpdateShouldFlip(true);
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
