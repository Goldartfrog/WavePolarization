using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionSettings : MonoBehaviour
{
    //==========================================
    // TransmissionSettings.cs (Version 1)
    // 
    // Description: This script controls 
    // waves user has to match in order to
    // progress through the lab.
    //
    // Future plans include calculating power
    // and potentially splitting up AdvanceWave
    // for legibility and clarity
    // 
    // Wave Polarization Developer: Alexander
    // Romanov
    // 
    // Last updated: July 28, 2022
    //=========================================

   

    private WaveConstructor waveConstructor;
    [SerializeField] private GameObject fieldParent;
    //number of waves student has to match
    [SerializeField] private int numberOfWaves;
    public int userWaveGuess = 0;
    public int wavesThusFar = 0;
    public float outphi = 0; //used by WaveGameController to decide handedness of wave

    //Current wave type transmitted, with direction indicated by E field for linear and
    //handedness in time for circular (which is customary). LCP in time is RCP in space.
    public enum CurrentWaveType
    {
        LinearX,
        LinearY,
        LinearDiag,
        Circular,
        Eliptical
    }

    public CurrentWaveType waveType;

    // Start is called before the first frame update
    void Start()
    {
        this.waveType = CurrentWaveType.LinearX;
        //get wave drawer so we can set its parameters
        this.waveConstructor = this.fieldParent.GetComponent<WaveConstructor>();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        //AdvanceWave();
    }

    //this handles wave type generation and display control
    public void AdvanceWave()
    {   //check if activity should continue, whether or not the wave advances
        if (this.wavesThusFar <= this.numberOfWaves )
        {
            //generate values
            this.waveType = (CurrentWaveType)Random.Range(0, 5);
            //print for testing
            print(this.waveType);
            float newXAmp = 0;
            //technically unnecessary to calculate the y amplitudes, just included for completeness
            float newYAmp = 0;
            float newPhi = 0;
            float newBeta = 0;
            float newOmega = 0;
            int LCPorRCP = 0;
            //linearX
            switch (this.waveType)
            {
                case CurrentWaveType.LinearX:
                    newXAmp = 1f;
                    break;

                case CurrentWaveType.LinearY:
                    newYAmp = 1f;
                    break;
                case CurrentWaveType.LinearDiag:
                    newXAmp = Mathf.Sqrt(2) / 2;
                    newYAmp = Mathf.Sqrt(2) / 2;
                    break;
                case CurrentWaveType.Circular:
                    newXAmp = Mathf.Sqrt(2) / 2;
                    newYAmp = Mathf.Sqrt(2) / 2;
                    LCPorRCP = Random.Range(0, 2);
                    if (LCPorRCP == 0)
                    {
                        newPhi = Mathf.PI / 2;
                        outphi = newPhi;
                    }
                    else
                    {
                        newPhi = -Mathf.PI / 2;
                        outphi = newPhi;
                    }
                    break;
                case CurrentWaveType.Eliptical:
                    //note to self: this range may need to be adjusted
                    newXAmp = Random.Range(.1f, .9f);
                    newYAmp = Mathf.Sqrt(1 - (newXAmp * newXAmp));

                    LCPorRCP = Random.Range(0, 2);
                    if (LCPorRCP == 0)
                    {
                        //note to self: this range may need to be adjusted
                        newPhi = Random.Range((-Mathf.PI / 2 + .1f), -0.1f);
                        outphi = newPhi;
                    }
                    else
                    {
                        //note to self: this range may need to be adjusted
                        newPhi = Random.Range(0.1f, (Mathf.PI / 2 - .1f));
                        outphi = newPhi;
                    }
                    break;
            }
            //note to self: this range may need to be adjusted
            newBeta = Random.Range(1, 5);
            //note to self: this range may need to be adjusted
            newOmega = Random.Range(1, 5);

            //set values
            this.waveConstructor.waveBeta = newBeta;
            this.waveConstructor.waveOmega = newOmega;
            this.waveConstructor.waveCompPhaseDelta = newPhi;
            this.waveConstructor.eFieldXcomp = newXAmp;
            this.waveConstructor.eFieldYcomp = newYAmp;
            this.wavesThusFar += 1;
        }

    }

    //so far this is all the billboard interacts with, just sends in the user guess each time
    //the user makes a guess. Should NOT continuously send the user guess, only when adjusted
    //by user.
    //user wavetype guess, goes from 0-4, 0: linear in x, 1: linear in y, 2: diagonal linear,
    //3: circular, 4: eliptical

    //switch to enum
    public void SetUserGuess(CurrentWaveType guess)
    {
        if (guess == this.waveType)
        {
            AdvanceWave();
        }
    }
    //sets total number of waves to cycle through
    public void SetNumberOfWaves(int noWaves)
    {
        this.numberOfWaves = noWaves;
    }
}
