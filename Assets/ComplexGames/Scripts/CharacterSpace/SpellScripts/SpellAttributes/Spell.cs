using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{

    public abstract class Spell : ScriptableObject
    {
        public new string name = "New Spell";
        public int levelNeeded;
        public int spellPointsNeeded;
        public float cooldownTime = 1f;
        
        public abstract void TriggerSpell(GameObject obj);
    }
}