using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera_rig;
    private Vector3 origin;
    void Start()
    {
        origin = transform.position;
        origin.y += 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void move_camera()
    {
        camera_rig.transform.position = origin;
    }
}
