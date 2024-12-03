using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadVRTutorial : MonoBehaviour
{

    public GameObject player;
    public GameObject tutorial;
    public GameObject main;
    public GameObject OVRCameraRig;

    public void onMoveTutorial()
    {
        player.transform.localPosition = new Vector3(11f,0f,43f);
        player.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
    }

    public void onMoveMain()
    {
        player.transform.localPosition = new Vector3(1, -0.089f, -4.8f);
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
