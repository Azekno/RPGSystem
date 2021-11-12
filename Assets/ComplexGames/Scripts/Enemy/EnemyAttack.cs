using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSpace;

public class EnemyAttack : MonoBehaviour
{
    EnemyStats enemyStats;
    PlayerStats playerStats;

    public bool inCombat;
    public float wanderTime;
    public float movementSpeed;

    public GameObject target;
    public float detectionDistance;
    public float attackRange;

    //Attack
    public int attackDamageMin;
    public int attackDamageMax;
    public float attackCooldownTimeMain;
    public float attackCooldownTime;
    
    public float knockback;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyStats.isDead)
        {
            if (enemyStats.isMultiplayer)
            {
                if (target == null)
                {
                    SearchForTarget();

                    WanderAround();
                }
                else
                {
                    FollowTarget();
                }
            }
            else
            {
                WanderAround();
                SearchForTarget();
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AttackTarget();
        }
    }*/

    /// <summary>
    /// The enemy will move around in a direction for a set amount of time before determining a new wander direction;
    /// </summary>
    void WanderAround()
    {
        if (wanderTime > 0)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            wanderTime -= Time.deltaTime;
        }
        else
        {
            wanderTime = Random.Range(5.0f, 15.0f);
            Wander();
        }
    }

    /// <summary>
    /// Helps the enemy to 'randomly' walk and change direction
    /// </summary>
    void Wander()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    ///This function can be used when the game requires an enemy to select multiple targets but otherwise it will can the distance of the enemy to the player before 'deciding' if it will chase the player.
    void SearchForTarget()
    {
        if (!enemyStats.isMultiplayer)
        {
            float distanceToPlayer = Vector3.Distance(target.transform.position, this.transform.position);
            if (distanceToPlayer < detectionDistance)
            {
                FollowTarget();
            }
        }
        else
        {
            Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Collider[] hitColliders = Physics.OverlapSphere(center, 100);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].transform.CompareTag("Player"))
                {
                    target = hitColliders[i].transform.gameObject;
                }
            }
        }
    }

    void FollowTarget()
    {
        //Face towards the target
        Vector3 targetPosition = target.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
        float distanceToPlayer = Vector3.Distance(target.transform.position, this.transform.position);

        if (distanceToPlayer > attackRange)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            if (attackCooldownTime > 0)
            {
                attackCooldownTime -= Time.deltaTime;
            }
            else
            {
                attackCooldownTime = attackCooldownTimeMain;
                AttackTarget();
            }
        }
    }

    public void AttackTarget()
    {
        Vector3 pushVector;
        playerStats = target.GetComponent<PlayerStats>();

        playerStats.ReceiveDamage(Random.Range(attackDamageMin, attackDamageMax));

        //pushVector = playerStats.gameObject.transform.position - transform.position;
        pushVector = target.transform.position - transform.position;
        pushVector.y = 0;
        //pushVector = Vector3.Normalize(pushVector);

        //playerStats.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(pushVector * knockForce, transform.position, ForceMode.Impulse);
        target.GetComponent<KnockbackScript>().AddImpactForce(pushVector, knockback);
    }

    
}
