using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Stat/Magical Stat")]
    public class MagicalStat : Stat
    {
        public bool isMagAttk = false;
        public bool isMagDef = false;
        public bool affectsManaRegen = false;

        public float statMultiplier = 10;
        private float magValue;

        /// <summary>
        /// Intializes the values for the magical stat and gives those values to the gameobject that this function is called for/on
        /// </summary>
        /// <param name="obj"></param>
        public override void Initialize(GameObject obj)
        {
            magValue = obj.GetComponent<PlayerStats>().baseStatValue * statMultiplier;
            if (statChanged)
            {
                if (affectsMana && statChanged)
                {
                    obj.GetComponent<PlayerStats>().maxMana += magValue / 10;
                    obj.GetComponent<PlayerStats>().manaChanged = true;
                }
                if (isMagAttk & statChanged)
                {
                    obj.GetComponent<PlayerStats>().currentMagicAttackPower += magValue / 20;
                }
                if (isMagDef && statChanged)
                {
                    obj.GetComponent<PlayerStats>().currentMagicDefense += magValue / 20;
                }
                if (affectsManaRegen && statChanged)
                {
                    obj.GetComponent<PlayerStats>().manaRegenAmount += (obj.GetComponent<PlayerStats>().maxMana + magValue) / 100;
                }
            }
        }
    }
}