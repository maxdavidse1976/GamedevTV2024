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
        if (Player.Instance.IsDead()) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movementInput = new Vector3(moveX, 0f, moveZ).normalized;
        ApplyFakeGravity();
        Player_Animator.Instance.PlayMovementAnimations(new Vector2(moveX,moveZ));
    }

    void FixedUpdate()
    {
        if (Player.Instance.IsDead()) return;

        if (useRB)
        {
            MoveRBPlayer();
        }
        else
        {
            MoveCCPlayer();
        }
    }

    void MoveCCPlayer()
    {
        charController.Move(movementInput * Player.Instance.moveSpeed * Time.fixedDeltaTime);
    }

    void MoveRBPlayer()
    {
        Vector3 newPosition = rb.position + movementInput * Player.Instance.moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }

    void ApplyFakeGravity()
    {
        if(rb)
        rb.velocity = new Vector3(rb.velocity.x, fakeGravity, rb.velocity.z);
        if (charController)
        {
            movementInput.y = fakeGravity * Time.deltaTime;
        }
    }
}
