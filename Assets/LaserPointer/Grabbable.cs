using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private GameObject pointingAtL, pointingAtR;

    public void OnSelectedL(Transform t)
    {
        if (pointingAtR == t.gameObject) return;
        pointingAtL = t.gameObject;
        ExecuteEvents.Execute(t.gameObject, null, (DragDropHandler handler, BaseEventData data) => handler.HandleTriggerStart(true));
    }

    public void OnSelectedR(Transform t)
    {
        if (pointingAtL == t.gameObject) return;
        pointingAtR = t.gameObject;
        ExecuteEvents.Execute(t.gameObject, null, (DragDropHandler handler, BaseEventData data) => handler.HandleTriggerStart(false));
    }

    public void OnDeselectedL(Transform t)
    {
        if (pointingAtL == null) return;
        ExecuteEvents.Execute(pointingAtL, null, (DragDropHandler handler, BaseEventData data) => handler.HandleTriggerEnd(true));
        pointingAtL = null;
    }

    public void OnDeselectedR(Transform t)
    {
        if (pointingAtR == null) return;
        ExecuteEvents.Execute(pointingAtR, null, (DragDropHandler handler, BaseEventData data) => handler.HandleTriggerEnd(false));
        pointingAtR = null;
    }
}
