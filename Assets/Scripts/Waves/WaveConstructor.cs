using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

//======================================
// WaveConstructor.cs (Version 1)
// 
// Description: This script constructs
// the electric and magnetic field wave
// for the line renderer to render.
//
// Default mode is finished and functional
// Other modes for manipulating phase of
// a light wave are started but not finished.
// 
// Wave Polarization Developer: Alexander
// Romanov, Nan Kang
// 
// Last updated: July 28, 2022
//======================================

public class WaveConstructor : MonoBehaviour
{
    //this mode selector chooses between normal wave propogation,
    //wave propogation with a dielectric, and wave propogation with
    //a faraday rotator. It just changes the noraml wave to a piecewise
    //wave where the pieces further down are altered by the respective
    //device. Default is normal wave, no devices.
    public enum Mode
    {
        Default,
        Faraday,
        Dielectric
    }

    public Mode mode;

    //The parameters of the electric and magnetic field. The initial
    //conditions are just a linearly polarized (in x) light wave
    //propogating in z direction. 
    [SerializeField] public float waveBeta = 1;
    [SerializeField] public float waveOmega = 1;
    [SerializeField] public float waveCompPhaseDelta = 0;
    [SerializeField] public float eFieldXcomp = 1;
    [SerializeField] public float eFieldYcomp = 0;

    //lookup table for phase values and wave component values
    Dictionary<int, float> phaseAngles = new Dictionary<int, float>();
    Dictionary<int, Vector2> waveComponents = new Dictionary<int, Vector2>();


    float time_t = 0;
    Vector3 currentEFieldandZ = new Vector3(0, 0, 0);
    Vector3 currentBFieldandZ = new Vector3(0, 0, 0);
    float verdetConst;
    //Indicators for types of polarization
    public Text indicatorText;
    public GameObject rhcpIndicator;
    public GameObject lhcpIndicator;
    public GameObject rhepIndicator;
    public GameObject lhepIndicator;
    public GameObject verticalIndicator;
    public GameObject horizontalIndicator;
    public GameObject diagonalIndicator;

    // Start is called before the first frame update
    void Start()
    {
        this.mode = Mode.Default;
        //in actuality, this depends on the frequency of the light. 
        this.verdetConst = 1;

        //set lookup table of phase values and component values
        this.phaseAngles.Add(0, -1.57f);
        this.phaseAngles.Add(1, -1.05f);
        this.phaseAngles.Add(2, -0.52f);
        this.phaseAngles.Add(3, 0);
        this.phaseAngles.Add(4, 0.52f);
        this.phaseAngles.Add(5, 1.05f);
        this.phaseAngles.Add(6, 1.57f);
        this.waveComponents.Add(0, new Vector2(0,1));
        this.waveComponents.Add(1, new Vector2(0.26f, 0.97f));
        this.waveComponents.Add(2, new Vector2(0.5f, 0.87f));
        this.waveComponents.Add(3, new Vector2(0.71f, 0.71f));
        this.waveComponents.Add(4, new Vector2(0.87f, 0.5f));
        this.waveComponents.Add(5, new Vector2(0.97f, 0.26f));
        this.waveComponents.Add(6, new Vector2(1,0));

        this.SetIndicator();
    }

    // Update is called once per frame, for this to work the script must be added as a
    // component to the wave generator. It cannot just be initiated in another script.
    // This can of course be changed by having the active component script send in the time
    // with the z position. 
    void Update()
    {
        this.time_t += Time.deltaTime;
    }

    //returns electric field along z axis
    public Vector2 FindElectricField(float z)
    {
        //cumulative phase offset, used for Faraday rotator. Might not be best way to do this.
        //Currently unfinished.
        float faradayBeta = 0;
        float zStartRotator = -10000000;
        if (this.mode == Mode.Faraday)
        {
            if (zStartRotator < -9999999)
            {
                zStartRotator = z;
            }
            faradayBeta = (z - zStartRotator) * this.verdetConst * (new Vector3(currentBFieldandZ.x, currentBFieldandZ.y, 0).magnitude);
        }

        //Calculation
        Vector2 eField = new Vector2(0, 0);
        switch (this.mode)
        {
            //default wave
            case Mode.Default:
                eField = new Vector2(
                    this.eFieldXcomp * math.sin((this.waveBeta * z) - (this.waveOmega * this.time_t) - faradayBeta),
                    this.eFieldYcomp * math.sin((this.waveBeta * z) - (this.waveOmega * this.time_t) + (this.waveCompPhaseDelta ) - faradayBeta));

                this.currentEFieldandZ = new Vector3(eField.x, eField.y, z);
                break;
            //dielectric, not done yet.
            case Mode.Faraday:

                eField = new Vector2(this.eFieldXcomp * math.sin(3), 3);
                this.currentEFieldandZ = new Vector3(eField.x, eField.y, z);
                break;
            //faraday rotator, not done yet.
            case Mode.Dielectric:
                eField = new Vector2(this.eFieldXcomp * math.sin(3), 3);
                this.currentEFieldandZ = new Vector3(eField.x, eField.y, z);
                break;
        }

        this.SetIndicator();

        return eField;
    }

