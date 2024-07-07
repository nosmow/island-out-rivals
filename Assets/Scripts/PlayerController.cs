using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private AudioSource playerAudio;
    public AudioClip poweripSound;

    public float speed = 5f;
    private float movX, movZ;

    public bool hasPowerup = false;
    public float forcePowerup = 30f;
    public GameObject powerupIndicator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleMovement(); 
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0.58f, 0);

        if (transform.position.y < -1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void HandleMovement()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        if ((movX != 0 || movZ != 0))
        {
            Vector3 mov = new Vector3(movX * speed, 0, movZ * speed);

            rb.AddForce(mov);

            Quaternion rotation = Quaternion.LookRotation(mov, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed);

            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            playerAudio.PlayOneShot(poweripSound, 0.4f);
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowertpCountdownRoutine());
        }
    }

    IEnumerator PowertpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
