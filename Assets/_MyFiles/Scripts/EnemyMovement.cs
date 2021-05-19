using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float latestDirectionChangeTime;
    //private readonly float directionChangeTime = 3f;
    //public float directionChangeTime = 3f;
    public float characterMoveSpeed = 2f;
    private Vector3 movementDirection;
    private Vector3 movementPerSecond;
    public float directionChangeTimer = 0;
    public float timeLimit = 3f;

    public bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        //directionChangeTimer = timeLimit;
        latestDirectionChangeTime = 0f;
        CalculateNewMovementVector();
    }

    // Update is called once per frame
    void Update()
    {
        directionChangeTimer += Time.deltaTime;
        //If the cvhangETime was reached, calculate a new movement vector
        /*if(Time.time  - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            CalculateNewMovementVector();
        }*/

        if (directionChangeTimer >= timeLimit)
        {
            directionChangeTimer = 0;
            CalculateNewMovementVector();
        }

        //Move enemy
        if(!isColliding)
        {
            //transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y, transform.position.z + (movementPerSecond.z * Time.deltaTime)) ? isColliding : !isColliding;
            transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y, transform.position.z + (movementPerSecond.z * Time.deltaTime));
        }
    }

    void CalculateNewMovementVector()
    {
        //Create a random direction vector with a magnitude(preferrably 1) chosen by the user and later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1, 1), transform.position.y, Random.Range(-1, 1)).normalized;
        movementPerSecond = movementDirection * characterMoveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            isColliding = true;
            directionChangeTimer = 0;
            CalculateNewMovementVector();
            Vector3.Reflect()
            collision.contacts[0].normal
            //movementDirection = -movementDirection;
        }
    }
}
