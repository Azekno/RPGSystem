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
        private float tempManaValue;

        public override void Initialize(GameObject obj)
        {
            magValue = statValue * statMultiplier;
            tempManaValue = (statValue * statMultiplier) / 2;
            if(affectsMana)
            {
                obj.GetComponent<PlayerStats>().maxMana += tempManaValue;
            }
            if (isMagAttk)
            {
                obj.GetComponent<PlayerStats>().currentMagicAttackPower += magValue;
            }
            if(isMagDef)
            {
                obj.GetComponent<PlayerStats>().currentMagicDefense += magValue;
            }
            if(affectsManaRegen)
            {
                obj.GetComponent<PlayerStats>().manaRegenAmount += (obj.GetComponent<PlayerStats>().maxMana + magValue) / 100;
            }
        }
    }
}