using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    public class PlayerStats : MonoBehaviour
    {
        public ParticleSystem particle;
        public bool particleSystemActive;

        [Header("Main Stats")]
        public string playerName;
        public int level = 1;
        public int exp = 0;

        public float baseHealth = 100;
        public float currentHealth;
        [SerializeField]
        private float maxHealth;

        public float baseMana = 100;
        public float currentMana;
        [SerializeField]
        private float maxMana;

        public float baseAttackPower;
        public float currentAttackPower;
        public float baseAttackSpeed;
        public float currentAttackSpeed;
        //public float baseDodge;
        //public float currentDodge;
        public float baseCritHitPercent;
        public float currentCritHitPercent;

        public float baseMagicAttackPower;
        public float currentMagicAttackPower;
        public float spellCastTimer;

        public float hpRegenTimer;
        public float hpRegenAmount;
        public float manaRegenTimer;
        public float manaRegenAmount;

        public int nextLevelXP;
        public float expExponent = 1;
        public float baseExpValue = 100;
        
        public int unassignedAttributes = 0;
        public int unassignedSkillPoints = 0;
        public int unassignedSpellPoints = 0;

        //The health level modifier is the amount of health that increases the players health when they level
        public int healthLevelModifier = 10;
        //The mana level modifier is just how much mana is increased upon level up.
        public int manaLevelModifier = 10;
        //The amount of attributes gained when the player levels
        public int levelUpAttributeModifier = 2;
        //Skill points gained upon level
        public int levelSkillPointModifier = 1;
        //Spell points gained upon level
        public int spellPointsLevelModifier = 1;

        public int statIncreaseModifier;

        public bool isDead;

        public GameObject selectedUnit;
        public EnemyStats enemyStatsScript;

        public bool behindEnemy = false;
        public bool canAttack = false;
        public bool canCast = false;


        public GameObject rangedSpellPrefab;


        //Creates a list for the players stats
        [Header("Player Attributes")]
        public List<CharacterAttributes> attributes = new List<CharacterAttributes>();

        //Creates a list to the resistances
        [Header("Player Resistances")]
        public List<CharacterAttributes> resistance = new List<CharacterAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> playerSkills = new List<Skills>();

        [Header("Player Spells Enabled")]
        public List<Spells> playerSpells = new List<Spells>();

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
            unassignedAttributes += levelUpAttributeModifier;
            unassignedSkillPoints += levelSkillPointModifier;
            maxHealth += healthLevelModifier;
            maxMana += manaLevelModifier;
            currentHealth = maxHealth;
            currentMana = maxMana;
            IncreaseSpellPoints();
            particle.Play();

        }

        void IncreaseAttributes()
        {
            if(unassignedAttributes > 0)
            {
                List<CharacterAttributes>.Enumerator playerAttributes = attributes.GetEnumerator();
                while(playerAttributes.MoveNext())
                {
                    var currentAttribute = playerAttributes.Current;
                    if(currentAttribute.attribute.name == this.name)
                    {
                        currentAttribute.baseValue += statIncreaseModifier;
                        playerAttributes.Current.baseValue += currentAttribute.baseValue;
                        unassignedAttributes--;
                    }
                }
            }
        }

        void IncreaseSpellPoints()
        {
            unassignedSpellPoints += spellPointsLevelModifier;

        }
        private void Start()
        {
            maxHealth = (baseHealth + (attributes.Find(xc => xc.attribute.name == "Vitality").baseValue * 10));
            maxMana = (baseMana + (attributes.Find(x => x.attribute.name == "Wisdom").baseValue * 2)) / 2;
            currentAttackPower = (baseAttackPower + (attributes.Find(x => x.attribute.name == "Strength").baseValue * 2)) / 2;
            currentMagicAttackPower = (baseMagicAttackPower + (attributes.Find(x => x.attribute.name == "Intelligence").baseValue * 2)) / 2;
            currentAttackSpeed = (baseAttackSpeed + (attributes.Find(x => x.attribute.name == "Dexterity").baseValue * 2)) / 2;
            currentCritHitPercent = (baseCritHitPercent + (attributes.Find(x => x.attribute.name == "Agility").baseValue * 2)) / 4;

            currentHealth = maxHealth;
            currentMana = maxMana;

            hpRegenAmount = (maxHealth + attributes.Find(x => x.attribute.name == "Vitality").baseValue) / 100;
            particle.Stop();
        }

        void Update()
        {
            AdjustingHealth();
            ExpToNextLevel(level);
            if (exp >= nextLevelXP)
            {
                LevelUp();
            }
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                IncreaseAttributes();
            }
            if(Input.GetMouseButtonDown(0))
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

                if(angle > 20.0)
                {
                    canAttack = false;
                }
                else
                {
                    if(distance < 20.0)
                    {
                        canAttack = true;
                    }
                    else
                    {
                        canAttack = false;
                    }
                }
            }
            //Attack
            if(Input.GetKeyDown(KeyCode.B))
            {
                //ToDo:make sure player is facing enemy and is in attack range
                if(selectedUnit != null && canAttack)
                {
                    BasicAttack();
                }
            }

            //Ranged Spell Attack
            if(Input.GetKeyDown(KeyCode.M))
            {
                if(selectedUnit != null)
                {
                    RangedSpell();
                }
            }
        }

        void SelectTarget()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 10000))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Hit has occured");
                    selectedUnit = hit.transform.gameObject;

                    enemyStatsScript = selectedUnit.transform.gameObject.transform.GetComponent<EnemyStats>();
                }
            }
        }

        void BasicAttack()
        {
            enemyStatsScript.ReceiveDamage(currentAttackPower);
        }

        void RangedSpell()
        {
            Vector3 spawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

            GameObject clone;
            clone = Instantiate(rangedSpellPrefab, spawnSpellLoc, Quaternion.identity);
            clone.GetComponent<RangedSpell>().target = selectedUnit;
            enemyStatsScript.ReceiveDamage(currentMagicAttackPower);
        }

        public void AdjustingHealth()
        {
            if (currentHealth <= 0)
            {
                //Player will die
            }
            if (currentHealth < maxHealth)
            {
                currentHealth += hpRegenAmount * Time.deltaTime;
            }
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }




        /*Listerner for the player exp*/
        public int PlayerSkillPointListener
        {
            get { return exp; }
            set
            {
                exp = value;

                //If we have subscribers, then tell them the exp has changed;
                if(onSkillPointChange != null)
                {
                    onSkillPointChange();
                }
            }
        }
        public int PlayerLevelListener
        {
            get { return level; }
            set 
            {
                level = value;

                //If we have subscribers, then tell them the level has changed;
                if(onLevelChange != null)
                {
                    onLevelChange();
                }
            }
        }

        //Delegates for the listeners
        public delegate void OnExpChange();
        public event OnExpChange onSkillPointChange;

        public delegate void OnLevelChange();
        public event OnLevelChange onLevelChange;
    }
}