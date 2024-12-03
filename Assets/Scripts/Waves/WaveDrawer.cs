using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//======================================
// WaveDrawer.cs (Version 1)
// 
// Description: This script draws
// the electric and magnetic field wave
// using the information from the wave
// constructor.
// 
// Wave Polarization Developer: Aidan Wefel
// 
// Last updated: August 4, 2022
//======================================

public class WaveDrawer : MonoBehaviour
{
    /// <summary> The waveConstructor object that provides the information about the fields. </summary>
    private WaveConstructor waveConstructor;
    /// <summary> The linerenderer component for the electric field line </summary>
    private LineRenderer electricLineRenderer;
    /// <summary> The linerenderer component for the magnetic field line </summary>
    private LineRenderer magneticLineRenderer;
    /// <summary> The object that holds everything else and the scripts, usually "Wave Generator Box" </summary>
    [SerializeField] private GameObject fieldParent;
    /// <summary> The  electric field line object </summary>
    [SerializeField] private GameObject electricFieldObject;
    /// <summary> The magnetic field line object </summary>
    [SerializeField] private GameObject magneticFieldObject;
    /// <summary> The z axis line object </summary>
    [SerializeField] private GameObject zAxis;
    /// <summary> The material to assign to the electric line </summary>
    [SerializeField] private Material electricFieldMaterial;
    /// <summary> The material to assign to the magnetic line </summary>
    [SerializeField] private Material magneticFieldMaterial;
    /// <summary> The width to make all the lines - can be finalized </summary>
    [SerializeField] private float fieldLineWidth;
    /// <summary> The amount of points for each line - can be finalized </summary>
    [SerializeField] private int detail = 200;
    /// <summary> The scale factor to multiply the electric field by </summary>
    [SerializeField] private float scaleEFieldOutput = 10.0f;
    /// <summary> The scale factor to multiply the magnetic field by </summary>
    [SerializeField] private float scaleBFieldOutput = 10.0f;
    /// <summary> Script-set variable that works with endposition to define the line </summary>
    private Vector3 startPosition;
    /// <summary> Script-set variable that works with startposition to define the line </summary>
    private Vector3 endPosition;
    /// <summary> The length of the line (not in unity units, needs experimentation) </summary>
    [SerializeField] private float length = 10.0f;
    /// <summary> Script-set variable that defines the disance between points </summary>
    private float stepLength;
    /// <summary> Script-set variable that defines the direction of the line </summary>
    private Vector3 pointingVector;
    /// <summary> Script-used array for holding the positions of the electric points </summary>
    private Vector3[] electricPositions;
    /// <summary> Script-used array for holding the positions of the magnetic points </summary>
    private Vector3[] magneticPositions;

    // Variables for the InitializeVectors and DrawInfo functions
    /// <summary> The vectors will be created as children of this object </summary>
    [SerializeField] private GameObject vectorHolder;
    /// <summary> The magnetic vectors will be created as children of this object </summary>
    [SerializeField] private GameObject bVectorHolder;
    /// <summary> The number of vectors to create - can be finialized </summary>
    [SerializeField] private int vectorNumber;
    /// <summary> The object the vectors instantiate </summary>
    [SerializeField] public GameObject vectorPrefab;
    /// <summary> The array of electric vectors </summary>
    private List<GameObject> electricVectors = new List<GameObject>();
    /// <summary> The array of magnetic vectors </summary>
    private List<GameObject> magneticVectors = new List<GameObject>();
    /// <summary> The width of the instantiated vectors - can be finalized </summary>
    [SerializeField] private float vectorWidth;

    // Variables for the TraceWave function
    /// <summary> The prefab for the point object </summary>
    [SerializeField] private GameObject pointPrefab;
    /// <summary> The points will be created as children of this object </summary>
    [SerializeField] private GameObject pointHolder;
    /// <summary> Script used variable to detect time passing </summary>
    private int count = 0;
    /// <summary> The array of points </summary>
    private List<GameObject> points = new List<GameObject>();
    /// <summary> Script-used variable to keep track of index of points </summary>
    private int location = 0;
    /// <summary> The number of points to create at once - can be finialized </summary>
    [SerializeField] private int maxPoints = 50;
    /// <summary> The interval to wait to draw the points - can be finialized </summary>
    [SerializeField] private int interval = 10;


    
    // Start is called before the first frame update
    void Start()
    {
        // Get or set all of these once

        // Get the LineRenderers for the electric and magnetic field objects
        this.electricLineRenderer = this.electricFieldObject.GetComponent<LineRenderer>();
        this.magneticLineRenderer = this.magneticFieldObject.GetComponent<LineRenderer>();

        // Get the Constructor object off the parent for the field objects
        this.waveConstructor = this.fieldParent.GetComponent<WaveConstructor>();
        

        // These will need to be refreshed every frame
        this.UnecessaryUpdates();
        this.DrawFields();
        this.InitializeVectors();

    }

    // Update is called once per frame
    void Update()
    {
        
        // Currently no reason to keep track of time
        //this.time_t += Time.deltaTime;

        // get rid of this when we decide on settings we like
        this.UnecessaryUpdates();

        // Drawing the important things
        this.DrawFields();
        this.DrawInfo();
        this.TraceWave();

        
    }

