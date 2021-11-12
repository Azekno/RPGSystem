using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell/Healing Spell")]
    public class HealingSpellAttribute : Spell
    {
        public GameObject spellPrefab;
        public string description;
        public Sprite icon;
        public bool isAOE;
        public bool isHealOverTime;

        public int healingAmount;


        public void SingleTargetHealing(GameObject obj)
        {
            spellPrefab.GetComponent<HealingSpell>().SingleHealing(obj, healingAmount);
        }

        /*public void AOEHeal()
        {

        }
        public void SingleTargetHealOverTime()
        {

        }
        public void AOEHealOverTime()
        {

        }*/

        public override void TriggerSpell(GameObject obj)
        {
            //spellPrefab.GetComponent<HealingSpell>();

            Vector3 spawnSpellLoc = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

            GameObject clone;
            clone = Instantiate(spellPrefab, spawnSpellLoc, Quaternion.identity);
            //clone.GetComponent<HealingSpell>().player = obj;
            clone.GetComponent<HealingSpell>().target = obj.GetComponent<PlayerStats>().selectedUnit;
            if (!isHealOverTime)
            {
                if (!isAOE)
                {
                    SingleTargetHealing(obj);
                }
                else
                {
                    //AOEHeal();
                }
            }
            else
            {
                /*if (!isAOE)
                {
                    SingleTargetHealOverTime();
                }
                else
                {
                    AOEHealOverTime();
                }*/
            }
        }

    }
}
