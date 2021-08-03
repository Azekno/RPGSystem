using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    public class PlayerStats : MonoBehaviour
    {
        public GameManager statManager;

        public ParticleSystem particle;
        public bool particleSystemActive;

        [Header("Main Stats")]
        public string playerName;
        public int level = 1;
        public int exp = 0;

        public float baseHealth = 100;
        public float currentHealth;
        
        [HideInInspector]
        public float maxHealth;

        public float baseMana = 100;
        public float currentMana;
        
        [HideInInspector]
        public float maxMana;

        public float currentAttackPower;
        public float currentAttackSpeed;
        public float currentMagicAttackPower;

        public float currentPhysicalDefense;
        public float currentMagicDefense;


        [Header("Base Settings")]
        public int baseStatValue = 10;
        public float spellCastTimer;
        public int nextLevelXP;
        public float expExponent = 1;
        public float baseExpValue = 100;
        public int unassignedStatPoints = 0;
        public int unassignedSkillPoints = 0;
        public int unassignedSpellPoints = 0;

        public bool statIncreased = false;

        [Header("Modifiers")]
        //The health level modifier is the amount of health that increases the players health when they level
        public int healthLevelModifier = 10;
        //The mana level modifier is just how much mana is increased upon level up.
        public int manaLevelModifier = 10;
        //The amount of attributes gained when the player levels
        public int levelUpStatPointsModifier = 2;
        //Skill points gained upon level
        public int levelSkillPointModifier = 1;
        //Spell points gained upon level
        public int spellPointsLevelModifier = 1;
        
        public int statIncreaseModifier;

        [Header("Regen Details")]
        public float hpRegenTimer;
        public float hpRegenAmount;
        public float manaRegenTimer;
        public float manaRegenAmount;
        public float percentOfHp;
        public float percentOfMana;

        [Header("Background Settings")]
        public bool isDead;

        public GameObject selectedUnit;
        public EnemyStats enemyStatsScript;

        public bool behindEnemy = false;
        public bool canAttack = false;
        public bool canCast = false;

        public bool didDoubleClick = false;
        public float doubleClickTimer;

        /// <summary>
        /// Currently a hack until a proper spell manager is created
        /// </summary>
        public GameObject rangedSpellPrefab;


        //Creates a dictionary for the player that will assign a value to a particular stat.
        [Header("Player Stats")]
        public Dictionary<Stat, int> statDictionary = new Dictionary<Stat, int>();

        public Dictionary<Stat, int> tempDictionary = new Dictionary<Stat, int>();

        //Creates a list to the resistances
        [Header("Player Resistances")]
        public List<Stat> resistance = new List<Stat>();

        //[Header("Player Skills Enabled")]
        //public List<Skills> playerSkills = new List<Skills>();

        [Header("Player Spells Enabled")]
        public List<Spell> playerSpells = new List<Spell>();

        /// <summary>
        /// Takes in the current level of the player, the baseExp and the expExponent. 
        /// The current level is multiplied by the expExponenet and then multiplied by the baseExpValue to which the next Levels exp value will increase by.
        /// </summary>
        /// <param name="currentLevel"></param>
        private void ExpToNextLevel(int currentLevel)
        {
            nextLevelXP = (int)(baseExpValue * (currentLevel * expExponent));
        }

        /// <summary>
        /// Increase the level, makes the exp the left over value of the current exp taken away by the next levels XP. The leftover value is the, 
        /// 'remainder' and is needed in the case of exp being great enough for multiple levels.
        /// Unassigned attributes is increased based on the attributesIncreaseValue. e.g. Eacvh level will increase a players unassigned attributes by 5.
        /// Same thing for the unassigned skill points
        /// </summary>
        void LevelUp()
        {
            level++;
            exp -= nextLevelXP;
            unassignedStatPoints += levelUpStatPointsModifier;
            unassignedSkillPoints += levelSkillPointModifier;
            unassignedSpellPoints += spellPointsLevelModifier;

            maxHealth += healthLevelModifier;
            maxMana += manaLevelModifier;
            currentHealth = maxHealth;
            currentMana = maxMana;

            particle.Play();

        }

        private void Start()
        {
            particle.Stop();
            maxHealth = baseHealth;
            maxMana = baseMana;

            foreach (Stat thisStat in statManager.stats)
            {
                statDictionary.Add(thisStat, baseStatValue);
            }

            foreach (Stat thisStat in statManager.stats)
            {
                tempDictionary.Add(thisStat, baseStatValue);
            }

            foreach (var item in statDictionary.Keys)
            {
                item.Initialize(this.gameObject);
            }

            currentHealth = maxHealth;
            currentMana = maxMana;
        }


        public int GetStatValue(Stat target)
        {
            return statDictionary[target];
        }

        void Update()
        {
            ExpToNextLevel(level);
            ///For updating the stats when one is increased.
            if (statIncreased)
            {
                statDictionary = tempDictionary;
                foreach (var item in statDictionary.Keys)
                {
                    item.Initialize(this.gameObject);
                }
                statIncreased = false;
            }

            if (currentHealth <= 0)
            {
                //Player will die
                currentHealth = 0;
            }
            if (currentHealth < maxHealth)
            {
                percentOfHp = currentHealth / maxHealth;
            }
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentMana <= 0)
            {
                currentMana = 0;
            }
            if (currentMana < maxHealth)
            {
                percentOfMana = currentMana / maxMana;
                currentMana += manaRegenAmount * Time.deltaTime;
            }
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }

            if (exp >= nextLevelXP)
            {
                LevelUp();
            }

            if (Input.GetMouseButtonDown(0))
            {
                SelectTarget();
            }

            if (selectedUnit != null)
            {
                Vector3 toTarget = (selectedUnit.transform.position - transform.position).normalized;
                //Check if the player is behind Enemy (Calculate dodge, parry, extra damage, etc.)
                //Good for critical strikes/ backstabs
                if (Vector3.Dot(toTarget, selectedUnit.transform.forward) < 0)
                {
                    behindEnemy = false;
                }
                else
                {
                    behindEnemy = true;
                }

                //Calculate if the player is facing the enemy and is within the attack distance.
                float distance = Vector3.Distance(this.transform.position, selectedUnit.transform.position);
                Vector3 targetDir = selectedUnit.transform.position - transform.position;
                Vector3 forward = transform.forward;
                float angle = Vector3.Angle(targetDir, forward);

                if (angle > 20.0)
                {
                    canAttack = false;
                }
                else
                {
                    if (distance < 20.0)
                    {
                        canAttack = true;
                    }
                    else
                    {
                        canAttack = false;
                    }
                }

                //To Deselect an enemy
                if (doubleClickTimer > 0)
                {
                    doubleClickTimer -= Time.deltaTime;
                }
                else
                {
                    didDoubleClick = false;
                }
            }
            //Attack
            if (Input.GetKeyDown(KeyCode.B))
            {
                //ToDo:make sure player is facing enemy and is in attack range
                if (selectedUnit != null && canAttack)
                {
                    BasicAttack();
                }
            }

            //Ranged Spell Attack
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (selectedUnit != null)
                {
                    //playerSpells.Find(x => x.name)
                    //RangedSpell();
                    CastSpell();
                }
            }
        }

        void SelectTarget()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 10000))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    selectedUnit = hit.transform.gameObject;

                    selectedUnit.transform.GetComponent<EnemyStats>().Selected();

                    enemyStatsScript = selectedUnit.transform.gameObject.transform.GetComponent<EnemyStats>();

                }
            }
            else
            {
                if (selectedUnit != null)
                {
                    if (didDoubleClick == false)
                    {
                        didDoubleClick = true;
                        doubleClickTimer = 0.5f;
                    }
                    else
                    {
                        selectedUnit.transform.GetComponent<EnemyStats>().Deselected();
                        selectedUnit = null;
                        didDoubleClick = false;
                        doubleClickTimer = 0;
                    }
                }
            }
        }

        void BasicAttack()
        {
            enemyStatsScript.ReceiveDamage(currentAttackPower);
        }

        void CastSpell()
        {
            //playerSpells.Find(x => x.name == "Fireball").TriggerSpell(rangedSpellPrefab);
            playerSpells.Find(x => x.name == "Fireball").TriggerSpell(this.gameObject);
        }

        public void ReceiveDamage(float dmg)
        {
            currentHealth -= dmg;
        }

        /*Listerner for the player exp*/
        public int PlayerSkillPointListener
        {
            get { return exp; }
            set
            {
                exp = value;

                //If we have subscribers, then tell them the exp has changed;
                ExpChange?.Invoke();
            }
        }
        public int PlayerLevelListener
        {
            get { return level; }
            set
            {
                level = value;

                //If we have subscribers, then tell them the level has changed;
                LevelChange?.Invoke();

            }
        }

        public int PlayerUnallocStatListener
        {
            get { return unassignedStatPoints; }
            set
            {
                unassignedStatPoints = value;

                //If we have subscribers, then tell them the unassigned stat points have changed
                StatChange?.Invoke();
            }
        }

        //Delegates for the listeners
        public delegate void OnExpChange();
        public event OnExpChange ExpChange;

        public delegate void OnLevelChange();
        public event OnLevelChange LevelChange;

        public delegate void OnStatPointChange();
        public event OnStatPointChange StatChange;
    }
}