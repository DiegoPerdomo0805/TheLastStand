using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed = 5f;
    public float Speed2 = 7.5f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensure the player does not rotate unexpectedly
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Determine speed based on whether the space key is pressed
        float currentSpeed = Input.GetKey(KeyCode.Space) ? Speed2 : Speed;

        // Apply movement
        rb.velocity = movement * currentSpeed;

        // If no input, set velocity to zero for immediate stop
        if (movement == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
