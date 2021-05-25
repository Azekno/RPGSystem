using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Status Effect Spell")]
    public class StatusEffect : Spell
    {
        PlayerStats player;

        public string description;
        public Sprite icon;
        public bool isBuff;
        public bool isDebuff;

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