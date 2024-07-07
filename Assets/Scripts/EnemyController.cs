using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody rb;
    private Animator anim;
    private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        anim.SetBool("isRun", true);
    }

    private void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        lookDirection.y = 0;
        
        if (rb != null)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed);

            rb.AddForce(lookDirection * speed);
        }
        
        if (transform.position.y < -0.5)
        {
            GameManager.FindFirstObjectByType<GameManager>().UpdateScore();
            Destroy(gameObject);
        }
    }
}
