﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;
//using static UnityEngine.ParticleSystem;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D playerRb;

    float counter;
    bool isOnGround;

    [Header("")]
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;

    private void Start()
    {
        touchParticle.transform.parent = null;
        dieParticle.transform.parent = null;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayParticle(Particles particle, Vector2 pos = default(Vector2))
    {
        switch (particle)
        {
            case Particles.touch:
                AudioManager.instance.PlaySFX(AudioManager.instance.wallTouch);
                touchParticle.transform.position = pos;
                touchParticle.Play();
                break;

            case Particles.fall:
                AudioManager.instance.PlaySFX(AudioManager.instance.wallTouch);
                fallParticle.Play();
                break;

            case Particles.die:
                AudioManager.instance.PlaySFX(AudioManager.instance.death);
                dieParticle.transform.position = pos;
                dieParticle.Play();
                isOnGround = false;
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    public enum Particles
    {
        touch,
        fall,
        die
    }
}
