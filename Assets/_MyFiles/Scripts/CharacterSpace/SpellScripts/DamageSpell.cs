using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Damage Spell")]
    public class DamageSpell : Spell
    {
        public EnemyStats enemyStatsScript;
        public PlayerStats playerStats;

        public string description;
        public Sprite icon;
        public bool isAOE;
        public bool isDamageOverTime;

        public float baseDamage;
        private float currentDamage;
        public float spellRange = 10f;

        public GameObject spellPrefab;

        void RangedSpell()
        {
            currentDamage = (baseDamage * playerStats.currentMagicAttackPower) / 2;
            Vector3 spawnSpellLoc = new Vector3(playerStats.transform.position.x, playerStats.transform.position.y, playerStats.transform.position.z);
        
            GameObject clone;
            clone = Instantiate(spellPrefab, spawnSpellLoc, Quaternion.identity);
            clone.GetComponent<RangedSpell>().target = playerStats.selectedUnit;
            enemyStatsScript.ReceiveDamage(currentDamage);
        }

        public override void Initialize(GameObject obj)
        {
            //spellPrefab = obj.GetComponent<RangedSpell>();
            //spellPrefab.spellRange = spellRange;

            //spellPrefab = obj;
            //spellPrefab.
        }

        public override void TriggerSpell()
        {
            //spellPrefab.
        }
    }
}