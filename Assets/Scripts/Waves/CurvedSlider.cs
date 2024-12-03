using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//======================================
// CurvedSlider.cs (Version 1)
// 
// Description: This script is used to support the curved slider for Ex&Ey slider
// 
// Developers: Nan Kang
// 
// Last updated: July 20, 2022
//======================================

public class CurvedSlider : MonoBehaviour
{
    Slider slider;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        arrow.transform.localRotation = Quaternion.Euler(0, 0, 15*slider.value);
    }
}
