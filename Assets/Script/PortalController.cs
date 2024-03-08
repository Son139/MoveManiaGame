﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Animation anim;
    Rigidbody2D playerRb;

    MovementController movementController;

    AudioManager audioManager;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
        movementController = player.GetComponent<MovementController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        audioManager.PlaySFX(audioManager.portalIn);
        playerRb.simulated = false;
        anim.Play("Portal In");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        movementController.UpdateShouldFlip(false);
        player.transform.position = destination.position;
        anim.Play("Portal Out");
        audioManager.PlaySFX(audioManager.portalOut);
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
