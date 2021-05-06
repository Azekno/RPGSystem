using UnityEngine;

namespace CharacterSpace
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerStats player;

        [SerializeField]
        private Canvas skillCanvas;
        private bool seeCanvas;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("tab"))
            {
                if(skillCanvas)
                {
                    seeCanvas = !seeCanvas;
                    skillCanvas.gameObject.SetActive(seeCanvas);// displays the canvas or not depending on the state of the bool
                }
            }
        }
    }
}
