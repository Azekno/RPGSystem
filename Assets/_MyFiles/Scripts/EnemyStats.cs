using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float currentHp;
    public float maxHp;

    public bool isDead = false;

    private void Awake()
    {
        currentHp = maxHp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp <= 0)
        {
            isDead = true;
            currentHp = 0;
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
}
