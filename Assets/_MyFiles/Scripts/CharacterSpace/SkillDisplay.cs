using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CharacterSpace
{
    public class SkillDisplay : MonoBehaviour
    {
        //Get the Scriptable Object for the Skill
        public Skills skill;
        //Get the UI components
        public Text skillName;
        public Text skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillPointsNeeded;
        public Text skillAttribute;
        public Text skillAttributeAmount;

        [SerializeField]
        private PlayerStats playerHandler;

        // Start is called before the first frame update
        void Start()
        {
            playerHandler = this.GetComponentInParent<PlayerHandler>().playerStats;
            //Listener for the skill point change
            playerHandler.onSkillPointChange += ReactToChange;
            //Listener for the level change
            playerHandler.onLevelChange += ReactToChange;

            if(skill)
            {
                skill.SetValues(this.gameObject, playerHandler);
            }
        }

        public void EnableSkills()
        {
            //if the player has the skill already, then show it as enabled
            if(playerHandler && skill && skill.EnableSkill(playerHandler))
            {
                TurnOnSkillIcon();
            } 
            //if the player doesn't have the skill but can acquire it, then show it as interactable
            else if(playerHandler && skill && skill.CheckSkills(playerHandler))
            {
                this.GetComponent<Button>().interactable = true;
                //This is a quick fix and can be changed// 
                this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        private void OnEnable()
        {
            EnableSkills();
        }

        //Method to be used when you click the Skill icon
        public void GetSkill()
        {
            if(skill.GetSkill(playerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        //Turn on the Skill Icon - stop it from being clickable and sdiable the UI elements that make it change colour
        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }

        //Turn off the Skill Icon so it cannot be used - stop it from being clickable and enable the UI elements that make it change colour
        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            playerHandler.onSkillPointChange -= ReactToChange;
        }

        //event for when the listener is woken
        void ReactToChange()
        {
            EnableSkills();
        }
    }
}
