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
    public bool hasCollided = false;
    private float triggerCountdown;
    public float triggerTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        triggerCountdown = triggerTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyStats.isDead)
        {
            if(!hasCollided)
            {
                WanderAround();
                SearchForTarget();
            }
           else
            {
                if (triggerCountdown <= 0)
                {
                    transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
                    hasCollided = false;
                    triggerCountdown = triggerTimer;
                }
                triggerCountdown -= Time.deltaTime;
            }

        }
    }

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



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            hasCollided = true;
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

        pushVector = target.transform.position - transform.position;
        pushVector.y = 0;

        target.GetComponent<KnockbackScript>().AddImpactForce(pushVector, knockback);
    }

    
}
