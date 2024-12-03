using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerSelection
{
    public class Rotatable : MonoBehaviour, DragDropHandler
    {
        [HideInInspector]
        public bool isHeld;
        private Pointer pointer;
        //
        private float length;
        public float smooth = 2.0F;
        public float tiltAngle = 5.0F;
        public GameObject wire1;

        void Start()
        {
            isHeld = false;
            pointer = GameObject.Find("Pointer").GetComponent<Pointer>();
        }

        void Update()
        {
            if (isHeld)
            {
                Vector3 direction = Vector3.Normalize(pointer.endPosition - pointer.origin);
                Ray ray = new Ray(pointer.origin, direction);
                transform.position = ray.GetPoint(length);
                ButtonMovementForwardAndBack();
                ButtonMovementRotate();
            }
        }

        void ButtonMovementForwardAndBack()
        {
            bool a_button = Input.GetButton("Oculus_CrossPlatform_Button2");
            bool b_button = Input.GetButton("Oculus_CrossPlatform_Button1");

            if (a_button)
            {
                if (length >= 0.15f)
                    length -= Time.deltaTime;
            }
            if (b_button && length < 8.0f)
            {
                if (length <= 8.0f)
                    length += Time.deltaTime;
            }
        }

        void ButtonMovementRotate()
        {

            bool x_button = Input.GetButton("Oculus_CrossPlatform_Button4");
            bool y_button = Input.GetButton("Oculus_CrossPlatform_Button3");

            if (x_button)
            {
                wire1.transform.Rotate(Vector3.forward * 2);
            }

            if (y_button)
            {
                wire1.transform.Rotate(Vector3.back * 2);
            }
        }

        void DragDropHandler.HandleTriggerStart(bool isLeft)
        {
            isHeld = true;
            length = Vector3.Magnitude(transform.position - pointer.origin);
        }

        void DragDropHandler.HandleTriggerEnd(bool isLeft)
        {
            isHeld = false;
        }
    }
}
