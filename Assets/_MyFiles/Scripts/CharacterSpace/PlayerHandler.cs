using UnityEngine;

namespace CharacterSpace
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerStats player;

        public string statMenuHotKey;
        public string skillMenuHotKey;
        public string spellMenuHotKey;

        [SerializeField]
        private Canvas statMenu = null;
        private bool seeStatMenu;

        [SerializeField]
        private Canvas skillMenu = null;
        private bool seeSkillMenu;

        [SerializeField]
        private Canvas spellMenu = null;
        private bool seeSpellMenu;


        // Update is called once per frame
        void Update()
        {
            if(player.displayStats || Input.GetKeyDown(statMenuHotKey))
            {
                if(statMenu)
                {
                    Time.timeScale = 0;
                    seeSkillMenu = false;
                    seeSpellMenu = false;
                    seeStatMenu = !seeStatMenu;
                    statMenu.gameObject.SetActive(seeStatMenu);// will display the stat menu depending on the bool
                    skillMenu.gameObject.SetActive(seeSkillMenu);
                    spellMenu.gameObject.SetActive(seeSpellMenu);
                }
                else
                {
                    Time.timeScale = 1;
                }
                player.displayStats = false;
            }

            //Can use either of these Get Key Downs to return that if r is pressed then the skill canvas will appear 
            //if(Input.GetKeyDown("r") || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(skillMenuHotKey))
            if(player.displaySkills || Input.GetKeyDown(skillMenuHotKey))
            {
                if(skillMenu)
                {
                    Time.timeScale = 0;
                    seeStatMenu = false;
                    seeSpellMenu = false;
                    seeSkillMenu = !seeSkillMenu;
                    skillMenu.gameObject.SetActive(seeSkillMenu);// displays the canvas or not depending on the state of the bool
                    statMenu.gameObject.SetActive(seeStatMenu);
                    spellMenu.gameObject.SetActive(seeSpellMenu);
                }
                else
                {
                    Time.timeScale = 1;
                }
                player.displaySkills = false;
            }
            ///If the gui button is pressed in game or the hotkey for the menu, it will pause the game time and then open this menu.
            ///If you have another menu already open, it will disable all other menus
            if(player.displaySpells || Input.GetKeyDown(spellMenuHotKey))
            {
                if(spellMenu)
                {
                    Time.timeScale = 0;
                    seeStatMenu = false;
                    seeSkillMenu = false;
                    seeSpellMenu = !seeSpellMenu;
                    spellMenu.gameObject.SetActive(seeSpellMenu);// displays the spell canvas depending on the state of the bool
                    statMenu.gameObject.SetActive(seeStatMenu);
                    skillMenu.gameObject.SetActive(seeSkillMenu);
                }
                else 
                {
                    Time.timeScale = 1;
                }
                player.displaySpells = false;
            }
        }
    }
}
