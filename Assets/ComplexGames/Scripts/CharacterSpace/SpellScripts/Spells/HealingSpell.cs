using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSpace;

public class HealingSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    //Stat intelligenceStat;
    
    public float spellSpeed = 30;
    public float healRange;
    public float baseHealAmount = 1;
    private float currentHealAmount;
    //public float healCooldown = 5f;

    /*private void Awake()
    {
        //currentHealAmount = baseHealAmount + player.GetComponent<PlayerStats>().GetStatValue(intelligenceStat);
        *//*foreach(var item in player.GetComponent<PlayerStats>().statDictionary.Keys)
        {
            if(item.name == "INT")
            {
                intelligenceStat = item;
            }
        }*//*
    }*/

    public void SingleHealing(GameObject targetObj, int healAmount)
    {
        target = targetObj;
        currentHealAmount = healAmount;
        if (target != null)
        {
            Debug.Log("Heal by " + currentHealAmount);
            target.GetComponent<PlayerStats>().currentHealth += currentHealAmount;
            if (target.GetComponent<PlayerStats>().currentHealth > target.GetComponent<PlayerStats>().maxHealth)
            {
                target.GetComponent<PlayerStats>().currentHealth = target.GetComponent<PlayerStats>().maxHealth;
            }
        }
        //Destroy(gameObject, 2f);
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
