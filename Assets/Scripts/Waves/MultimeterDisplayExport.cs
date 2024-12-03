using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultimeterDisplayExport : MonoBehaviour
{
    public float zRotation;

    // Update is called once per frame
    void Update()
    {
        zRotation = this.gameObject.transform.localEulerAngles.z;
        this.gameObject.transform.localEulerAngles = new Vector3(0,0,zRotation);

    }
}
