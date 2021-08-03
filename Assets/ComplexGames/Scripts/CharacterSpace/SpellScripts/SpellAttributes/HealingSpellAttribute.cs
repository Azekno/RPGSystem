using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Healing Spell")]
    public class HealingSpellAttribute : Spell
    {
        public string description;
        public Sprite icon;
        public bool isAOE;
        public bool isHealOverTime;

        public int healingAbility;

        public GameObject spellPrefab;

        public void SingleTargetHealing()
        {
            //spellPrefab
        }

        public void AOEHeal()
        {

        }
        public void SingleTargetHealOverTime()
        {

        }
        public void AOEHealOverTime()
        {

        }

        public override void TriggerSpell(GameObject obj)
        {
            if (!isHealOverTime)
            {
                if (!isAOE)
                {
                    SingleTargetHealing();
                }
                else
                {
                    AOEHeal();
                }
            }
            else
            {
                if (!isAOE)
                {
                    SingleTargetHealOverTime();
                }
                else
                {
                    AOEHealOverTime();
                }
            }
        }

    }
}
