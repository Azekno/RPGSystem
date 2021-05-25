using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour {


    public GameObject spellPrefab;
    public float throwForce;
    public Transform spawnPoint;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject spellGo = Instantiate(spellPrefab, spawnPoint.position, spawnPoint.rotation);
            spellGo.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * throwForce, ForceMode.Impulse);
        }
    }

}
