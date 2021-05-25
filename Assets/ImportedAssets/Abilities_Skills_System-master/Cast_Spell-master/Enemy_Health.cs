using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

  
    public void TakeDamage(float _amnt, SpellTest.SpellType _spellType)
    {
        Debug.Log("Enemy took " + _amnt.ToString() + " of damage");

        if(_spellType == SpellTest.SpellType.Ice)
        {
            Debug.Log("spell type is Ice");
        }

        if (_spellType == SpellTest.SpellType.Fire)
        {
            Debug.Log("spell type is Fire");
        }

        if (_spellType == SpellTest.SpellType.Posion)
        {
            Debug.Log("spell type is Posion");
        }

    }

}
