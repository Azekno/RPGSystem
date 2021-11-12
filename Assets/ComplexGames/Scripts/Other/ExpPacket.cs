using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    /// <summary>
    /// The scripts for exp packets. The will disable when collected before reactivating when the timer reaches it's limit. Exp value that the player receives can be set.
    /// </summary>
    public class ExpPacket : MonoBehaviour
    {
        public PlayerStats playerStats;
        public int expGiven = 0;

        private Transform childObj;
        
        public bool respawnExpPacket;
        public float timer;
        public float timerLimit = 5;
        

        void Start()
        {
            childObj = transform.GetChild(0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(childObj.gameObject.activeSelf)
            {
                if(other.gameObject.CompareTag("Player"))
                {
                    playerStats.exp += expGiven;
                    DeactiveObject();
                }
            }
        }

        void ActivateObject()
        {
            childObj.gameObject.SetActive(true);
        }

        public void DeactiveObject()
        {
            childObj.gameObject.SetActive(false);
        }

        void Update()
        {
            transform.Rotate(transform.position, 45 * Time.deltaTime);
            if (respawnExpPacket)
            {
                if (!childObj.gameObject.activeSelf)
                {
                    if (timer >= timerLimit)
                    {
                        ActivateObject();
                        timer = 0;
                    }
                    timer += Time.deltaTime;
                }
            }
        }
    }
}
