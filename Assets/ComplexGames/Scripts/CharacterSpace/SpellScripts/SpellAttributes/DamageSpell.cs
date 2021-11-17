using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Damage Spell")]
    public class DamageSpell : Spell
    {
        public GameObject spellObject;
        public string description;
        public Sprite icon;
        public bool isAOE;
        public bool isDamageOverTime;
        public float hitBox;

        /// <summary>
        /// Using the obj which is given, which should be either the player or the enemy casting the spell, you are able to get the position to which the spell object will spawn at before doing it's own thing.
        /// </summary>
        /// <param name="obj"></param>
        public override void TriggerSpell(GameObject obj)
        {
            //spellObject = spellPrefab;
            spellObject.GetComponent<RangedSpell>().hitBox = hitBox;
            spellCost = spellObject.GetComponent<RangedSpell>().rangedSpellCost;

            Vector3 spawnSpellLoc = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

            GameObject clone;
            clone = Instantiate(spellObject, spawnSpellLoc, Quaternion.identity);
            clone.GetComponent<RangedSpell>().player = obj;
            clone.GetComponent<RangedSpell>().target = obj.GetComponent<PlayerStats>().selectedUnit;
        }
    }
}