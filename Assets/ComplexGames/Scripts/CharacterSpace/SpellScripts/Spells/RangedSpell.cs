using CharacterSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    private EnemyStats enemy = null;
    public float hitBox = 5f;
    public float spellSpeed = 30f;
    public float spellRange;
    public float baseSpellDamage = 1f;
    public float currentSpellDamage;
    public float rangedSpellCost = 10f;


    private void Start()
    {
        enemy = target.GetComponent<EnemyStats>();
        currentSpellDamage = baseSpellDamage;
        Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        ///If the target isn't null, the spell will do a distacne check and begin 'chasing' the target before entering the hitbox which will call the hitTarget function
        if(target != null && target != player)
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
