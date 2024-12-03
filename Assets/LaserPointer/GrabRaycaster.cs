using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ControllerSelection
{
    public class GrabRaycaster : MonoBehaviour
    {
        [System.Serializable]
        public class SelectionCallback : UnityEvent<Transform> { }

        [Header("Tracking space")]
        public Transform trackingSpace = null;

        [Header("Pointers")]
        [Tooltip("Left Hand Pointer")]
        public Pointer lPointer;
        [Tooltip("Right Hand Pointer")]
        public Pointer rPointer;

        [Header("Raycast Support")]
        [Tooltip("Layers to exclude from raycast (Normally should be set to IgnoreRaycast)")]
        public LayerMask excludeLayers;
        [Tooltip("Maximum raycast distance")]
        public float raycastDistance = 500;

        [Header("Selection Callbacks")]
        public SelectionCallback onLeftSelect;
        public SelectionCallback onLeftDeselect;
        public SelectionCallback onRightSelect;
        public SelectionCallback onRightDeselect;

        RaycastHit hitL, hitR;
        Ray leftRay, rightRay;

        void Awake()
        {
            if (trackingSpace == null)
            {
                trackingSpace = OVRInputHelpers.FindTrackingSpace();
            }
        }

        // Update is called once per frame
        void Update()
        {
            leftRay = OVRInputHelpers.GetSelectionRay(OVRInput.Controller.LTouch, trackingSpace);
            rightRay = OVRInputHelpers.GetSelectionRay(OVRInput.Controller.RTouch, trackingSpace);

            if (Physics.Raycast(leftRay, out hitL, raycastDistance, ~excludeLayers))
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                {
                    if (onLeftSelect != null)
                    {
                        onLeftSelect.Invoke(hitL.transform);
                    }
                    else
                    {
                        Debug.Log("ERROR: Assign onLeftSelect!");
                    }
                }
                else if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                {
                    if (onLeftDeselect != null)
                    {
                        onLeftDeselect.Invoke(hitL.transform);
                    }
                    else
                    {
                        Debug.Log("ERROR: Assign onLeftDeselect!");
                    }
                }

            }

            if (Physics.Raycast(rightRay, out hitR, raycastDistance, ~excludeLayers))
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                {
                    if (onRightSelect != null)
                    {
                        onRightSelect.Invoke(hitR.transform);
                    }
                    else
                    {
                        Debug.Log("ERROR: Assign onRightSelect!");
                    }
                }
                else if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                {
                    if (onRightDeselect != null)
                    {
                        onRightDeselect.Invoke(hitR.transform);
                    }
                    else
                    {
                        Debug.Log("ERROR: Assign onRightDeselect!");
                    }
                }
            }
        }
    }
}