using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    bool useRB;

    Rigidbody rb;
    CharacterController charController;
    Vector3 movementInput;

    float fakeGravity = -10f;

    void Start()
    {
        if (useRB)
        {
            rb = GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.isKinematic = false;
        }
        else
        {
            charController = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movementInput = new Vector3(moveX, 0f, moveZ).normalized * Player.Instance.moveSpeed;
    }

    void FixedUpdate()
    {
        if(useRB) MoveRBPlayer();
        else charController.Move(movementInput * Time.fixedDeltaTime);
    }

    void MoveRBPlayer()
    {
        Vector3 newPosition = rb.position + movementInput * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
        ApplyFakeGravity();
    }

    void ApplyFakeGravity()
    {
        rb.velocity = new Vector3(rb.velocity.x, fakeGravity, rb.velocity.z);
    }
}
