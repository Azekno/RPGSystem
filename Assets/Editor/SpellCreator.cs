using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CharacterSpace;

public class SpellCreator : EditorWindow
{
    [MenuItem("Spell Maker/Spell Wizard")]
    static void Init()
    {
        SpellCreator spellWindow = (SpellCreator)CreateInstance(typeof(SpellCreator));
        spellWindow.Show();
    }

    Spell tempSpell = null;
    SpellManager spellManager = null;

    void OnGui()
    {
        if(spellManager = null)
        {
            spellManager = GameObject.Find("SpellManager").GetComponent<SpellManager>();
        }

        if(tempSpell)
        {
            tempSpell.spellName = EditorGUILayout.TextField("Spell Name", tempSpell.spellName);
            tempSpell.spellPrefab = (GameObject)EditorGUILayout.ObjectField("Spell Prefab", tempSpell.spellPrefab, typeof(GameObject), false);
        }
    }
}
