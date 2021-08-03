using UnityEngine;

namespace CharacterSpace
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerStats playerStats;

        public string statMenuHotKey;
        public string skillMenuHotKey;
        public string spellMenuHotKey;

        private bool timePaused = false;

        [SerializeField]
        private Canvas playerInterface = null;
        private bool seePlayerInterface;

        [SerializeField]
        private Canvas statMenu = null;
        private bool seeStatMenu;

        [SerializeField]
        private Canvas skillMenu = null;
        private bool seeSkillMenu;

        [SerializeField]
        private Canvas spellMenu = null;
        private bool seeSpellMenu;

        //Player Menus
        public bool displayPlayerUI = true;
        public bool displayStats = false;
        public bool displaySkills = false;
        public bool displaySpells = false;


        // Update is called once per frame
        void Update()
        {
            ///If the gui button is pressed in game or the hotkey for the menu, it will pause the game time and then open this menu.
            ///If you have another menu already open, it will disable all other menus
            if (displayStats || Input.GetKeyDown(statMenuHotKey))
            {
                if (statMenu)
                {
                    if (timePaused)
                    {
                        ResumeGame();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
            }

            //Can use either of these Get Key Downs to return that if r is pressed then the skill canvas will appear 
            //if(Input.GetKeyDown("r") || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(skillMenuHotKey))
            if(displaySkills || Input.GetKeyDown(skillMenuHotKey))
            {
                if(skillMenu)
                {
                    if (timePaused)
                    {
                        ResumeGame();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
            }
            if(displaySpells || Input.GetKeyDown(spellMenuHotKey))
            {
                if(spellMenu)
                {
                    if (timePaused)
                    {
                        ResumeGame();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
            }
        }

        public void PauseGame()
        {
            timePaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            timePaused = false;
            Time.timeScale = 1;
        }
    }
}
