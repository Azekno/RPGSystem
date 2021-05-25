using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Healing Spell")]
    public class HealingSpell : Spell
    {
        public string description;
        public Sprite icon;
        public bool isAOE;
        public bool isHealOverTime;

        public int healingAbility;

        public override void Initialize(GameObject obj)
        {
            //spellPrefab = obj.GetComponent<RangedSpell>();
            //spellPrefab
            //spellPrefab.

        }

        public override void TriggerSpell()
        {
            //spellPrefab.
        }

    }
}
