using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSelection;

public class KeepInScene : MonoBehaviour
{
    [Header("Bound Transforms")]
    [SerializeField] private Transform LowerYBound;
    [SerializeField] private Transform UpperYBound;

    [SerializeField] private Transform LowerXBound, UpperXBound;
    [SerializeField] private Transform LowerZBound, UpperZBound;

    [Header("Anchors")]
    [SerializeField] private Transform OriginAnchor;

    [Header("Grabbable Objects")]
    [SerializeField] private Draggable KeepInBounds;

    void Update()
    {
        if(!IsInBounds(KeepInBounds.transform.position))
        {
            if(KeepInBounds.isHeld)
            {
                bool heldByLeft = KeepInBounds.heldByLeft;
                KeepInBounds.FreeHold();
            }

            KeepInBounds.transform.position = OriginAnchor.transform.position;
        }    
    }

    private bool IsInBounds(Vector3 Position)
    {
        if (Position.y > UpperYBound.position.y || Position.y < LowerYBound.position.y ||
            Position.x > UpperXBound.position.x || Position.x < LowerXBound.position.x ||
            Position.z > UpperZBound.position.z || Position.z < LowerZBound.position.z)
            return false;
        else
            return true;
    }
}
