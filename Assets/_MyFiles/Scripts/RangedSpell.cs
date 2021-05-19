using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    public float hitBox = 2;
    public float spellSpeed = 30;

    private void Start()
    {
        targetTransform = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            Vector3 targetPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
            this.transform.LookAt(targetPosition);

            //float distance2 = Vector3.Distance(target.transform.position, this.transform.position);
            float distance2 = Vector3.Distance(targetTransform.position, this.transform.position);

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

    void HitTarget()
    {
        Destroy(this.gameObject);
    }
}
