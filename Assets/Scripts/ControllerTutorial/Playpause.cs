using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ControllerSelection
{
    public class Playpause : MonoBehaviour
    {
        public bool paused;
        public Canvas pauseMenu;
        // Start is called before the first frame update
        void Start()
        {
            paused = false;
            pauseMenu.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                if (!paused)
                {
                    Debug.Log("paused");
                    paused = true;
                    Time.timeScale = 0;
                }
                else
                {
                    paused = false;
                    Time.timeScale = 1;
                }
                pauseMenu.enabled = paused;
            }
        }
    }
}
