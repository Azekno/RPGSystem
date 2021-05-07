using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Spell")]
    public class Spells : ScriptableObject
    {
        public string description;
        public Sprite icon;
        public int levelNeeded;
        public int spellPointsNeeded;

        public List<CharacterAttributes> affectedAttributes = new List<CharacterAttributes>();
    }
}