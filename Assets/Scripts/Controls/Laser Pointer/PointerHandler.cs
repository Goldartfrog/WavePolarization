using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface PointerHandler : IEventSystemHandler
{
    void HandlePointerStart();
    void HandlePointerEnd();
}