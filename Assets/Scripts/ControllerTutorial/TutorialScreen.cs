using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ControllerSelection;
using UnityEngine.SceneManagement;

public class TutorialScreen : MonoBehaviour
{
public enum tutorialStep {Welcome, Laser, Point, Hold, Control, Drop, Teleport1, Teleport2, Simulation1, Simulation2, Reset, Meter, Complete};
public bool tutorial, forceExited;
public Text tutorialText;
public string finalText;
public tutorialStep checkpoint;
public GameObject rightController;
public GameObject leftController;
public GameObject cube;
public Draggable draggableCube;
public ObjectFeedback feedback;
public AudioSource ding;
public Playpause playpause;
public GameObject camera_rig;
public List<GameObject> tplist;
private float timeCounter;

public GameObject tutorialImage;

    [SerializeField] GameObject teleporterObject;


// end tutorial
public GameObject chargeListGetter;

// etube light color animator
[SerializeField] private Animator eTube1;
[SerializeField] private Animator eTube2;
[SerializeField] private Animator eTube3;
[SerializeField] private Animator eTube4;
[SerializeField] private Animator image;
[SerializeField] private Animator text;
[SerializeField] private Light tutorialLight;
[SerializeField] private Light spotLight;


//eye catcher
private Renderer rend;
Color colorEnd = Color.yellow;
Color colorEnd2 = new Color(0, 0, 0.5f, 0.3f); // transparent blue
Color colorStart;
float duration = 1.0f;

Vector3 originalLocation;

public GameObject probePic;

[SerializeField] Animator tutorialAnimator;
[SerializeField] Animator sideAnimator;


// Start is called before the first frame update
void Start()
{

    //end tutorial
    
    this.tutorial = true;
    this.forceExited = false;
    this.checkpoint = 0;

    // To access the isHeld variable from the draggable script
    this.draggableCube = cube.GetComponent<Draggable>();
    // To access the hit variable from the feedback script
    this.feedback = cube.GetComponent<ObjectFeedback>();
    this.rightController.transform.Find("Hand").gameObject.SetActive(false);
    this.rightController.transform.Find("Stick").gameObject.SetActive(false);
    this.rightController.transform.Find("Index").gameObject.SetActive(false);
    this.rightController.transform.Find("Canvas Right").transform.Find("HandText").gameObject.SetActive(false);
    this.rightController.transform.Find("Canvas Right").transform.Find("IndexText").gameObject.SetActive(false);
    this.rightController.transform.Find("Canvas Right").transform.Find("StickText").gameObject.SetActive(false);
    this.leftController.transform.Find("Hand").gameObject.SetActive(false);
    this.leftController.transform.Find("Stick").gameObject.SetActive(false);
    this.leftController.transform.Find("Index").gameObject.SetActive(false);
    this.leftController.transform.Find("Canvas Left").transform.Find("HandText").gameObject.SetActive(false);
    this.leftController.transform.Find("Canvas Left").transform.Find("IndexText").gameObject.SetActive(false);
    this.leftController.transform.Find("Canvas Left").transform.Find("StickText").gameObject.SetActive(false);
    this.checkpoint = tutorialStep.Welcome;

    //eye catcher
    this.rend = GameObject.Find("Tutorial Probe").GetComponent<Renderer>();
    this.colorStart = rend.material.color;
    this.spotLight.intensity = 0;


    GameObject go = GameObject.Find("transport_spot");
    this.timeCounter = 0;


    if (go)
    {
        GameObject.Find("transport_spot").SetActive(false);
    }

    for (int i = 1; i <= 15; i++)
    {
        string name = "transport_spot (" + i + ")";
        go = GameObject.Find(name);
        if (go)
        {
            GameObject.Find(name).SetActive(false);
        }

    }
}

// Update is called once per frame
    void Update()
    {
    //if(!playpause.paused)
    //{
        // Change tutorial screen text
        if (this.tutorial)
        {

            switch (this.checkpoint)
            {
                case tutorialStep.Welcome:
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        this.forceExited = true;
                        this.tutorial = false;
                        this.tutorialText.text = "";
                    }
                    if (OVRInput.GetDown(OVRInput.RawButton.B))
                    {
                        this.rightController.transform.Find("Index").gameObject.SetActive(true);
                        this.rightController.transform.Find("Canvas Right").transform.Find("IndexText").gameObject.SetActive(true);
                        this.leftController.transform.Find("Index").gameObject.SetActive(true);
                        this.leftController.transform.Find("Canvas Left").transform.Find("IndexText").gameObject.SetActive(true);
                        this.tutorialText.text = "";
                    this.checkpoint = tutorialStep.Laser;
                    }
                    break;
                case tutorialStep.Laser:
                    tutorialAnimator.SetBool("laserStep", true);
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
                    {
                        tutorialImage.transform.Find("Main").transform.position = new Vector3(-43.8f, 1.17f, 77f);
                        tutorialImage.transform.Find("Side").transform.position = new Vector3(-44.5f, 1.17f, 77f);
                        checkpoint = tutorialStep.Point;
                        this.spotLight.intensity = 3;
                        ding.Play();
                    }

                    break;
                case tutorialStep.Point:
                    tutorialAnimator.SetBool("laserStep", false);
                    tutorialAnimator.SetBool("pointStep", true);
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    
                        rightController.transform.Find("Hand").gameObject.SetActive(true);
                        rightController.transform.Find("Canvas Right").transform.Find("HandText").gameObject.SetActive(true);
                        leftController.transform.Find("Hand").gameObject.SetActive(true);
                        leftController.transform.Find("Canvas Left").transform.Find("HandText").gameObject.SetActive(true);
                        tutorialAnimator.SetBool("holdStep", true);
                        checkpoint = tutorialStep.Hold;
                    
                    break;
                case tutorialStep.Hold:
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    if (draggableCube.isHeld)
                    {
                        rightController.transform.Find("Stick").gameObject.SetActive(true);
                        rightController.transform.Find("Canvas Right").transform.Find("StickText").gameObject.SetActive(true);
                        // leftController.transform.Find("Stick").gameObject.SetActive(true);
                        // leftController.transform.Find("Canvas Left").transform.Find("StickText").gameObject.SetActive(true);
                        tutorialAnimator.SetBool("raiseStep", true);
                        sideAnimator.SetBool("start", true);
                        this.spotLight.GetComponent<Light>().intensity = 0;
                        checkpoint = tutorialStep.Control;
                        ding.Play();
                        }
                    break;
                case tutorialStep.Control:
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    float push = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;
                    //float rotate = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).x;
                    if ((push > 0.1f || push < -0.1f) && draggableCube.isHeld)
                    {
                        ding.Play();
                        sideAnimator.SetBool("fadeOut", true);
                        checkpoint = tutorialStep.Drop;
                    }

                    break;
                case tutorialStep.Drop:
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    if (!draggableCube.isHeld)
                    {
                        rightController.SetActive(false);
                        leftController.SetActive(false);
                        ding.Play();
                        tutorialImage.SetActive(false);

                        checkpoint = tutorialStep.Teleport1;
                    }
                    break;
                case tutorialStep.Teleport1:
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    timeCounter += Time.deltaTime;
                    eTube1.Play("TurnOff");
                    eTube2.Play("TurnOff");
                    image.Play("ScreenOff");
                    text.Play("ScreenOff");
                    eTube3.Play("TurnOn");
                    eTube4.Play("TurnOn");
                    checkpoint = tutorialStep.Teleport2;
                    break;
                case tutorialStep.Teleport2:
                    if (OVRInput.GetDown(OVRInput.RawButton.X))
                    {
                        rightController.SetActive(true);
                        leftController.SetActive(true);
                    }
                    else if (OVRInput.GetUp(OVRInput.RawButton.X))
                    {
                        rightController.SetActive(false);
                        leftController.SetActive(false);
                    }
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    timeCounter += Time.deltaTime;
                    if (timeCounter > 1)
                    {
                        GameObject.Find("Canvas").transform.position = new Vector3(-43.7f, 1.77f, 80.5f);
                        image.Play("ScreenOn");
                        text.Play("ScreenOn");
                        teleporterObject.SetActive(true);
                    }
                    if (timeCounter > 1.05)
                    {
                        this.spotLight.transform.position = new Vector3(-43.7f, 1.77f, 74.2f);
                        this.spotLight.GetComponent<Light>().intensity = 2f;
                        GameObject go3 = GameObject.Find("Controller Tutorial").transform.Find("Tutorial Probe").gameObject;
                        go3.SetActive(false);
                        tutorialImage.SetActive(true);
                        tutorialAnimator.SetBool("teleportStep", true);
                        tutorialImage.transform.Find("Side").transform.position = new Vector3(-43.75f, 1.4f, 80f);
                        sideAnimator.SetBool("teleportClick", true);
                    }
                    if (timeCounter > 3)
                    {
                        tplist[0].SetActive(true);
                        
                    }
                    if (tplist[0].GetComponent<Draggable>().isHeld)
                    {
                        rightController.transform.position = new Vector3(-41.7f, 0.8f, 79.5f);
                        leftController.transform.position = new Vector3(-45.5f, 0.8f, 79.5f);
                        timeCounter = 0;


                        this.tutorialText.text = "Congrats on completing the tutorial!";

                        ding.Play();
                        tutorialImage.SetActive(false);

                        tutorial = false;
                    }
                    break;
                default:
                    if (OVRInput.GetDown(OVRInput.RawButton.X))
                    {
                        rightController.SetActive(true);
                        leftController.SetActive(true);
                    }
                    else if (OVRInput.GetUp(OVRInput.RawButton.X))
                    {
                        rightController.SetActive(false);
                        leftController.SetActive(false);
                    }
                    if (OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        forceExited = true;
                        tutorial = false;
                    }
                    break;
            }
        }
        // Change to the main lab at the end of the tutorial
        else
        {
            timeCounter += Time.deltaTime;

            if (timeCounter > 3)
            {
                GameObject go = GameObject.Find("Controller Tutorial").transform.Find("OculusTouchForQuestAndRiftSRightModel").gameObject;
                GameObject go2 = GameObject.Find("Controller Tutorial").transform.Find("OculusTouchForQuestAndRiftSLeftModel").gameObject;
                GameObject go3 = GameObject.Find("Controller Tutorial").transform.Find("Tutorial Probe").gameObject;
                GameObject go4 = GameObject.Find("Controller Tutorial").transform.Find("Canvas").transform.Find("TutorialAnimation").transform.Find("Main").gameObject;
                GameObject go5 = GameObject.Find("Controller Tutorial").transform.Find("Canvas").transform.Find("TutorialAnimation").transform.Find("Side").gameObject;
                GameObject go6 = GameObject.Find("Controller Tutorial").transform.Find("Spot Light").gameObject;

                if (go && go2 && go3 && go4 && go5 && go6)
                {
                    Destroy(go.gameObject);
                    Destroy(go2.gameObject);
                    Destroy(go3.gameObject);
                    Destroy(go4.gameObject);
                    Destroy(go5.gameObject);
                    Destroy(go6.gameObject);
                }

                eTube3.Play("TurnOff");
                eTube4.Play("TurnOff");
                eTube1.Play("TurnOff");
                eTube2.Play("TurnOff");
                image.Play("ScreenOff");
                text.Play("ScreenOff");
                tutorialLight.intensity = timeCounter / 100;

                for (int i = 0; i < tplist.Count; i++)
                {
                    tplist[i].SetActive(true);
                }
                StartCoroutine(LoadWavePolarizationAsync());
                if (timeCounter > 6)
                {
                    GameObject go7 = GameObject.Find("Controller Tutorial");
                    if (go7)
                    {
                        Destroy(go7.gameObject);
                    }
                }

            }
        }
    }

    IEnumerator LoadWavePolarizationAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WavePolarization");
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

