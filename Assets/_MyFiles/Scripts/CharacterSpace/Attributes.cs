using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Attribute")]

    public class Attributes : ScriptableObject
    {
        //public string name;
        public string description;
        public Sprite thumbnail;
    }
}