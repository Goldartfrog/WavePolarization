using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEffect : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 2);
    }
}
