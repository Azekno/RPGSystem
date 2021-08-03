using CharacterSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    private EnemyStats enemy = null;
    public float hitBox = 5;
    public float spellSpeed = 30;
    public float spellRange;
    public float baseSpellDamage = 1;
    public float currentSpellDamage;


    private void Start()
    {
        enemy = target.GetComponent<EnemyStats>();
        currentSpellDamage = baseSpellDamage;
    }

    // Update is called once per frame
    void Update()
    {
        ///If the target isn't null, the spell will do a distacne check and begin 'chasing' the target before entering the hitbox which will call the hitTarget function
        if(target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            this.transform.LookAt(targetPosition);

            float distance2 = Vector3.Distance(target.transform.position, this.transform.position);

            if (distance2 > hitBox)
            {
                transform.Translate(Vector3.forward * spellSpeed * Time.deltaTime);
            }
            else
            {
                HitTarget();
            }
        }
    }
    
    /// <summary>
    /// Decides the damage of the spell
    /// </summary>
    void SpellDamage()
    {
        currentSpellDamage = baseSpellDamage * (player.GetComponent<PlayerStats>().currentMagicAttackPower) / 4;
        enemy.ReceiveDamage(currentSpellDamage);
    }

    /// <summary>
    /// What happens when the spell hits the target
    /// </summary>
    public void HitTarget()
    {
        SpellDamage();
        Destroy(gameObject);
        target = null;
    }
}
