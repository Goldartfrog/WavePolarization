using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrabbable : MonoBehaviour
{
    public bool movable;
    public bool rotatable;
    public bool scalable;

    public bool isGrabbed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGrabbed()
    {
        isGrabbed = true;
    }

    public void resetGrabbed()
    {
        isGrabbed = false;
    }
}
