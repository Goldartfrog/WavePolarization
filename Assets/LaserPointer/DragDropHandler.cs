using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface DragDropHandler : IEventSystemHandler
{
    void HandleTriggerStart(bool isLeft);
    void HandleTriggerEnd(bool isLeft);
}
