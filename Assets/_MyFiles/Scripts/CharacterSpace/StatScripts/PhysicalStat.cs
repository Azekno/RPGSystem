using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Stat/Physical Stat")]

    public class PhysicalStat : Stat
    {
        public bool isAttack = false;
        public bool isDefense = false;
        public bool isAgility = false;
        public bool affectsHealthRegen = false;

        public float statMultiplier = 10;
        private float physValue = 0;
        //private float tempHealthValue;

        public override void Initialize(GameObject obj)
        {
            physValue = statValue * statMultiplier;
            //tempHealthValue = (statValue * statMultiplier);
            if(affectsHealth)
            {
                //obj.GetComponent<PlayerStats>().maxHealth += tempHealthValue;
                obj.GetComponent<PlayerStats>().maxHealth += physValue;
            }
            if (isAttack)
            {
                obj.GetComponent<PlayerStats>().currentAttackPower += physValue;
            }
            if(isAgility)
            {
                obj.GetComponent<PlayerStats>().currentAttackSpeed += physValue / 2;
            }
            if(isDefense)
            {
                obj.GetComponent<PlayerStats>().currentPhysicalDefense += statValue;
            }
            if(affectsHealthRegen)
            {
                obj.GetComponent<PlayerStats>().hpRegenAmount += (obj.GetComponent<PlayerStats>().maxHealth + physValue)/ 100;
            }
        }
    }
}