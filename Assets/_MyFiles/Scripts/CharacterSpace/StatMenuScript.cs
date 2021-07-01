using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterSpace
{
    public class StatMenuScript : MonoBehaviour
    {
        public PlayerStats playerStats;
        //The rate at which unassigned skill points are removed when a stat is selected.
        public int statConsumption = 1;
        //The rate at which a stat is increased.
        public int statIncrease = 1;

        //The list of the stats the 
        public List<Stat> currentStats;

        // Start is called before the first frame update
        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            currentStats = playerStats.stats;
        }

        public void IncreaseSelectedStat()
        {
            playerStats.unassignedStatPoints -= statConsumption;
            //playerStats.stats.
        }
    }
}