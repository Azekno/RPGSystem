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

        /// <summary>
        /// Intializes the values for the physical stat and gives those values to the gameobject that this function is called for/on
        /// </summary>
        /// <param name="obj"></param>
        public override void Initialize(GameObject obj)
        {
            physValue = obj.GetComponent<PlayerStats>().baseStatValue * statMultiplier;

            if (affectsHealth && statChanged)
            {
                obj.GetComponent<PlayerStats>().maxHealth += physValue / 10;
                obj.GetComponent<PlayerStats>().healthChanged = true;
            }
            if (isAttack && statChanged)
            {
                obj.GetComponent<PlayerStats>().currentAttackPower += physValue / 20;
            }
            if(isAgility && statChanged)
            {
                obj.GetComponent<PlayerStats>().currentAttackSpeed += physValue / 100;
            }
            if(isDefense && statChanged)
            {
                //obj.GetComponent<PlayerStats>().currentPhysicalDefense += obj.GetComponent<PlayerStats>().baseStatValue;
                obj.GetComponent<PlayerStats>().currentPhysicalDefense += physValue / 20;
            }
            if(affectsHealthRegen && statChanged)
            {
                obj.GetComponent<PlayerStats>().hpRegenAmount += (obj.GetComponent<PlayerStats>().maxHealth + physValue)/ 100;
            }
        }
    }
}