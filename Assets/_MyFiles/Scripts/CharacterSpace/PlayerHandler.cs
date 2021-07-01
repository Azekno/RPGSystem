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

        //Texture
        public Texture hpBarTexture;
        public Texture manaBarTexture;
        public Texture toolTipBackgroundTexture;

        //User GUI Bars stats
        /*public float hpBarLength;
        public float percentOfHp;
        public float manaBarLength;
        public float percentOfMana;*/


        //Player Menus
        public bool displayPlayerUI = true;
        public bool displayStats = false;
        public bool displaySkills = false;
        public bool displaySpells = false;


        // Update is called once per frame
        void Update()
        {
            if(displayPlayerUI)
            {
                if(playerInterface)
                {
                    TogglePlayerUI();
                }
            }
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
                    ToggleStatMenu();
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
                    ToggleSkillMenu();
                }
            }
            ///If the gui button is pressed in game or the hotkey for the menu, it will pause the game time and then open this menu.
            ///If you have another menu already open, it will disable all other menus
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
                    ToggleSpellMenu();
                }
            }
        }

        /*private void OnGUI()
        {
            if (displayPlayerUI)
            {
                //Hp and Mana Bars
                //Textures
                GUI.DrawTexture(new Rect(20, 30, 120, 70), toolTipBackgroundTexture);
                *//*GUI.DrawTexture(new Rect(30, 40, hpBarLength, 20), hpBarTexture);
                GUI.DrawTexture(new Rect(30, 65, manaBarLength, 20), manaBarTexture);*//*
                GUI.DrawTexture(new Rect(30, 40, 100, 20), hpBarTexture);
                GUI.DrawTexture(new Rect(30, 65, 100, 20), manaBarTexture);
            
                //GUI.Label(new Rect(50, 40, 200, 20), "" + currentHealth + " / " + maxHealth);
                //GUI.Label(new Rect(50, 65, 200, 20), "" + currentMana + " / " + maxMana);
            }


            //Stat Menu Button
            if (GUI.Button(new Rect(50, Screen.height - 64, 100, 64), "Stat Menu"))
            {
                displayStats = !displayStats;
            }
            //Skill Menu Button
            if (GUI.Button(new Rect(150, Screen.height - 64, 100, 64), "Skill Menu"))
            {
                displaySkills = !displaySkills;
            }
            //Spell Menu Button
            if (GUI.Button(new Rect(250, Screen.height - 64, 100, 64), "Spells Menu"))
            {
                displaySpells = !displaySpells;
            }
        }*/

        private void TogglePlayerUI()
        {
            seePlayerInterface = !seePlayerInterface;
            seeStatMenu = false;
            seeSkillMenu = false;
            seeSpellMenu = false;
            playerInterface.gameObject.SetActive(seePlayerInterface);
            statMenu.gameObject.SetActive(seeStatMenu);
            skillMenu.gameObject.SetActive(seeSkillMenu);
            spellMenu.gameObject.SetActive(seeSpellMenu);
        }


        private void ToggleStatMenu()
        {
            seePlayerInterface = false;
            seeSkillMenu = false;
            seeSpellMenu = false;
            seeStatMenu = !seeStatMenu;
            playerInterface.gameObject.SetActive(seePlayerInterface);
            statMenu.gameObject.SetActive(seeStatMenu);// will display the stat menu depending on the bool
            skillMenu.gameObject.SetActive(seeSkillMenu);
            spellMenu.gameObject.SetActive(seeSpellMenu);
        }

        private void ToggleSkillMenu()
        {
            seePlayerInterface = false;
            seeStatMenu = false;
            seeSpellMenu = false;
            seeSkillMenu = !seeSkillMenu;
            playerInterface.gameObject.SetActive(seePlayerInterface);
            statMenu.gameObject.SetActive(seeStatMenu);
            skillMenu.gameObject.SetActive(seeSkillMenu);// displays the canvas or not depending on the state of the bool
            spellMenu.gameObject.SetActive(seeSpellMenu);
        }

        private void ToggleSpellMenu()
        {
            seePlayerInterface = false;
            seeStatMenu = false;
            seeSkillMenu = false;
            seeSpellMenu = !seeSpellMenu;
            playerInterface.gameObject.SetActive(seePlayerInterface);
            statMenu.gameObject.SetActive(seeStatMenu);
            skillMenu.gameObject.SetActive(seeSkillMenu);
            spellMenu.gameObject.SetActive(seeSpellMenu);// displays the spell canvas depending on the state of the bool
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