    // Controls the drawing of the field waves
    /// <summary> Pulls information from the waveConstructor and draws the electric and magnetic fields </summary>
    void DrawFields()
    {
        // go through each point, find the place where that point needs to go and set it there
        float scaleFieldInput = 30.0f;
        
        // Takes steps equal to the detail setting
        for (float i = 0; i < this.detail; i++)
        {
            // Plug current position into the electric field finder, sets the 3d vector to the output
            Vector2 electricValues = this.waveConstructor.FindElectricField(this.stepLength * i * scaleFieldInput);
            this.electricPositions[(int)i] = new Vector3(electricValues.x * this.scaleEFieldOutput, electricValues.y * this.scaleEFieldOutput, this.stepLength * i);

            // Plug current position into the magnetic field finder, sets the 3d vector to the output
            Vector2 magneticValues = this.waveConstructor.FindMagneticField(this.stepLength * i * scaleFieldInput);
            this.magneticPositions[(int)i] = new Vector3(magneticValues.x * this.scaleBFieldOutput, magneticValues.y * this.scaleBFieldOutput, this.stepLength * i);

            // shift em over
            this.electricPositions[(int)i] += this.endPosition;
            this.magneticPositions[(int)i] += this.endPosition;
        }
        // Sets the positions of the LineRenderers
        this.electricLineRenderer.SetPositions(this.electricPositions);
        this.magneticLineRenderer.SetPositions(this.magneticPositions);
    }

    // Controls the drawing of the axes and other informational objects
    /// <summary> Draws the axes and other informational objects </summary>
    void DrawInfo() 
    {
        Vector3[] zaxisEndpoints = new Vector3[2];

        zaxisEndpoints[0].z = this.endPosition.z;
        zaxisEndpoints[1].z = this.startPosition.z;
        this.zAxis.GetComponent<LineRenderer>().SetPositions(zaxisEndpoints);
        
        float smallsteplength = this.length / (float)this.vectorNumber;
        float scalething = this.detail / this.vectorNumber;


        
        for (int i = 0; i < electricVectors.Count; i++) 
        {
            Transform vectorTransform = electricVectors[i].GetComponent<Transform>();
            Transform bvectorTransform = magneticVectors[i].GetComponent<Transform>();

            vectorTransform.localPosition = this.endPosition - this.pointingVector * i * smallsteplength;
            bvectorTransform.localPosition = this.endPosition - this.pointingVector * i * smallsteplength;
            
            Vector3 fromLineToEField = this.electricPositions[(int)(i * scalething)] - vectorTransform.localPosition;
            Vector3 fromLineToBField = this.magneticPositions[(int)(i * scalething)] - bvectorTransform.localPosition;
            float edist = fromLineToEField.magnitude;
            float bdist = fromLineToBField.magnitude;
            
            // Scales the vectors. The z scale controls the length of the vectors, the 2.5 that seems random is because of the specific scale of the model I imported
            vectorTransform.localScale = new Vector3(vectorWidth, vectorWidth, edist * 2.5f);
            bvectorTransform.localScale = new Vector3(vectorWidth, vectorWidth, bdist * 2.5f);

            vectorTransform.localRotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(fromLineToEField.x, fromLineToEField.y, 0));
            bvectorTransform.localRotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(fromLineToBField.x, fromLineToBField.y, 0));
        }
        

    }

    // Draws points to trace the path of the last vector
    /// <summary> Draws points to trace the path of the last vector </summary>
    void TraceWave() 
    {
        Transform pointHolderTransform = pointHolder.GetComponent<Transform>();
        if (count == interval) 
        {
            if (points.Count < maxPoints)
            {
                points.Add(Instantiate(pointPrefab, pointHolderTransform));
                points[points.Count - 1].GetComponent<Transform>().localPosition = this.electricPositions[0];
            } else 
            {
                points[location].GetComponent<Transform>().localPosition = this.electricPositions[0];
                location++;
                if (location == maxPoints) 
                {
                    location = 0;
                }
            }

            count = 0;
        }
        count++;
    }

    // Creates the vector objects
    /// <summary> Creates the vector objects in the electricVectors and magneticVectors lists </summary>
    void InitializeVectors() 
    {

        if (electricVectors.Count != vectorNumber) {
            Transform vectorHolderTransform = vectorHolder.GetComponent<Transform>();
            Transform bVectorHolderTransform = bVectorHolder.GetComponent<Transform>();
            for(int i = 0; i < electricVectors.Count; i++) {
                Destroy(electricVectors[i]);
                Destroy(magneticVectors[i]);
            }
            electricVectors = new List<GameObject>();
            magneticVectors = new List<GameObject>();
            for (int i = 0; i < vectorNumber; i++) {
                electricVectors.Add(Instantiate(vectorPrefab, vectorHolderTransform));
                magneticVectors.Add(Instantiate(vectorPrefab, bVectorHolderTransform));
                electricVectors[i].GetComponent<Renderer>().material = electricFieldMaterial;
                magneticVectors[i].GetComponent<Renderer>().material = magneticFieldMaterial;
            }
        }
        
    }

    // These are the things that only need to be updated while we are finializing settings
    // They can not be updated every frame once we have them the way we like them
    // NEED TO BE RAN AT LEAST ONCE
    /// <summary> These are the things that only need to be updated while we are finializing settings </summary>
    void UnecessaryUpdates() {

        this.electricPositions = new Vector3[this.detail];
        this.magneticPositions = new Vector3[this.detail];
        this.startPosition = new Vector3(0, 0, this.length / 2);
        this.endPosition = new Vector3(0, 0, -this.length / 2);

        this.pointingVector = (this.endPosition - this.startPosition).normalized;

        this.electricLineRenderer.positionCount = this.detail;
        this.magneticLineRenderer.positionCount = this.detail;
        this.stepLength = this.length / this.detail;
        this.waveConstructor.mode = WaveConstructor.Mode.Default;
        this.electricLineRenderer.startWidth = this.fieldLineWidth;
        this.electricLineRenderer.endWidth = this.fieldLineWidth;
        this.magneticLineRenderer.startWidth = this.fieldLineWidth;
        this.magneticLineRenderer.endWidth = this.fieldLineWidth;
        InitializeVectors();
    }


}
