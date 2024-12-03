using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//======================================
// TextScrolling.cs (Version 1)
// 
// Description: This script makes text
// appear one letter at a time.
// 
// Wave Polarization Developer: Aidan Wefel
// 
// Last updated: August 4, 2022
//======================================

public class TextScrolling : MonoBehaviour
{
    /// <summary> The text to be displayed </summary>
    [SerializeField] private string text;
    /// <summary> the TMP object that displays the text </summary>
    private TextMeshProUGUI TMP;
    /// <summary> Private variable that stores the currently displayed text </summary>
    private string currentText = "";
    /// <summary> Private variable that delays the next letter appearing </summary>
    private float nextActionTime = 0.0f;
    /// <summary> the speed at which the text scrolls </summary>
    [SerializeField] private float period = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        TMP = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // takes text as the end string and displays it one character at a time
        if (Time.time > nextActionTime) 
        {
            nextActionTime = Time.time + period;
            if (currentText != text)
            {
                currentText = text.Substring(0, currentText.Length + 1);
                TMP.text = currentText;
            }
        }

    }
}
