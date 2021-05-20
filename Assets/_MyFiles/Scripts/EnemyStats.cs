using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    public float currentMana;
    public float maxMana;

    public float hpRegenAmount;
    public float hpRegenTimer;
    public float manaRegenAmount;
    public float manaRegenTimer;
    public float hpRegenModifier;
    public float manaRegenModifier;

    public bool isDead = false;

    private void Awake()
    {
        currentHp = maxHp;
        currentMana = maxMana;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpRegenAmount = (maxHp * hpRegenModifier) / 100;
        manaRegenAmount = (maxMana * manaRegenModifier) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
            Die();
            //Player will die
        }
        if (currentHp < maxHp)
        {
            currentHp += (hpRegenAmount * Time.deltaTime);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        //AdjustingEnemyHealth();
        AdjustingEnemyMana();
    }

    public void AdjustingEnemyHealth()
    {
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
            Die();
            //Player will die
        }
        if (currentHp < maxHp)
        {
            currentHp += (hpRegenAmount * Time.deltaTime);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void AdjustingEnemyMana()
    {
        if(currentMana < 0)
        {
            currentMana = 0;
        }
        if(currentMana < maxMana)
        {
            currentMana += manaRegenAmount * Time.deltaTime;
        }
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }

    }

    public void ReceiveDamage(float dmg)
    {
        if(currentHp > 0)
        {
            currentHp -= dmg;
            Debug.Log("Did " + dmg + " damage");
            Debug.Log("Enemy current has " + currentHp + " health remaining");
        }
    }



    private void Die()
    {
        Destroy(this.gameObject);
    }

}
