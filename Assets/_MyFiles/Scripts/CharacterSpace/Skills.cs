using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{ 
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int SkillPointsNeeded;

        public List<CharacterAttributes> AffectedSkills = new List<CharacterAttributes>();
    }
}