    //returns Magnetic field along z axis
    public Vector2 FindMagneticField(float z)
    {
        Vector2 bField = new Vector2(0, 0);
        Vector2 eField = this.FindElectricField(z);
        this.currentEFieldandZ = new Vector3(eField.x, eField.y, z);
        //here I do (0,0,1) x (E_x(z),E_y(z),0) implicity to get (-E_y(z),E_x(z),0)
        //which in our case implicitly reinforces orthogonality (See David J. Griffiths Intro to E&M Equation 9.46)
        this.currentBFieldandZ = this.waveBeta / this.waveOmega * new Vector3(-eField.y, eField.x, z);
        bField = new Vector2(this.currentBFieldandZ.x, this.currentBFieldandZ.y);
        return bField;
    }

    // Setters for the parameters of the electric and magnetic field.
    public void SetWaveBeta(float beta)
    {
        this.waveBeta = beta;
        this.SetIndicator();
    }

    // Uses to change wave direction
    public void SetWaveBetaSign()
    {
        this.waveBeta = (-1) * this.waveBeta;
        this.SetIndicator();
    }

    public void SetWaveOmega(float omega)
    {
        this.waveOmega = omega;
        this.SetIndicator();
    }

    // These following three setters, phase offset, components of E field, indicators, are specifically locked
    // tied to user input sliders. For wave transmitter, we directly change the variables' values.
    public void SetWaveCompPhaseDelta(float compPhaseDelta)
    {
        this.waveCompPhaseDelta = this.phaseAngles[(int)compPhaseDelta];
        this.SetIndicator();
    }
    // takes the 0-4 inputs on the slider and converts them to the desired values for the electric and magnetic field.
    public void SetEFieldcomp(float eFieldcomp)
    {
        this.eFieldXcomp = this.waveComponents[(int)eFieldcomp].x;
        this.eFieldYcomp = this.waveComponents[(int)eFieldcomp].y;
        this.SetIndicator();
    }
    
    //used to highlight the corresponding wave type on the panel
    public void SetIndicator()
    {
        if (((this.waveCompPhaseDelta == -1.57f && this.waveBeta > 0) || (this.waveCompPhaseDelta == 1.57f && this.waveBeta < 0)) && (this.eFieldXcomp<0.72f) && (this.eFieldXcomp>0.7f) )
        { 
            this.indicatorText.text = "Circular";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //LHCP
        else if (((this.waveCompPhaseDelta == 1.57f && this.waveBeta > 0) || (this.waveCompPhaseDelta == -1.57f && this.waveBeta < 0)) && (this.eFieldXcomp < 0.72f) && (this.eFieldXcomp > 0.7f))
        { 
            this.indicatorText.text = "Circular";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //RHCP
        //horizontal then vertical then negative vertical, or if no phase difference
        else if ((this.eFieldXcomp < 1.001f && this.eFieldXcomp > 0.999f)|| (this.eFieldXcomp > -1.001f && this.eFieldXcomp < -0.999f))
        { 
            this.indicatorText.text = "Linear";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //gorizontal
        else if (this.eFieldXcomp < .001f && this.eFieldXcomp > -.001f)
        {
            this.indicatorText.text = "Linear";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //vertical
        else if (this.waveCompPhaseDelta < .001f && this.waveCompPhaseDelta > -.001f)
        {
            this.indicatorText.text = "Linear";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
        } //diagonal
        else if ((this.waveCompPhaseDelta > -1.579f && this.waveCompPhaseDelta < -0.001f && this.waveBeta > 0) || (this.waveCompPhaseDelta < 1.57f && this.waveCompPhaseDelta > 0.001f && this.waveBeta < 0)) //&& ((this.eFieldXcomp > 0.72f) || (this.eFieldXcomp < 0.7f))
        {
            this.indicatorText.text = "Elliptical";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //LHEP
        else
        {  
            this.indicatorText.text = "Elliptical";
            this.rhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.lhcpIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.rhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            this.lhepIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.horizontalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.verticalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
            this.diagonalIndicator.GetComponent<Renderer>().sharedMaterial.color = Color.black;
        } //RHEP
    }

}

