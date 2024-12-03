using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropogatingWave : MonoBehaviour
{
    [SerializeField] private LineRenderer xRenderer, yRenderer, waveRenderer;

    [SerializeField] private Transform Anchor;

    public float X, Y, Period, Beta, Dist;

    public bool PropogationInZ, StartWave;

    bool initCycleDone = false;

    Vector3 PtPosition, RotatedPosition;

    [HideInInspector] public float RotateAngle;

    [SerializeField] private int renderCount;

    [SerializeField] private WaveCollider waveCollider;

    float reflectedAt = 0;

    float pi = Mathf.PI;
    float angF;
    int nodes_t, prop_t;
    float beta_t, x_t, y_t, dist_t;
    int i_alt = 0;

    [SerializeField] float time_t = 0;
    float period_t;

    // Start is called before the first frame update
    void Awake()
    {
        beta_t = Beta;
        x_t = X;
        y_t = Y;
        period_t = Period;
        angF = 2 * pi / period_t;
        xRenderer.positionCount = renderCount;
        yRenderer.positionCount = renderCount;
        waveRenderer.positionCount = renderCount;
        nodes_t = renderCount;
        dist_t = Dist;
        if (PropogationInZ) prop_t = -1;
        else prop_t = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartWave) return;
        transform.position = Anchor.position;
        transform.rotation = Anchor.rotation;

        int loopCount;
        float initLoopCount = nodes_t * time_t / period_t;
        if(time_t < period_t && !initCycleDone)
        {
            if (time_t + Time.deltaTime >= period_t)
            {
                loopCount = nodes_t;
                xRenderer.positionCount = loopCount;
                yRenderer.positionCount = loopCount;
                waveRenderer.positionCount = loopCount;
                initCycleDone = true;
            } else
            {
                loopCount = (int)initLoopCount;
                xRenderer.positionCount = loopCount;
                yRenderer.positionCount = loopCount;
                waveRenderer.positionCount = loopCount;
            }
        } else
        {
            loopCount = nodes_t;
        }

        float i_f = 0;

        for (int i = 0; i < loopCount; i++)
        {

            i_f = ((float)(i + i_alt) - (float)nodes_t / 2) / ((float)nodes_t * 2) * Dist;

            float i_since_reflection = i_alt - reflectedAt;

            float z_pos = i_f;

            PtPosition.z = z_pos;

            PtPosition.x = x_t * Mathf.Sin(angF * time_t + prop_t * beta_t * z_pos);
            PtPosition.y = 0;
            RotatedPosition = PtPosition / 10;
            xRenderer.SetPosition(i, RotatedPosition);

            PtPosition.y = y_t * Mathf.Cos(angF * time_t + prop_t * beta_t * z_pos);
            RotatedPosition = PtPosition / 10;
            waveRenderer.SetPosition(i, RotatedPosition);

            PtPosition.x = 0;
            RotatedPosition = PtPosition / 10;
            yRenderer.SetPosition(i, RotatedPosition);
        }
        i_alt++;

        setColliderPos(new Vector3(Anchor.position.x, Anchor.position.y, RotatedPosition.z + Anchor.position.z));

        time_t += Time.deltaTime;
    }

    private void setColliderPos(Vector3 wavePosition)
    {
        waveCollider.transform.position = wavePosition;
    }

    private void updateValues()
    {
        beta_t = Beta;
        x_t = X;
        y_t = Y;
        dist_t = Dist;
        if (PropogationInZ) prop_t = -1;
        else prop_t = 1;
        if (nodes_t != renderCount)
        {
            nodes_t = renderCount;
            xRenderer.positionCount = nodes_t;
            yRenderer.positionCount = nodes_t;
            waveRenderer.positionCount = nodes_t;
        }
        period_t = Period;
        angF = 2 * pi / period_t;
    }

    public void updateY(Slider newY)
    {
        Y = newY.value;
        y_t = Y;
    }

    public void updateX(Slider newX)
    {
        X = newX.value;
        x_t = X;
    }

    public void updateBeta(Slider newB)
    {
        Beta = newB.value;
        beta_t = Beta;
    }

    public void updateAngularF(Slider newW)
    {
        Period = 1 / newW.value;
        period_t = Period;
        angF = newW.value * 2 * pi;
    }

    public void updatePropogation(Slider newProp)
    {
        if (newProp.value == 0) PropogationInZ = false;
        else PropogationInZ = true;
        if (PropogationInZ) prop_t = -1;
        else prop_t = 1;
    }

    public void reflectedWave()
    {
        reflectedAt = i_alt;
    }
}
