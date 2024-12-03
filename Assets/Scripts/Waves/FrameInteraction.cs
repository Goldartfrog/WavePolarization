using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrameInteraction : MonoBehaviour
{
    private Vector3 scale;                                                                              // A vetor used to store the local scale information of the frame size.
    private GameObject message;                                                                         // Gameobject "StartMessage", the Maxwell quote students will see when they enter the scene.
    AudioSource audioSource;                                                                            // Add Aduio source of "Fun facts" of people in the frames;



    // Start is called before the first frame update
    // The following code is used to find relevant gameobjects and deactivate them in the scene.
    public void Start()
    {
        message = GameObject.Find("StartMessage");
        Debug.Log("message.name");

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Fun Fact"))                       // Using a for loop to find all fun facts which are gameobjects displaying fun facts about people in the frames.
        {                                                                                               // Set all fun facts inactive.
            if (obj.name == "Fun Fact")             
            {
                obj.SetActive(false);
            }
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Intro"))                          // Using a loop to find all "Intro" which are gameobjects displaying a short introduction of people in the frames.
        {                                                                                               // Set all "Intro" inactive.
            if (obj.name == "Intro")
            {
                obj.SetActive(false);
            }
        }
    }

    // The following code is used to dismiss the Maxwell quote seen when player enters the scene.
    public void Update()
    {
        if (message.activeSelf)                                                                         // Check if the Maxwell quote is active.
            //Debug.Log("Active");                                                                        // If it is active, check if button "A" on controllers is pressed.
        {
            if (OVRInput.Get(OVRInput.Button.One))                                                      // If it is pressed, deactivate the gameobject, dismiss the Maxwell quote.
            {
                message.SetActive(false);
                Debug.Log("Deactivate");
            }
        }
    }


    // The following code is used to control the event when the laser pointer(controller) points at the frames.

    // Enlarge the frame when the laser pointer enters the selecte area.
    public void OnHoverEnter(Transform t)
    {
        if (t.gameObject.tag == "Lab")                                          // "Lab" tag gameobjects are frames that represent actual labs, outer circle frames.
        {
            Debug.Log("Enter");
            GameObject lab = t.gameObject;
            Debug.Log(lab.name);
            scale = lab.transform.localScale;                                       
            lab.transform.localScale += new Vector3(0, 0.15F, 0.15F);           // Enlarge the object when pointer hits it.
            GameObject parent = lab.transform.parent.gameObject;
            GameObject child = parent.transform.Find("Fun Fact").gameObject;    // Find Fun Fact and display it.
            child.SetActive(true);


            audioSource = t.gameObject.GetComponent<AudioSource>();
            audioSource.Play();                                                 // Play the audio clip.
        }

        if (t.gameObject.tag == "Frame")                                        // "Frame" tag gameobjects are frames that without labs attached. inner circle frames.
        {
            Debug.Log("Enter");
            GameObject frame = t.gameObject;
            GameObject child = frame.transform.Find("Intro").gameObject;        // Find Fun Fact and display it.
            child.SetActive(true);
        }
    }

    // Reset the frame size when laser pointer leaves the area.
    public void OnHoverExit(Transform t)
    {
        if (t.gameObject.tag == "Lab")                                          // "Lab" tag gameobjects are frames that represent actual labs, outer circle frames.
        {
            Debug.Log("Exit.");
            GameObject lab = t.gameObject;
            lab.transform.localScale = scale;                                   // return to its original size after pointer leaves it.
            GameObject parent = lab.transform.parent.gameObject;
            GameObject child = parent.transform.Find("Fun Fact").gameObject;
            if (child.activeSelf == true)                                       // Find Fun Fact and disable it.
            {
                child.SetActive(false);
            }

            audioSource = t.gameObject.GetComponent<AudioSource>();
            audioSource.Stop();                                                 // Stop the audio clip.
        }

        if (t.gameObject.tag == "Frame")                                        // "Frame" tag gameobjects are frames that without labs attached. inner circle frames.
        {
            Debug.Log("Exit.");
            GameObject frame = t.gameObject;
            //GameObject parent = frame.transform.parent.gameObject;
            GameObject child = frame.transform.Find("Intro").gameObject;    
            if (child.activeSelf == true)                                       
            {
                child.SetActive(false);
            }
            
        }
    }


    // The following code is to enter to different labs depending on which frames are selected.

    // Load different scenes based on what labs are selected.
    public void OnSelected(Transform t)
    {
        if (t.gameObject.name == "Lab 1")
        {
            //SceneManager.LoadScene("Sample", LoadSceneMode.Single);
        }
        //Debug.Log("Clicked on " + t.gameObject.name);

        if (t.gameObject.name == "Lab 2")
        {
            SceneManager.LoadScene("Lab2_Coulomb", LoadSceneMode.Single);
        }

        if (t.gameObject.name == "Lab 3")
        {
           SceneManager.LoadScene("Lab3_Gauss", LoadSceneMode.Single);
        }

        if (t.gameObject.name == "Lab 4")
        {
            SceneManager.LoadScene("Lab4_polarization", LoadSceneMode.Single);
        }

        if (t.gameObject.name == "Lab 5")
        {
            //SceneManager.LoadScene("Sample", LoadSceneMode.Single);
        }

    }
}
