using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{ 
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string description;
        public Sprite icon;
        public int levelNeeded;
        public int skillPointsNeeded;

        public List<CharacterAttributes> affectedAttributes = new List<CharacterAttributes>();

        public void SetValues(GameObject skillDisplayObject, PlayerStats player)
        {
            if(player)
            {
                CheckSkills(player);
            }

            //bit of error handling, to make sure that the ScriptableObject is used
            if(skillDisplayObject)
            {
                SkillDisplay skillDisplay = skillDisplayObject.GetComponent<SkillDisplay>();
                skillDisplay.skillName.text = name;
                if(skillDisplay.skillDescription)
                {
                    skillDisplay.skillDescription.text = description;
                }
                if(skillDisplay.skillIcon)
                {
                    skillDisplay.skillIcon.sprite = icon;
                }
                if(skillDisplay.skillLevel)
                {
                    skillDisplay.skillLevel.text = levelNeeded.ToString();
                }
                if(skillDisplay.skillPointsNeeded)
                {
                    skillDisplay.skillPointsNeeded.text = skillPointsNeeded.ToString();
                }
                if(skillDisplay.skillAttribute)
                {
                    skillDisplay.skillAttribute.text = affectedAttributes[0].attribute.ToString();
                }
                if(skillDisplay.skillAttributeAmount)
                {
                    skillDisplay.skillAttributeAmount.text = "+" + affectedAttributes[0].baseValue.ToString();
                }
            }
        }
        public bool CheckSkills(PlayerStats player)
        {
            //Check if player is the right level
            if(player.level < levelNeeded)
            {
                return false;
            }
            //Check if player has enough skill points
            if(player.unassignedSkillPoints < skillPointsNeeded)
            {
                return false;
            }
            //otherwise they can enable this skill
            return true;
        }
        public bool EnableSkill(PlayerStats player)
        {
            //Go through all the skills that the player currently has
            List<Skills>.Enumerator skills = player.playerSkills.GetEnumerator();
            while(skills.MoveNext())
            {
                var CurrSkill = skills.Current;
                if(CurrSkill.name == this.name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool GetSkill(PlayerStats player)
        {
            int i = 0;
            //List through the Skill's Attributes
            List<CharacterAttributes>.Enumerator skillAttributes = affectedAttributes.GetEnumerator();
            while(skillAttributes.MoveNext())
            {
                //List through the Players attributes and match with Skill attribute
                List<Stat>.Enumerator playerAttributes = player.stats.GetEnumerator();
                while(playerAttributes.MoveNext())
                {
                    if(skillAttributes.Current.attribute.name.ToString() == playerAttributes.Current.name.ToString())
                    {
                        //update the players attributes
                        playerAttributes.Current.statValue += skillAttributes.Current.baseValue;
                        //mark that an attribute was updated
                        i++;
                    }
                }
                if(i > 0)
                {
                    //reduce the skill points from the player
                    player.unassignedSkillPoints -= this.skillPointsNeeded;
                    //add to the list of skills
                    player.playerSkills.Add(this);
                    return true;
                }
            }
            return false;
        }
    }
}
