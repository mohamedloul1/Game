using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public bool isAboveBound = false;

    public float floatForce;
    public int points;

    private float gravityModifier = 1.5f;
    private float upperBound = 15f;
    private float countdown = 3f;
    private float restartTime = 0f;

    private Vector3 startPos;

    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        startPos = transform.position;

        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && !isAboveBound)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y > upperBound)
        {
            isAboveBound = true;
            transform.position = new Vector3(transform.position.x, upperBound, transform.position.z);
        }
        else
            isAboveBound = false;

        if (gameOver)
        {
            restartTime += Time.deltaTime;
            if (restartTime >= countdown)
                RestartPlayer();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over! Total score: " + points);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over! Total score: " + points);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            points++;
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Debug.Log("Score: " + points);
            Destroy(other.gameObject);
        }

    }

    private void RestartPlayer()
    {
        points = 0;
        restartTime = 0f;
        transform.position = startPos;
        gameOver = false;
    }

}
