using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed for moving forward/backward
    private Vector3 velocity;  // Store player velocity (for gravity)

    //player move
    public bool noMove = false;
    private Vector3 moveDirection;    // Direction of movement

    [SerializeField] private Animator anme;

    //cal speed
    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();  // Get the CharacterController attached to the player
        lastPosition = transform.position;
    }

    void Update()
    {
        if (noMove)
        {

        }
        else
        {
            // Reset movement direction each frame
            moveDirection = Vector3.zero;

            // Move forward along the Z-axis when pressing W
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += Vector3.forward;    // Z+ direction
            }

            // Move left along the X-axis when pressing A
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection += Vector3.left;       // X- direction
            }

            // Move backward along the Z-axis when pressing S
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection += Vector3.back;       // Z- direction
            }

            // Move right along the X-axis when pressing D
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += Vector3.right;      // X+ direction
            }

            // Normalize the movement direction to ensure consistent speed in diagonal movement
            moveDirection = moveDirection.normalized;

            // Move the player in the calculated direction
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotate the player to face the direction of movement when moving backward (S)
            if (Input.GetKey(KeyCode.S))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.right);
            }

            Vector3 currentPosition = transform.position;
            currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;
            float speedCharacter = currentVelocity.magnitude;
            lastPosition = currentPosition;

            anme.SetFloat("Speed", speedCharacter);
        }
    }
}