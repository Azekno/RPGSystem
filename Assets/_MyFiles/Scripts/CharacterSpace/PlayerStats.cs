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
        
        [HideInInspector]
        public float maxHealth;

        public float baseMana = 100;
        public float currentMana;
        
        [HideInInspector]
        public float maxMana;

        //public float baseAttackPower;
        public float currentAttackPower;
        //public float baseAttackSpeed;
        public float currentAttackSpeed;
        //public float baseDodge;
        //public float currentDodge;
        //public float baseCritHitPercent;
        //public float currentCritHitPercent;

        //public float baseMagicAttackPower;
        public float currentMagicAttackPower;

        public float currentPhysicalDefense;
        public float currentMagicDefense;

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

        public bool didDoubleClick = false;
        public float doubleClickTimer;

        public GameObject rangedSpellPrefab;

        //Texture
        public Texture hpBarTexture;
        public Texture manaBarTexture;
        public Texture toolTipBackgroundTexture;

        //User GUI Bars stats
        public float hpBarLength;
        public float percentOfHp;
        public float manaBarLength;
        public float percentOfMana;


        //Player Menus
        public bool PlayerSpellShowMenu;

        //Creates a list for the players stats
        [Header("Player Stats")]
        public List<Stat> stats = new List<Stat>();

        //public List<CharacterAttributes> attributes = new List<CharacterAttributes>();

        //Creates a list to the resistances
        [Header("Player Resistances")]
        public List<Stat> resistance = new List<Stat>();

        [Header("Player Skills Enabled")]
        public List<Skills> playerSkills = new List<Skills>();

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
            /*if (unassignedAttributes > 0)
            {
                List<Stat>.Enumerator playerAttributes = attributes.GetEnumerator();
                while (playerAttributes.MoveNext())
                {
                    var currentAttribute = playerAttributes.Current;
                    if (currentAttribute.name == this.name)
                    {
                        currentAttribute.statValue += statIncreaseModifier;
                        playerAttributes.Current.statValue += currentAttribute.statValue;
                        unassignedAttributes--;
                    }
                }
            }*/
        }

        void IncreaseSpellPoints()
        {
            unassignedSpellPoints += spellPointsLevelModifier;

        }

        private void OnGUI()
        {
            //Hp and Mana Bars
            //Textures
            GUI.DrawTexture(new Rect(20, 30, 120, 70), toolTipBackgroundTexture);
            /*GUI.DrawTexture(new Rect(30, 40, hpBarLength, 20), hpBarTexture);
            GUI.DrawTexture(new Rect(30, 65, manaBarLength, 20), manaBarTexture);*/
            GUI.DrawTexture(new Rect(30, 40, 100, 20), hpBarTexture);
            GUI.DrawTexture(new Rect(30, 65, 100, 20), manaBarTexture);

            GUI.Label(new Rect(50, 40, 200, 20), "" + currentHealth + " / " + maxHealth);
            GUI.Label(new Rect(50, 65, 200, 20), "" + currentMana + " / " + maxMana);


            //Tooltip spell buttons
            Rect rect1 = new Rect(Screen.width / 2, Screen.height - 64, 32, 32);

            //if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 64, 32, 32), "5"));
            //{
            //    UsedSpell(playerTestSpells[0].id);
            //}
            //if(rect1.Contains(Event.current.mousePosition))
            //{
            //    GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 150, 200, 200), toolTipBackgroundTexture);
            //    GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 150, 200, 200),
            //        "Spell name: " + playerTestSpells[0].name + "\n" +
            //        "Spell description: " + playerTestSpells[0].description + "\n" +
            //        "Spell id: " + playerTestSpells[0].id);
            //}

            //Spell Menu Button
            if (GUI.Button(new Rect(50, Screen.height - 64, 32, 64), "Player Spells Menu Button"))
            {
                PlayerSpellShowMenu = !PlayerSpellShowMenu;
            }

            /*//player spell menu
            if(PlayerSpellShowMenu)
            {
                //show player spell menu for spells learned
            }*/
        }

        private void Start()
        {
            particle.Stop();
            maxHealth = baseHealth;
            maxMana = baseMana;
            
            foreach(Stat stat in stats)
            {
                stat.Initialize(gameObject);
            }
            currentHealth = maxHealth;
            currentMana = maxMana;
            //for(int i = 0; i < stats.Count; i++)
            //{
            //    stats[i].Initialize(gameObject);
            //}
        }

        void Update()
        {
            //Test spells
            if (Input.GetKeyDown("5"))
            {
                //UsedSpell(playerTestSpells[0].id);
            }
            //Test spells
            if (Input.GetKeyDown("6"))
            {
                //UsedSpell(playerTestSpells[1].id);
            }

            if (currentHealth <= 0)
            {
                //Player will die
                currentHealth = 0;
            }
            if (currentHealth < maxHealth)
            {
                percentOfHp = currentHealth / maxHealth;
                hpBarLength = percentOfHp * 100;
                currentHealth += hpRegenAmount * Time.deltaTime;
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
                manaBarLength = percentOfMana * 100;
                currentMana += manaRegenAmount * Time.deltaTime;
            }
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }

            //AdjustingHealth();
            ExpToNextLevel(level);
            if (exp >= nextLevelXP)
            {
                LevelUp();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                IncreaseAttributes();
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
                }
            }
        }

        void SelectTarget()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
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
            //playerSpells.
        }

        public void ReceiveDamage(float dmg)
        {
            currentHealth -= dmg;
        }

        public void AdjustingHealth()
        {
            if (currentHealth <= 0)
            {
                //Player will die
            }
            if (currentHealth < maxHealth)
            {
                percentOfHp = currentHealth / maxHealth;
                hpBarLength = percentOfHp * 100;
                currentHealth += hpRegenAmount * Time.deltaTime;
            }
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void AdjustingMana()
        {

        }

        void UsedSpell(int id)
        {
            switch (id)
            {
                case 0:
                    Debug.Log("Used spell 1");
                    break;
                case 1:
                    Debug.Log("Used spell 2");
                    break;
                case 2:
                    Debug.Log("Used spell 3");
                    break;
                case 3:
                    Debug.Log("Used spell 4");
                    break;
                case 4:
                    Debug.Log("Used spell 5");
                    break;
                default:
                    Debug.Log("Spell Error");
                    break;
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