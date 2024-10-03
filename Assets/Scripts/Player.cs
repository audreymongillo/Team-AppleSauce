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
        // Temporarily disable CharacterController to allow manual position change
        character.enabled = false;

        // Set the starting position using ViewportToWorldPoint to ensure camel is on screen
        Vector3 startPositionViewport = new Vector3(0.1f, 0.5f, Camera.main.nearClipPlane); // 10% from the left, centered vertically
        Vector3 startPositionWorld = Camera.main.ViewportToWorldPoint(startPositionViewport);
        
        // Apply the calculated world position to the camel
        transform.position = new Vector3(startPositionWorld.x, GameManager.Instance.camelStartPosition.y, GameManager.Instance.camelStartPosition.z);
        Debug.Log("Player position reset to: " + transform.position);

        // Reset the vertical velocity and jump force
        direction = Vector3.zero;
        currentJumpForce = defaultJumpForce;

        // Re-enable the CharacterController
        character.enabled = true;

        // Ensure the player is active
        gameObject.SetActive(true);
    }


}
