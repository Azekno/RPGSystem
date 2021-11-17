using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;

    public float speed = 6f;
    public float jumpVelocity = 10;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 moveInput = new Vector3();
    public Vector3 velocity;

    public bool isGrounded = true;
    public bool jumpInput;

    public CharacterController controller;

    public float gravity = -9.81f;

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");

    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.z).normalized;


        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = direction.x;
            velocity.z = direction.z;
        }
        //check for jumping
        if (jumpInput && isGrounded)
        {
            velocity.y = jumpVelocity;
        }


        //check if we've hit ground from falling. If so, remove our velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        velocity += Physics.gravity * Time.fixedDeltaTime;
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
        controller.Move(velocity * Time.fixedDeltaTime);
        isGrounded = controller.isGrounded;
    }
}