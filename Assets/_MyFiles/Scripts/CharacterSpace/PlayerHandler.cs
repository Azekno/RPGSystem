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
            if(Input.GetKeyDown(statMenuHotKey))
            {
                if(statMenu)
                {
                    seeStatMenu = !seeStatMenu;
                    statMenu.gameObject.SetActive(seeStatMenu);// will display the stat menu depending on the bool
                }
            }

            //Can use either of these Get Key Downs to return that if r is pressed then the skill canvas will appear 
            //if(Input.GetKeyDown("r") || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(skillMenuHotKey))
            if(Input.GetKeyDown(skillMenuHotKey))
            {
                if(skillMenu)
                {
                    seeSkillMenu = !seeSkillMenu;
                    skillMenu.gameObject.SetActive(seeSkillMenu);// displays the canvas or not depending on the state of the bool
                }
            }
            if(Input.GetKeyDown(spellMenuHotKey))
            {
                if(spellMenu)
                {
                    seeSpellMenu = !seeSpellMenu;
                    spellMenu.gameObject.SetActive(seeSpellMenu);// displays the spell canvas depending on the state of the bool
                }

            }
        }
    }
}
