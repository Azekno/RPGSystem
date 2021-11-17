using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSpace;

public class HealingSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    
    public float spellSpeed = 30;
    public float healRange;
    public float baseHealAmount = 1;
    private float currentHealAmount;


    public void SingleHealing(GameObject targetObj, int healAmount)
    {
        target = targetObj;
        currentHealAmount = healAmount;
        if (target != null)
        {
            target.GetComponent<PlayerStats>().currentHealth += currentHealAmount;
            if (target.GetComponent<PlayerStats>().currentHealth > target.GetComponent<PlayerStats>().maxHealth)
            {
                target.GetComponent<PlayerStats>().currentHealth = target.GetComponent<PlayerStats>().maxHealth;
            }
        }
    }

    public void AOEHeal()
    {
        target.GetComponent<PlayerStats>().currentHealth += currentHealAmount;
    }
    public void SingleTargetHealOverTime()
    {
        target.GetComponent<PlayerStats>().currentHealth += currentHealAmount;
    }
    public void AOEHealOverTime()
    {
        target.GetComponent<PlayerStats>().currentHealth += currentHealAmount;
    }
}
