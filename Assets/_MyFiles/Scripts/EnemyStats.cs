using UnityEngine;
using CharacterSpace;

public class EnemyStats : MonoBehaviour
{
    Transform enemyTransform;
    public float currentHp;
    public float maxHp;
    public float currentMana;
    public float maxMana;

    public float hpRegenAmount;
    public float hpRegenTimer;
    public float manaRegenAmount;
    public float manaRegenTimer;
    public float hpRegenModifier;
    public float manaRegenModifier;

    public bool isDead = false;
    public bool isMultiplayer = false;

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

    //Shaders
    public Shader shader1;
    public Shader shader2;
    public Renderer rend;

    private void Awake()
    {
        currentHp = maxHp;
        currentMana = maxMana;

        rend = GetComponent<Renderer>();
        shader1 = Shader.Find("Universal Render Pipeline/Lit");
        shader2 = Shader.Find("Universal Render Pipeline/Simple Lit");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = transform;
        hpRegenAmount = (maxHp * hpRegenModifier) / 100;
        manaRegenAmount = (maxMana * manaRegenModifier) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (isMultiplayer)
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

        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
            Die();
            //Player will die
        }
        if (currentHp < maxHp)
        {
            currentHp += (hpRegenAmount * Time.deltaTime);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        //AdjustingEnemyHealth();
        //AdjustingEnemyMana();
    }

    void WanderAround()
    {
        if (wanderTime > 0)
        {
            if(true)
            {

            }
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            wanderTime -= Time.deltaTime;
        }
        else
        {
            wanderTime = Random.Range(5.0f, 15.0f);
            Wander();
        }
    }

    void Wander()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    //This function is for when the game requires an enemy to select multiple targets
    void SearchForTarget()
    {
        if(!isMultiplayer)
        {
            float distanceToPlayer = Vector3.Distance(target.transform.position, this.transform.position);
            if(distanceToPlayer < detectionDistance)
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
            if(attackCooldownTime > 0)
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
        int randomNum = Random.Range(1, 5);

        //special Attack
        if (randomNum == 3)
        {
            //int critDmgNum = Random.Range(attackDamageMin, attackDamageMax) * 2;
            
            //For double hit
            target.transform.GetComponent<PlayerStats>().ReceiveDamage(Random.Range(attackDamageMin * 2, attackDamageMax * 2));
            target.transform.GetComponent<PlayerStats>().ReceiveDamage(Random.Range(attackDamageMin * 2, attackDamageMax * 2));
        }
        else
        {
            target.transform.GetComponent<PlayerStats>().ReceiveDamage(Random.Range(attackDamageMin, attackDamageMax));
        }
    }

    public void AdjustingEnemyHealth()
    {
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
            Die();
            //Player will die
        }
        if (currentHp < maxHp)
        {
            currentHp += (hpRegenAmount * Time.deltaTime);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void AdjustingEnemyMana()
    {
        if(currentMana < 0)
        {
            currentMana = 0;
        }
        if(currentMana < maxMana)
        {
            currentMana += manaRegenAmount * Time.deltaTime;
        }
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }

    }

    public void ReceiveDamage(float dmg)
    {
        if(currentHp > 0)
        {
            currentHp -= dmg;
            Debug.Log("Did " + dmg + " damage");
            Debug.Log("Enemy current has " + currentHp + " health remaining");
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.rotation = Quaternion.Euler(0f, 180, 0f);
            //movementSpeed *= -1;
        }
    }

    //Can have a bool as an arguement to determine how to select and deselect instead of two separate functions
    public void Selected()
    {
        rend.material.shader = shader2;
    }

    public void Deselected()
    {
        rend.material.shader = shader1;
    }
}
