using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Stat")]

    public abstract class Stat : ScriptableObject
    {
        public new string name = "New Stat";
        public string description;
        public Sprite thumbnail;

        public bool affectsHealth = false;
        public bool affectsMana = false;


        public abstract void Initialize(GameObject obj);
    }
}