using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public GameObject positionAnchor;

    void Update()
    {
        gameObject.transform.position = positionAnchor.transform.position;
        gameObject.transform.LookAt(Camera.main.GetComponent<Transform>());
        
    }
}
