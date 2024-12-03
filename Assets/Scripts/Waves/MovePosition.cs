using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    public GameObject player;
    public GameObject portalQuiz;
    public GameObject portalTerminal;
    public GameObject OVRCameraRig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onMoveQuiz() {
        Debug.Log("teleporting to terminal");
        player.transform.localPosition = new Vector3(0.89f, -0.089f, 0.297f);
        portalQuiz.SetActive(true);
        portalTerminal.SetActive(false);
    }

    public void onMoveTerminal()
    {
        Debug.Log("teleporting to quiz");
        player.transform.localPosition = new Vector3(1, -0.089f, -4.8f);
        portalQuiz.SetActive(false);
        portalTerminal.SetActive(true);
    }

    public void temp()
    {
        Debug.Log("Registered click");
    }
}
