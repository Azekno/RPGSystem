using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Stat/Physical Stat")]

    public class PhysicalStat : Stat
    {
        public GameObject player;

        public bool isAttack = false;
        public bool isDefense = false;
        public bool isAgility = false;
        public float statMultiplier = 10;
        private float attkValue;
        private float tempHealthValue;

        public override void Initialize()
        {
            attkValue = statValue * statMultiplier;
            tempHealthValue = (statValue * statMultiplier);
            if (isAttack)
            {
                player.GetComponent<PlayerStats>().currentAttackPower += attkValue;
            }
            else if(affectsHealth)
            {
                player.GetComponent<PlayerStats>().baseHealth += tempHealthValue;
                player.GetComponent<PlayerStats>().currentHealth += tempHealthValue;
            }
            else if(isAgility)
            {

            }
        }
    }
}