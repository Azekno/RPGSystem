using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{

    public abstract class Spell : ScriptableObject
    {
        public string spellName = "";
        public GameObject spellPrefab = null;
        public Texture2D spellIcon = null;
        public float spellCost = 0;
        public abstract void TriggerSpell(GameObject obj);

    }
}