using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip bouncySound;
    float slideForceMultiplier = 10f;
    private Rigidbody rb;
    public PlayerController playerController;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRb != null)
            {
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                pushDirection.y = 0;


                if (playerController.hasPowerup)
                {
                    playerAudio.PlayOneShot(bouncySound, 1f);
                    StartCoroutine(SlideEnemy(enemyRb, pushDirection, 2f, playerController.forcePowerup));
                }
                else
                {
                    playerAudio.PlayOneShot(bouncySound, 1f);
                    StartCoroutine(SlideEnemy(enemyRb, pushDirection, 0.8f, 30f));
                    StartCoroutine(SlideEnemy(rb, -pushDirection, 0.8f, 30f));
                }

                
            }
        }
    }

    private IEnumerator SlideEnemy(Rigidbody enemyRb, Vector3 direction, float slideDuration, float force)
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            if (enemyRb != null)
            {
                enemyRb.AddForce(direction * force * slideForceMultiplier * Time.deltaTime, ForceMode.VelocityChange);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
    }
}
