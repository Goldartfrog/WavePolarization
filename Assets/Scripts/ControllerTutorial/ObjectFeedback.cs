using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerSelection
{
    public class ObjectFeedback : MonoBehaviour, PointerHandler
    {

        public bool hit;

        private Renderer rend;
        [Header("Material")]
        [SerializeField]
        private Color color;

        public Draggable draggableCube;

        public GameObject pauseMenu;

        // Start is called before the first frame update
        void Start()
        {
            draggableCube = gameObject.GetComponent<Draggable>();
            rend = gameObject.GetComponent<Renderer>();
            color = rend.material.color;
        }

        // Update is called once per frame
        void Update()
        {
            // check if the game is in play state
            if (!pauseMenu.GetComponent<Playpause>().paused)
            {
                rend.material.color = color;
                // remove gravity when grabbing object
                gameObject.GetComponent<Rigidbody>().useGravity = !draggableCube.isHeld;
                // change velocities to 0 so that the object doesn't behave weirdly when deselected
                if (draggableCube.isHeld)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
                // provide color feedback when pointing
                if (hit || draggableCube.isHeld)
                {
                    rend.material.color = Color.yellow;
                }
            }
        }

        //Handler for raycast pointing to the object
        void PointerHandler.HandlePointerStart()
        {
            hit = true;
        }

        //Handler for raycast not pointing to the object
        void PointerHandler.HandlePointerEnd()
        {
            hit = false;
        }
    }
}
