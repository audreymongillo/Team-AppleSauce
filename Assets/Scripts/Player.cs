using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = 9.81f * 2f;
    public float defaultJumpForce = 8f;
    public float slowedJumpForce = 10f;
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
        currentJumpForce = defaultJumpForce;
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

    public void SetJumpForce(float jumpForce)
    {
        currentJumpForce = jumpForce;
    }

    public void ResetPlayer()
    {
        direction = Vector3.zero;
        currentJumpForce = defaultJumpForce;
        gameObject.SetActive(true); // Make sure the player is active
        // Reset the player's position if necessary
        transform.position = new Vector3(0, 1, 0); // Adjust the spawn position as needed
    }
}
