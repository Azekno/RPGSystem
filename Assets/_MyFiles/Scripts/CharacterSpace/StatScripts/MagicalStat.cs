using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Stat/Magical Stat")]
    public class MagicalStat : Stat
    {
        public GameObject player;

        public bool isMagAttk = false;
        public bool isMagDef = false;

        public float statMultiplier = 10;
        private float magValue;
        private float tempManaValue;

        public override void Initialize()
        {
            magValue = statValue * statMultiplier;
            tempManaValue = (statValue * statMultiplier) / 2;
            if (isMagAttk)
            {
                player.GetComponent<PlayerStats>().currentMagicAttackPower += magValue;
            }
            else if(affectsMana)
            {
                player.GetComponent<PlayerStats>().baseMana += tempManaValue;
            }
        }
    }
}