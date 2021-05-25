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
        //public float activeTime;
        public KeyCode key;

        public List<CharacterAttributes> affectedAttributes = new List<CharacterAttributes>();
        
        public abstract void Initialize(GameObject obj);
        public abstract void TriggerSpell();
    }
}