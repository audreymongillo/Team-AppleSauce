using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = 9.81f * 2f;
    public float defaultJumpForce = 8f;
    public float slowedJumpForce = 10f; // Adjust as needed
    private float currentJumpForce;
    private CharacterController character;
    private Vector3 direction;
    public AudioSource jumpSound; 




    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
        currentJumpForce = defaultJumpForce; // Initialize with default value
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * currentJumpForce;

                if (jumpSound != null)
                {
                    jumpSound.Play();
                    

                }
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        { 
            GameManager.Instance.GameOver();
	    
        }
        else if (other.CompareTag("Powerup"))
        {
            GameManager.Instance.SlowDown();
        }
    }

    // Call this method from GameManager to adjust jump force
    public void SetJumpForce(float jumpForce)
    {
        currentJumpForce = jumpForce;
    }
}
