using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveGameController : MonoBehaviour
{
    public int wavesReceived;
    public AudioSource audioSource;

    [SerializeField] private float delayBeforeGameStart;

    [SerializeField] private float waveTranslateSpeed;
    [SerializeField] private int howManyWaves;
    [SerializeField] private GameObject howManyWavesBattery;

    public delegate void WaveCrashed();
    public static event WaveCrashed waveCrashed;

    public delegate void WaveSuccess();
    public static event WaveSuccess waveSuccess;

    [SerializeField] private GameObject wave;
    private Transform waveTransform;
    private TransmissionSettings transmissionSettings;

    [SerializeField] private GameObject wheel;
    private RotateWheel rotateWheel;

    [SerializeField] private GameObject waveSpawnLocation;

    [SerializeField] private GameObject scoreTracker;
    private TextMeshProUGUI scoreText;

    // enums are pain actually
    private List<int> lookupList = new List<int> { 3, 4, 1, 2, 0 };

    [SerializeField] private float flashDuration = 1f;
    [SerializeField] private GameObject light1;
    private Light lt1;
    [SerializeField] private GameObject light2;
    private Light lt2;
    private Color colorRed = Color.red;
    private Color colorWhite = Color.white;

    //private List<TransmissionSettings.CurrentWaveType> lookupTable = {Circular, Eliptical, LinearY, LinearDiag, LinearX};

    [SerializeField] private float overallTimeValue = 300;
    [SerializeField] private float timePerWave = 20;
    private float currentWaveTime;

    [SerializeField] private GameObject countDownTimer;
    private TextMeshProUGUI timerText;

    public bool gameStarted = false;

    [SerializeField] private GameObject narrator;
    private NarratorController narratorController;
    private bool playedStartupSound = false;

    [SerializeField] private GameObject waveSoundObject;
    private AudioSource waveSound;
    public bool clockwise = true; // used for circular and elliptical waves

    void CallWaveCrash() {
        Debug.Log("Wave Crashed");
        this.StartCoroutine(this.FlashLightsRed());
    }

    void CallWaveSuccess() {
        Debug.Log("Wave good");
        this.wavesReceived++;
        this.StartCoroutine(this.TextScrolling());
        this.audioSource.Play();
    }

    void Awake()
    {
        waveTransform = wave.GetComponent<Transform>();
        transmissionSettings = wave.GetComponent<TransmissionSettings>();
        rotateWheel = wheel.GetComponent<RotateWheel>();
        scoreText = scoreTracker.GetComponent<TextMeshProUGUI>();
        timerText = countDownTimer.GetComponent<TextMeshProUGUI>();
        narratorController = narrator.GetComponent<NarratorController>();
        waveSound = waveSoundObject.GetComponent<AudioSource>();
        wavesReceived = 0;
        waveCrashed += CallWaveCrash;
        waveSuccess += CallWaveSuccess;
        lt1 = light1.GetComponent<Light>();
        lt2 = light2.GetComponent<Light>();
        scoreText.text = wavesReceived.ToString() + "/" + howManyWaves.ToString() + " Waves\nReceived";
        SetBattery();

        // this.StartCoroutine(this.PromptForGame());
        
        
    }

    public void callOnTeleport()
    {
        if (!playedStartupSound) {
            StartCoroutine(DelayGame());
            playedStartupSound = true;
        }
        
    }
    public void startGame() 
    {
        //Debug.Log("Game started");
        scoreText.text = wavesReceived.ToString() + "/" + howManyWaves.ToString() + " Waves\nReceived";
        SetBattery();
        if (gameStarted == false)
        {
            gameStarted = true;
            wave.active = true;
            currentWaveTime = timePerWave;
            ResetWave();
            this.StartCoroutine(this.OneMinWarning());
            narratorController.Play2MinWarning();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        if (overallTimeValue > 0) 
        {
            overallTimeValue -= Time.deltaTime;
            currentWaveTime -= Time.deltaTime;
            if (wavesReceived < howManyWaves) 
            {
                if (currentWaveTime > 0) 
                {
                    //Debug.Log("It works");
                    waveTransform.position += waveTransform.TransformDirection(Vector3.fwd) * waveTranslateSpeed * Time.deltaTime;
                } else {
                    //enums are pain
                    if ((int)transmissionSettings.waveType == lookupList[rotateWheel.state])
                    {
                        if(rotateWheel.state == 0 || rotateWheel.state == 1) {
                            if((clockwise && transmissionSettings.outphi < 0) || (!clockwise && transmissionSettings.outphi > 0)){
                                waveSuccess?.Invoke();
                            } else {
                                waveCrashed?.Invoke();
                            }
                        } else {
                            // checks if null, if its not, calls it
                            waveSuccess?.Invoke();
                        }
                        
                    } else {
                        waveCrashed?.Invoke();
                    }
                    ResetWave();
                    waveSound.Play();
                }

            } else if (wavesReceived == howManyWaves)
            {
                wave.SetActive(false);
                narratorController.PlayGameSuccess();
                gameStarted = false;
            }
            float minutes = Mathf.FloorToInt(overallTimeValue / 60); // convert game time to minutes and seconds
            float seconds = Mathf.FloorToInt(overallTimeValue % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // black magic string formatting

        } else if (wavesReceived < howManyWaves)
        {
            waveCrashed -= this.CallWaveCrash; // unsubscribe the functions from the event, without it there were errors changing scenes
            waveSuccess -= this.CallWaveSuccess;
            SceneManager.LoadScene("SpaceStation_Exterior_Night"); //load the crash scene
        } 

    }
    void ResetWave() {
        waveTransform.position = waveSpawnLocation.GetComponent<Transform>().position;
        currentWaveTime = timePerWave;
        transmissionSettings.AdvanceWave();
    }

    void SetBattery()
    {
        for (int i = 0; i < howManyWavesBattery.transform.childCount; i++)
        {
            howManyWavesBattery.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = wavesReceived >= i + 1 ? Color.green : Color.gray;
        }
    }

    IEnumerator TextScrolling() {
        string targetText = wavesReceived.ToString() + "/" + howManyWaves.ToString() + " Waves\nReceived";
        //Debug.Log(targetText);
        string currentText = "";
        while (currentText != targetText) {
            currentText = targetText.Substring(0, currentText.Length + 1);
            scoreText.text = currentText;
            SetBattery();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FlashLightsRed() {
        int count = 0;
        float startTime = Time.time;
        while (count < 3) {
            float t = Mathf.PingPong(Time.time, flashDuration) / flashDuration;
            lt1.color = Color.Lerp(colorRed, colorWhite, t);
            lt2.color = Color.Lerp(colorRed, colorWhite, t);
            if (Time.time - startTime > 3) {
                lt1.color = Color.white;
                lt2.color = Color.white;
                yield break;
            }
            yield return null;
        }
        yield break;
    }

    //IEnumerator PromptForGame()
    //{
    //    if (!gameStarted)
    //    {
    //        narratorController.PlayStartingTalk();
    //    }
    //}

    IEnumerator OneMinWarning()
    {
        yield return new WaitForSeconds(61f); // delayed by one second so that it won't play over the success sound if the player 
        // wins on their 6th wave
        if (gameStarted)
        {
            narratorController.Play1MinWarning();
        }
    }

    IEnumerator DelayGame()
    {
        //TODO make it only play once
        narratorController.PlayStartingTalk();
        yield return new WaitForSeconds(delayBeforeGameStart);
        startGame();
    }

    public void setClockwiseTrue() {
        clockwise = true;
    }
    public void setClockwiseFalse() {
        clockwise = false;
    }
}


