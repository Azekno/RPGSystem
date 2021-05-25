using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RangedSpell : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    public float hitBox = 2;
    public float spellSpeed = 30;
    public float spellRange = 2f;

    //public GameObject spellPrefab;
    //public Transform spawnPoint;

    private void Start()
    {
        //targetTransform = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            //Vector3 targetPosition = Vector3.forward;
            this.transform.LookAt(targetPosition);

            //float distance2 = Vector3.Distance(target.transform.position, this.transform.position);
            float distance2 = Vector3.Distance(this.transform.position * spellRange, this.transform.position);

            if (distance2 > hitBox)
            {
                transform.Translate(Vector3.forward * spellSpeed * Time.deltaTime);
            }
            else
            {
                HitTarget();
            }
        }

        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    GameObject spellGo = Instantiate(spellPrefab, spawnPoint.position, spawnPoint.rotation);
        //    spellGo.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * spellSpeed, ForceMode.Impulse);
        //}
    }



    public void HitTarget()
    {
        Destroy(this.gameObject);
    }
}
