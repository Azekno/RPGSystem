using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed = 30.0f;
    private float rotateSpeed = 150.0f;

    public bool grounded;
    private Vector3 moveDirection = Vector3.zero;

    public GameObject camera1;
    public CharacterController controller;
    public bool isJumping;
    private float myAng = 0.0f;
    public bool canJump = true;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera1.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }

        if (myAng > 50)
        {

            canJump = false;
        }
        else
        {

            canJump = true;
        }

        if (grounded)
        {

            isJumping = false;

            if (camera1.transform.gameObject.transform.GetComponent<UserCamera>().inFirstPerson == true)
            {
                moveDirection = new Vector3((Input.GetMouseButton(0) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));
            }
            moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }

        }


        // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.

        if (camera1.transform.gameObject.transform.GetComponent<UserCamera>().inFirstPerson == false)
        {
            if (Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

            }
        }
        else
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }

        }
    }
}