using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTutorial : MonoBehaviour
{
    [SerializeField] private float textSpeed;

    private string[] tutorialText = new string[]
    {
        "Welcome to the International Space Station! It took you a while to get here, and we are certain it was a challenging journey.", //0
        "There’s one more training you need to complete before you assume your full duties.",

        "Now, in front of you there’s a control panel. We are going to use it to train you in Fast Identification of Wave Polarization.", //2
        "The wave equation is listed on the front side of the control panel. You probably have seen this before in one of your electrodynamics courses.",
        "You might recall that the polarization of an electromagnetic wave is defined by how the electric field fluctuates.",
        "If the tip of the electric field vector traces a line, then the electromagnetic wave is linearly polarized.", //5
        "If it traces a circle, then it is circularly polarized; and if it traces an ellipse, then it is elliptically polarized.",
        "In the circular and elliptical cases, the handedness or helicity of the wave is also important to be specified.",
        "You can determine the handedness by looking along the direction of propagation (z in our example here), towards the source of the wave.",
        "The handedness of the circular or elliptically polarized waves describes the direction that the electric field vector is rotating.",
        "If you did not do this already, you can grab the wave viewer and bring it closer for inspection.", //10
        "There’s a display in front of you that shows the polarization of the wave you are visualizing.",

        "On the control panel in front of you, there are 4 variables that control the properties of the wave.", //12
        "Beta is called the phase constant, and measures the change in phase per unit length along the path traveled by the wave. It is measured in radians per meter",

        "Omega is called the angular frequency of the wave and it measures how fast the wave rotates. It is measured in radians per second.", //14

        "Phi is the phase offset between the x and y components of the wave. It is measured in radians.",//15
        "Phi appears in the argument of the sine function in the wave formula.",

        "Electromagnetic waves can propagate and oscillate in any direction, but ours is constrained to travel along the z axis.",//17
        "Using the slider on the right, you can change how much of the polarization is in the x direction and how much is in the y direction.",

        "The magnetic field can be visualized by activating the lever on your left.", //19

        "The electric field can propagate in both directions, which you can control with the lever below the polarization panel.", // 20

        "Feel free to practice at your own pace. When done, take a look around. There are notes regarding the International Space Station the previous crew left for you.", //21
        "When you feel confident in your abilities, head over to the Electromagnetic Observation Window to test out your new skills."
    };

    [SerializeField] private GameObject tutorialTextDisplay;
    [SerializeField] private Light spotLight;
    [SerializeField] private Transform spotLightTransform;
    
    [SerializeField] private Transform PolarizationScreenLocation;
    [SerializeField] private Transform BetaLocation;
    [SerializeField] private Transform OmegaLocation;
    [SerializeField] private Transform PhiLocation;
    [SerializeField] private Transform XYLocation;
    [SerializeField] private Transform BLeverLocation;
    [SerializeField] private Transform DirectionLeverLocation;
    [SerializeField] private GameObject WaveGenerator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


    private int textIndex = 0;
    private bool textIsScrolling = false;
    private Coroutine textGo;
    private bool betaPushed = false;
    private bool omegaPushed = false;
    private bool phiPushed = false;
    private bool xyPushed = false;
    private bool bPushed = false;
    private bool dirPushed = false;


    private bool inBeta = false;
    private bool inOmega = false;
    private bool inPhi = false;
    private bool inXY = false;
    private bool inB = false;
    private bool inDir = false;


    private TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {
        
        TMP = tutorialTextDisplay.GetComponent<TextMeshProUGUI>();
        Coroutine test = StartCoroutine(Welcome());
        spotLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //spotLightTransform.LookAt(DirectionLeverLocation);

    }

    IEnumerator TextScrolling(string targetText)
    {
        textIsScrolling = true;
        //Debug.Log(targetText);
        string currentText = "";
        while (currentText != targetText)
        {
            currentText = targetText.Substring(0, currentText.Length + 1);
            TMP.text = currentText;
            yield return new WaitForSeconds(textSpeed);
        }
        textIsScrolling = false;
    }

    // Try to do event control using coroutines

    IEnumerator Welcome()
    {
        print("started");
        textGo = StartCoroutine(TextScrolling(tutorialText[0]));
        audioSource.PlayOneShot(audioClips[0]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);
        textGo = StartCoroutine(TextScrolling(tutorialText[1]));
        audioSource.PlayOneShot(audioClips[1]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(EField());
    }

    IEnumerator EField()
    {
        yield return new WaitForSeconds(5);
        for (int i = 2; i <= 11; i++)
        {
            if (i == 4)
            {
                WaveGenerator.SetActive(true);
            }
            if (i == 11)
            {
                spotLight.intensity = 1;
                spotLightTransform.LookAt(PolarizationScreenLocation);
            }
            textGo = StartCoroutine(TextScrolling(tutorialText[i]));
            audioSource.PlayOneShot(audioClips[i]);
            textIndex++;
            while (textIsScrolling || audioSource.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForSeconds(5);
        }
        StartCoroutine(Beta());
    }

    IEnumerator Beta()
    {
        inBeta = true;
        spotLight.intensity = 0;
        textGo = StartCoroutine(TextScrolling(tutorialText[12]));
        audioSource.PlayOneShot(audioClips[12]);
        textIndex++;
        while(textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);

        spotLight.intensity = 2.5f;
        spotLightTransform.LookAt(BetaLocation);
        spotLight.spotAngle = 7;
        textGo = StartCoroutine(TextScrolling(tutorialText[13])); //Select Beta
        audioSource.PlayOneShot(audioClips[13]);
        textIndex++;
        while (textIsScrolling || betaPushed == false || audioSource.isPlaying) // keep looping until the text is done printing and the player has interacted with beta
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        spotLight.intensity = 0;
        inBeta = false;
        StartCoroutine(Omega());
        
    }

    IEnumerator Omega()
    {
        inOmega = true;
        spotLight.intensity = 2.5f;
        spotLightTransform.LookAt(OmegaLocation);
        spotLight.spotAngle = 7;
        textGo = StartCoroutine(TextScrolling(tutorialText[14]));
        audioSource.PlayOneShot(audioClips[14]);
        textIndex++;
        while (textIsScrolling || omegaPushed == false || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);


        inOmega = false;
        StartCoroutine(Phi());
    }

    IEnumerator Phi()
    {
        inPhi = true;
        spotLight.intensity = 2.5f;
        spotLightTransform.LookAt(PhiLocation);
        spotLight.spotAngle = 7;
        textGo = StartCoroutine(TextScrolling(tutorialText[15]));
        audioSource.PlayOneShot(audioClips[15]);
        textIndex++;
        while (textIsScrolling || phiPushed == false || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        textGo = StartCoroutine(TextScrolling(tutorialText[16]));
        audioSource.PlayOneShot(audioClips[16]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        inPhi = false;
        StartCoroutine(XY());
    }

    IEnumerator XY()
    {
        inXY = true;
        spotLight.intensity = 2;
        spotLightTransform.LookAt(XYLocation);
        spotLight.spotAngle = 16;
        textGo = StartCoroutine(TextScrolling(tutorialText[17]));
        audioSource.PlayOneShot(audioClips[17]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        textGo = StartCoroutine(TextScrolling(tutorialText[18]));
        audioSource.PlayOneShot(audioClips[18]);
        textIndex++;
        while (textIsScrolling || xyPushed == false || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        inXY = false;
        StartCoroutine(B());
    }

    IEnumerator B()
    {
        inB = true;
        spotLight.intensity = 1.5f;
        spotLightTransform.LookAt(BLeverLocation);
        spotLight.spotAngle = 14;
        textGo = StartCoroutine(TextScrolling(tutorialText[19]));
        audioSource.PlayOneShot(audioClips[19]);
        textIndex++;
        while (textIsScrolling || bPushed == false || audioSource.isPlaying) // This one isn't working, will just put a longer delay on it
        {
            yield return null;
        }
        yield return new WaitForSeconds(4);
        inB = false;
        StartCoroutine(DirectionLever());
    }

    IEnumerator DirectionLever()
    {
        inDir = true;
        spotLight.intensity = 2;
        spotLightTransform.LookAt(DirectionLeverLocation);
        spotLight.spotAngle = 9;
        textGo = StartCoroutine(TextScrolling(tutorialText[20]));
        audioSource.PlayOneShot(audioClips[20]);
        textIndex++;
        while (textIsScrolling || dirPushed == false || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        inDir = false;
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial()
    {
        spotLight.intensity = 0;
        textGo = StartCoroutine(TextScrolling(tutorialText[21]));
        audioSource.PlayOneShot(audioClips[21]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);
        textGo = StartCoroutine(TextScrolling(tutorialText[22]));
        audioSource.PlayOneShot(audioClips[22]);
        textIndex++;
        while (textIsScrolling || audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);
    }

    

    public void BetaPushed()
    {
        if (inBeta)
        {
            betaPushed = true;
        }
    }

    public void OmegaPushed()
    {
        if (inOmega)
        {
            omegaPushed = true;
        }
    }

    public void PhiPushed()
    {
        if (inPhi)
        {
            phiPushed = true;
        }
    }

    public void XYPushed()
    {
        if (inXY)
        {
            xyPushed = true;
        }
    }

    public void BPushed()
    {
        if (inB)
        {
            bPushed = true;
        }
    }

    public void DirPushed()
    {
        if (inDir)
        {
            dirPushed = true;
        }
    }
}
