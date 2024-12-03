using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RawInteraction : MonoBehaviour {

    public void OnHoverEnter(Transform t) { }

    public void OnHoverExit(Transform t) { }

    public void OnHover(Transform t) { }

    public void OnSelected(Transform t)
    {
        Debug.Log(t.gameObject.name);
    }
}
