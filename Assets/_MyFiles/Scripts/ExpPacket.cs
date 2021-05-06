using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSpace
{
    public class ExpPacket : MonoBehaviour
    {
        //ParentScript parentScript;
        public PlayerStats playerStats;
        public int expGiven = 0;

        private Transform childObj;
        public float timer;
        public float timerLimit = 5;
        //private bool seeExpPacket;

        void Start()
        {
            childObj = transform.GetChild(0);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (childObj.gameObject.activeSelf)
            {

                if (collision.gameObject.CompareTag("Player"))
                {
                    playerStats.exp += expGiven;
                }
            }
            DeactiveObject();
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
