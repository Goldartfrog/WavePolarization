using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

namespace ControllerSelection
{
    public class Draggable : MonoBehaviour, DragDropHandler
    {
        public bool isHeld;

        [HideInInspector]
        private Pointer PointerL, PointerR;

        public Transform AnchorL, AnchorR;

        private float lengthOffset = 0;

        private float length;
        bool isUI = false;

        bool triggeredOnce = false;

        public bool heldByLeft = false;

        [Header("Active Anchor & Pointer")]
        [SerializeField]
        private Pointer pointer;

        [SerializeField]
        private Transform Anchor;


        void Awake()
        {
            isHeld = false;
            GameObject[] PointerObj = GameObject.FindGameObjectsWithTag("Pointer");
            foreach(GameObject pointerParent in PointerObj)
            {
                Pointer pTemp = pointerParent.GetComponent<Pointer>();
                if (pTemp == null) Debug.Log("Pointer not found!");
                if (pTemp.hand == Hand.Left)
                {
                    PointerL = pTemp;
                    AnchorL = pTemp.Anchor;
                }
                else
                {
                    PointerR = pTemp;
                    AnchorR = pTemp.Anchor;
                }
            }
            if (gameObject.tag == "UI") isUI = true;
        }

        void Update()
        {
            if (isHeld && !isUI)
            {
                float push = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;
                if (push > 0.1f || push < -0.1f)
                {
                    float d_magnitude = (transform.position - pointer.origin).magnitude;
                    if ((d_magnitude > 0.1f && push < 0f) || (d_magnitude < 2 && push > 0f))
                    {
                        lengthOffset += Time.deltaTime * push;
                    }
                }
                Vector3 Rotation = new Vector3();
                float rotate = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).x;
                if (rotate > 0.1f || rotate < -0.1f)
                {
                    
                    Rotation = Vector3.up * (rotate * -180 * Time.deltaTime);
                }

                Vector3 direction = Vector3.Normalize(pointer.endPosition - pointer.origin);
                Ray ray = new Ray(pointer.origin, direction);
                Vector3 Slide = ray.direction * lengthOffset;
                transform.position = Anchor.position + Slide;
                transform.Rotate(Rotation);
                Debug.LogWarning(transform.position);
            }
            else if (isUI && isHeld && !triggeredOnce)
            {
                triggeredOnce = true;
                GetComponent<Button>().onClick.Invoke();
            }
        }

        void ButtonMovement()
        {
            bool a_button = Input.GetButton("Oculus_CrossPlatform_Button2");
            bool b_button = Input.GetButton("Oculus_CrossPlatform_Button1");
            bool x_button = Input.GetButton("Oculus_CrossPlatform_Button4");
            bool y_button = Input.GetButton("Oculus_CrossPlatform_Button3");

            if (a_button || x_button)
            {
                if (length >= 0.15f)
                    lengthOffset -= Time.deltaTime;
            }
            if (b_button || y_button && length < 8.0f)
            {
                if (length <= 8.0f)
                    lengthOffset += Time.deltaTime;
            }
        }

        void DragDropHandler.HandleTriggerStart(bool isLeft)
        {
            if (isHeld) return;
            if (isLeft) { Anchor = AnchorL; pointer = PointerL; }
            else { Anchor = AnchorR; pointer = PointerR; }
            heldByLeft = isLeft;
            Anchor.position = transform.position;
            Debug.LogWarning(gameObject.name);
            isHeld = true;
            length = Vector3.Magnitude(transform.position - pointer.origin);
            lengthOffset = 0;
        }

        void DragDropHandler.HandleTriggerEnd(bool isLeft)
        {
            if (!isHeld) return;
            if ((!heldByLeft && isLeft) || (heldByLeft && !isLeft)) return;
            triggeredOnce = false;
            Anchor = null;
            isHeld = false;
            lengthOffset = 0;
        }

        public void FreeHold()
        {
            triggeredOnce = false;
            Anchor = null;
            isHeld = false;
            lengthOffset = 0;
        }
    }
}
