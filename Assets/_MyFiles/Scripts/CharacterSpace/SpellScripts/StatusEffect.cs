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
        //This spell will be either a buff or a debuff
        public bool isBuff;

        public GameObject spellPrefab;

        public void BuffMagic()
        {

        }

        public void DeBuffMagic()
        {

        }

        public override void Initialize(GameObject obj)
        {
            if(isBuff)
            {
                //spellPrefab.GetComponent<BuffSpell>(). = obj.GetComponent<BuffSpell>();
            }
            else
            {
                //spellPrefab.GetComponent<DebuffSpell>()
            }
            //spellPrefab
            //spellPrefab.

        }

        public override void TriggerSpell()
        {
            //spellPrefab.
        }
    }
}