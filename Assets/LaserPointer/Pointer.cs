using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerSelection
{
    public enum Hand
    {
        Left, Right
    };

    public class Pointer : MonoBehaviour
    {
        public static Pointer LPointer, RPointer;
        [Header("Maximum Length")]
        [Tooltip("Maximum length that ray should be drawn")]
        public float maxPointerLength = 5.0f;

        [Header("Tracking space")]
        public Transform trackingSpace = null;

        [Header("Visual Elements")]
        [Tooltip("Line Renderer used to draw selection ray.")]
        public LineRenderer linePointer = null;

        public Hand hand;

        [HideInInspector]
        public Vector3 origin;

        [HideInInspector]
        public Vector3 endPosition;

        [HideInInspector]
        public OVRInput.Controller activeController = OVRInput.Controller.None;

        public Transform Anchor;
        
        [SerializeField]
        private bool linePointerGrab = false;

        void Awake()
        {
            if (hand == Hand.Right) RPointer = this;
            else LPointer = this;
        }

        private RaycastHit CreateRaycast(float length, Ray ray)
        {
            int layerMask = ~(1 << 2);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, maxPointerLength, layerMask,
                QueryTriggerInteraction.Ignore);
            return hit;
        }

        public void SetPointer(Ray ray)
        {
            if (linePointer != null)
            {
                // Draw the pointer up to an object that is hit.
                RaycastHit hit = CreateRaycast(maxPointerLength, ray);

                float targetLength = maxPointerLength;
                endPosition = ray.origin + ray.direction * targetLength;

                if (hit.collider != null)
                {
                    endPosition = hit.point;
                }

                linePointer.SetPosition(0, ray.origin);
                linePointer.SetPosition(1, endPosition);

                origin = ray.origin;
                this.transform.position = origin;
            }
        }

        public bool getIsGrabbing()
        {
            return linePointerGrab;
        }

        void Update()
        {
            if (hand == Hand.Right)
            {
                linePointer.enabled = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
                activeController = (OVRInput.Controller.RTouch);
            }
            else if (hand == Hand.Left)
            {
                linePointer.enabled = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
                activeController = (OVRInput.Controller.LTouch);
            }

            linePointerGrab = linePointer.enabled && ((OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) &&
                hand == Hand.Left) ||
                (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) &&
                hand == Hand.Right));

            Ray selectionRay = OVRInputHelpers.GetSelectionRay(activeController, trackingSpace);
            SetPointer(selectionRay);
            
        }
    }
}
