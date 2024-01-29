using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SnowManController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 3f;
    public GameObject winTextObject;

    public GameObject gameOverTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool gameOver;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        winTextObject.SetActive(false);
        gameOverTextObject.SetActive(false );
    }

    private void Update()
    {

        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Mathf.Abs(rb.velocity.y) < 0.001f)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            replay();
        }
    }

    void OnMove(InputValue movementValue)
    {
        if (!gameOver)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }

    }

    void replay()
    {
        SceneManager.LoadScene("cross");
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);

            rb.AddForce(movement * speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        
            if (transform.position.y < 0)
            {
                  gameOver = true;
                 gameOverTextObject.SetActive(true);
            }

        }
    }

    void OnTriggerEnter(Collider other)
     {
     if (other.gameObject.CompareTag("End"))
         {
             winTextObject.SetActive(true);
         }
     }
}
