using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    [Header("Jump Settings")]
    [SerializeField, Min(2)] private float jumpHeight = 2f;
    [SerializeField, Min(0.2f)] private float timeToJumpApex = 0.4f;

    private float gravity;
    private float initalJumpVelocity;

    CharacterController cc;

    private Vector2 moveInput = Vector2.zero;
    private Vector3 velocity;
    private bool jumpPressed = false;

    void CalculateJumpVariables()
    {
        try
        {
           if (timeToJumpApex <= 0)
           {
                throw new ArgumentException("Time to jump apex must be greater than zero.");
           }
           
           if (jumpHeight <= 0)
           {
                throw new ArgumentException("Jump height must be greater than zero.");
           }
        }
        catch (ArgumentException ex)
        {
            Debug.LogException(ex);
            return;
        }

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        initalJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void OnValidate()
    {
        CalculateJumpVariables();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CalculateJumpVariables();
        cc = GetComponent<CharacterController>();

        InputManager.instance.OnMoveEvent += (vector) => moveInput = vector;
        InputManager.instance.OnJumpEvent += (pressed) => jumpPressed = pressed;
    }

    void FixedUpdate()
    {
        UpdateCharacteVelocity();

        cc.Move(velocity * Time.fixedDeltaTime);
    }

    private void UpdateCharacteVelocity()
    {
        velocity.x = moveInput.x * speed;
        velocity.z = moveInput.y * speed;

        if (cc.isGrounded)
        {
            velocity.y = cc.skinWidth;
            if (jumpPressed)
            {
                velocity.y = initalJumpVelocity;
            }
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with Enemy!");
            // Handle collision with enemy (e.g., reduce health, play sound, etc.)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player picked up an item!");
            // Handle pickup logic (e.g., increase score, play sound, etc.)
            Destroy(other.gameObject); // Remove the pickup from the scene
        }
    }
}
