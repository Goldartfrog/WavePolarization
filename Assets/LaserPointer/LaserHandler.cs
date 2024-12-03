using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandler : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject leftObject;
    public GameObject lastLeftObject;
    public Vector3 leftVector;
    public LineRenderer ll;
    public LineRenderer lr;
    public GameObject rightHand;
    public GameObject rightObject;
    public GameObject lastRightObject;
    public Vector3 rightVector;

    Vector3 srVector;
    Vector3 lastPositionR, lastPositionL;
    bool activatableL;
    bool activatableR;
    bool leftLaser;
    bool rightLaser;
    Vector3 origScale;
    Vector3 zVector = new Vector3(0,0,1);

    enum Hand { Right, Left }

    // Start is called before the first frame update
    void Start()
    {
        // Set state of all booleans to their default value
        activatableL = true;
        activatableR = true;
        leftLaser = false;
        ll.enabled = false; // Makes sure the line isn't being rendered on startup
        ll.useWorldSpace = true; 
        lr.enabled = false; // Makes sure the line isn't being rendered on startup
        lr.useWorldSpace = true;
        rightLaser = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If object is dropped when being moved, maintain last velocity
        if (!leftObject && lastLeftObject)
        {
            lastLeftObject.GetComponent<Rigidbody>().velocity = (lastLeftObject.transform.position - lastPositionL)*50;
            lastLeftObject.GetComponent<LaserGrabbable>().resetGrabbed();
            lastLeftObject = null;
        }
        if (!rightObject && lastRightObject)
        {
            lastRightObject.GetComponent<Rigidbody>().velocity = (lastRightObject.transform.position - lastPositionR)*50;
            lastRightObject.GetComponent<LaserGrabbable>().resetGrabbed();
            lastRightObject = null;
        }
        // Detect whether the triggers on the side of the controllers are being pressed and sets the correct laser(s) to be active
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            rightLaser = true;
            lr.enabled = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            rightLaser = false;
            lr.enabled = false;
            activatableR = true;
            rightObject = null;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            leftLaser = true;
            ll.enabled = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            leftLaser = false;
            ll.enabled = false;
            activatableL = true;
            leftObject = null;
        }
        // If the index trigger is released allow it to activate objects again
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && !activatableR)
        {
            activatableR = true;
            rightObject = null;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && !activatableL)
        {
            activatableL = true;
            leftObject = null;
        }

        // Handle objects grabbed with both hands
        if (rightObject != null && leftObject != null && rightObject.Equals(leftObject))
        {
            Vector3 newSRVector = rightHand.transform.position - leftHand.transform.position;

            bool zButtonDown = 
                (OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.One));
            bool yButtonDown = 
                (OVRInput.Get(OVRInput.Button.Four) || OVRInput.Get(OVRInput.Button.Two));

            if (rightObject.GetComponent<LaserGrabbable>().scalable)
            {
                float scalingFactor = newSRVector.magnitude / srVector.magnitude;
                rightObject.transform.localScale = scalingFactor * origScale;
            }
            if (rightObject.GetComponent<LaserGrabbable>().rotatable && zButtonDown)
            {
                Vector3 Rotation = .25f*Vector3.Scale(
                    Quaternion.FromToRotation(srVector, newSRVector).eulerAngles, 
                    zVector);
                rightObject.transform.rotation *= Quaternion.Euler(Rotation);
            }
            if (rightObject.GetComponent<LaserGrabbable>().rotatable && yButtonDown)
            {
                Vector3 Rotation = .25f * Vector3.Scale(
                    Quaternion.FromToRotation(srVector, newSRVector).eulerAngles, 
                    Vector3.up);
                rightObject.transform.rotation *= Quaternion.Euler(Rotation);
            }
            leftVector = 
                leftHand.transform.InverseTransformPoint(leftObject.transform.position);
            rightVector = 
                rightHand.transform.InverseTransformPoint(rightObject.transform.position);
        } // Handle Objects grabbed by a single hand (only if there is a grabbed object)
        else if(rightObject != null || leftObject!= null)
        {
            if (rightObject != null && rightObject.GetComponent<LaserGrabbable>().movable)
            {
                rightObject.GetComponent<LaserGrabbable>().setGrabbed();
                lastRightObject = rightObject;
                Vector3 newVector = rightHand.transform.TransformPoint(rightVector);
                lastPositionR = rightObject.transform.position;
                rightObject.transform.position = newVector;
            }
            if (leftObject != null && leftObject.GetComponent<LaserGrabbable>().movable)
            {
                leftObject.GetComponent<LaserGrabbable>().setGrabbed();
                lastLeftObject = leftObject;
                Vector3 newVector = leftHand.transform.TransformPoint(leftVector);
                lastPositionL = leftObject.transform.position;
                leftObject.transform.position = newVector;
            }
        }
        
        

        RaycastHit rayHit = new RaycastHit();

        // Raycasting detection for the right controller
        if(rightLaser)
        {
            Physics.Raycast(rightHand.transform.position, 
                rightHand.transform.forward, 
                out rayHit, 10, -1);
            //Debug.Log(rayHit.collider);
            if (rayHit.transform != null)
            {
                GameObject rayObject = rayHit.transform.gameObject;

                // Set points for the line to be drawn
                Vector3[] positions = { rightHand.transform.position, rayHit.point };
                lr.SetPositions(positions);
                //Debug.Log(rayObject);
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && activatableR)
                {
                    // Ensures that an object cannot be activated more than once on a single button press
                    activatableR = false;
                    HandleHit(rayObject, rayHit.point, Hand.Right);
                }
            }
            else
            {
                // If there is no raycast finds no objects, draw a straight line from the correct hand
                Vector3[] positions = { rightHand.transform.position,
                    rightHand.transform.forward * 10 };
                lr.SetPositions(positions);
            }
        }

        // Raycasting detection for the left controller
        if (leftLaser)
        {
            Physics.Raycast(leftHand.transform.position, 
                leftHand.transform.forward, 
                out rayHit, 10, -1);
            //Debug.Log(rayHit.collider);
            if (rayHit.transform != null)
            {
                GameObject rayObject = rayHit.transform.gameObject;
                // Set points for the line to be drawn
                Vector3[] positions = { leftHand.transform.position, rayHit.point };
                ll.SetPositions(positions);
                //Debug.Log(rayObject);
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && activatableL)
                {
                    // Ensures that an object cannot be activated more than once on a single button press
                    activatableL = false;
                    HandleHit(rayObject, rayHit.point, Hand.Left);
                }
            }
            else
            {
                // If there is no raycast finds no objects, draw a straight line from the correct hand
                Vector3[] positions = { leftHand.transform.position,
                    leftHand.transform.forward * 10 };
                ll.SetPositions(positions);
            }
        }
    }

    void HandleHit(GameObject rayObject, Vector3 hitPosition, Hand h)
    {
        // If the object is tagged as UI, Invoke the onClick event
        if (rayObject.CompareTag("UI"))
        {
            if (rayObject.GetComponent<UnityEngine.UI.Button>() != null)
            {
                rayObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            else if (rayObject.GetComponent<UnityEngine.UI.Toggle>() != null)
            {
                rayObject.GetComponent<UnityEngine.UI.Toggle>().isOn = 
                    !rayObject.GetComponent<UnityEngine.UI.Toggle>().isOn;
            }
            else if (rayObject.GetComponent<UnityEngine.UI.Slider>() != null)
            {
                Vector3 localHit = rayObject.transform.InverseTransformPoint(hitPosition);
                float newValue = localHit.x / 
                    (rayObject.GetComponent<RectTransform>().rect.width) + .5f;
                rayObject.GetComponent<UnityEngine.UI.Slider>().value = newValue;
                Debug.Log(localHit);
                Debug.Log(newValue);
                Debug.Log(rayObject.GetComponent<RectTransform>().rect.width);
            }
            
        }
        else if (rayObject.GetComponent<LaserGrabbable>() != null)
        {
            if (h == Hand.Left)
            {
                leftObject = rayObject;
                leftVector = 
                    leftHand.transform.InverseTransformPoint(rayObject.transform.position);
                if (rightObject!=null && leftObject.Equals(rightObject))
                {
                    srVector = rightHand.transform.position - leftHand.transform.position;
                    origScale = rightObject.transform.localScale;
                }
            }
            else
            {
                rightObject = rayObject;
                rightVector = 
                    rightHand.transform.InverseTransformPoint(rayObject.transform.position);
                if (leftObject != null && rightObject.Equals(rightObject))
                {
                    srVector = rightHand.transform.position - leftHand.transform.position;
                    origScale = rightObject.transform.localScale;
                }
            }
        }
    }
}

// Handles all two-handed transformations

// Handles all single-handed transformations
