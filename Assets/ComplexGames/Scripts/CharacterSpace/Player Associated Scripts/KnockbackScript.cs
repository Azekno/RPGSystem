using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Since I'm using a character controller, this means I shouldn't be using a rigidbody alongside it,
/// and this requires a script like this one in order to apply a knockback force to the player when hit by the enemy.
/// </summary>
public class KnockbackScript : MonoBehaviour
{
    public float playerMass = 1.0f; //defines the mass of the player
    Vector3 impactForce = Vector3.zero;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //apply the impact force
        if (impactForce.magnitude > 0.2f)
        {
            characterController.Move(impactForce * Time.deltaTime);
        }
        //comsumes the impact force each cycle
        impactForce = Vector3.Lerp(impactForce, Vector3.zero, 5 * Time.deltaTime);
    }

    //Call this function to add an impact force
    public void AddImpactForce(Vector3 direction, float knockbackForce)
    {
        direction.Normalize();
        //reflect down force on the ground
        if(direction.y < 0)
        {
            direction.y = -direction.y;
        }
        impactForce += direction.normalized * knockbackForce / playerMass;
    }
}
