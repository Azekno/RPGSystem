using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSpace
{
    public class StatUIDisplay : MonoBehaviour
    {
        public PlayerStats player;

        public Stat stat;
        public Text unallocatedStats;
        //Get the UI components
        public Text statName;
        public Text currStatValue;




        // Start is called before the first frame update
        void Start()
        {
            currStatValue.text = player.GetStatValue(stat).ToString();

            unallocatedStats.text = player.unassignedStatPoints.ToString();
            //player.onStatChange += ReactToChange;

        }

        ///Increases the selected stats value in the dictionary before showing the change in the menu.
        ///The unassigned stat points value is reduced and the statIncreased bool becomes true
        public void IncreaseStat()
        {
            if (player.unassignedStatPoints != 0)
            {
                player.tempDictionary[stat] += 1;
                currStatValue.text = player.GetStatValue(stat).ToString();

                player.unassignedStatPoints -= 1;
                unallocatedStats.text = player.unassignedStatPoints.ToString();
                player.statIncreased = true;
            }
        }
    }
}