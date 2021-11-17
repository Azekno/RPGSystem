using UnityEngine;
using CharacterSpace;

public class EnemyStats : MonoBehaviour
{
    UIHealthBar healthBar;
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
    public bool isMultiplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        currentMana = maxMana;
        healthBar = GetComponentInChildren<UIHealthBar>();
        healthBar.SetMax(maxHp);
        hpRegenAmount = (maxHp * hpRegenModifier) / 100;
        manaRegenAmount = (maxMana * manaRegenModifier) / 100;
    }

    // Update is called once per frame
    void Update()
    { 
        if (currentHp < maxHp)
        {
            currentHp += hpRegenAmount * Time.deltaTime;
            healthBar.SetUIBarPercentage(currentHp);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }


    public void ReceiveDamage(float amount)
    {
        currentHp -= amount;
        healthBar.SetUIBarPercentage(currentHp);
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        healthBar.gameObject.SetActive(false);
    }
}
