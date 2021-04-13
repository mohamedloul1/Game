using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerCotroller playerCotrollerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerCotrollerScript = GameObject.Find("Player").GetComponent<PlayerCotroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCotrollerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
