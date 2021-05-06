using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    public class PlayerStats : MonoBehaviour
    {
        /*/// <summary>
        /// All apart from health can be "turned off" meaning that different games can be made using this rpg system by changing the stats used
        /// i.e. Melee combat only game, so mana and any magic related stat can be disabled
        /// </summary>
        public int baseHealth;
        public int currentHealth;
        [SerializeField] protected int maxHealth;

        public int baseMana;
        public int currentMana;
        [SerializeField] protected int maxMana;*/

        [Header("Main Stats")]
        public string playerName;
        
        [SerializeField]
        private int currentHealth;
        [SerializeField]
        private int currentMana;
        
        public int level = 1;
        public int exp = 0;
        public int nextLevelXP;
        public float expExponent = 1;
        public float baseExpValue = 100;
        
        public int unassignedAttributes = 0;
        public int unassignedSkillPoints = 0;

        //The health level modifier is the amount of health that increases the players health when they level
        public int healthLevelModifier = 10;
        //The mana level modifier is just how much mana is increased upon level up.
        public int manaLevelModifier = 10;
        //The amount of attributes gained when the player levels
        public int attributeLevelModifier = 2;
        //Skill points gained upon level
        public int skillPointLevelModifier = 1;
        
        public int maxHealth = 100;
        public int maxMana = 100;

        //Creates a list for the players stats
        [Header("Player Attributes")]
        public List<CharacterAttributes> attributes = new List<CharacterAttributes>();

        //Creates a list to the resistances
        [Header("Player Resistances")]
        public List<CharacterAttributes> resistance = new List<CharacterAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> playerSkills = new List<Skills>();

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
            unassignedAttributes += attributeLevelModifier;
            unassignedSkillPoints += skillPointLevelModifier;
            maxHealth += healthLevelModifier;
            maxMana += manaLevelModifier;
            currentHealth = maxHealth;
            currentMana = maxMana;
        }

        void IncreaseAttributes()
        {
            if(unassignedAttributes > 0)
            {
                CharacterAttributes tempAttri = attributes.Find(x => x.attribute.name == "Strength");
                tempAttri.baseValue += 2;
                unassignedAttributes--;
            }
        }
        private void Start()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
        }

        void Update()
        {
            ExpToNextLevel(level);

            if(exp >= nextLevelXP)
            {
                LevelUp();
            }
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                IncreaseAttributes();
            }
        }

        /*Listerner for the player exp*/
        public int PlayerExpListener
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