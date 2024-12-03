using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorController : MonoBehaviour
{
    public Transform VectorEndAnchor, VectorBeginAnchor, VectorTop, VectorBottom;

    bool resetScalingFactor = false;

    Vector3 vectorPoint;

    public Transform Vector;

    [SerializeField] private float scalingFactor;
    float length;

    // Start is called before the first frame update
    void Awake()
    {
        scalingFactor = (VectorTop.position - VectorBottom.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (VectorBeginAnchor == null || VectorEndAnchor == null) return;
        Vector.transform.position = VectorBeginAnchor.position;
        vectorPoint = VectorEndAnchor.position - VectorBeginAnchor.position;
        length = vectorPoint.magnitude;
        if (resetScalingFactor) // reinitialize scaling factor
        {
            scalingFactor = (VectorEndAnchor.position - VectorBeginAnchor.position).magnitude;
            resetScalingFactor = false;
        }

        // Set vector transform to fit the new vector
        Vector.transform.rotation =
            Quaternion.FromToRotation(new Vector3(0, 1, 0), vectorPoint.normalized);
        if(scalingFactor == 0)
        {
            scalingFactor = (VectorEndAnchor.position - VectorBeginAnchor.position).magnitude;
            resetScalingFactor = false;
        } else if (float.IsNaN(scalingFactor))
        {
            scalingFactor = 0;
            resetScalingFactor = true;
        }
        float newScale = vectorPoint.magnitude / scalingFactor;
        Vector.transform.localScale = new Vector3(1, newScale, 1);
    }

    public void SetAnchors(Transform BeginAnchor, Transform EndAnchor)
    {
        VectorEndAnchor = EndAnchor;
        VectorBeginAnchor = BeginAnchor;
        //scalingFactor = (VectorEndAnchor.position - VectorBeginAnchor.position).magnitude;
        resetScalingFactor = false;
    }
}
