using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class returnToGallery : MonoBehaviour
{
    private bool paused = false;
    public Transform canvas;
    //public GameObject cameraRig;
    //public GameObject cammy;
    private UnityAction action1;

    private GameObject view;
    private GameObject createdCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //view = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        //createdCanvas = new GameObject();  
        //createdCanvas.transform.parent = view.transform;
        //createCanvas();
        //createdCanvas.transform.rotation = transform.parent.rotation;
        //Vector3 temp = createdCanvas.transform.position;
        //createdCanvas.transform.localPosition = new Vector3(0, 0, 0.5f);
        //createdCanvas.SetActive(false);
        canvas.gameObject.SetActive(false);
        //createdCanvas.SetActive(false);
    }

    void createCanvas(){
        //returnToGallery script = cameraRig.GetComponent<returnToGallery>();
        createdCanvas.name = "PauseCanvas";
        createdCanvas.AddComponent<RectTransform>();
        createdCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(300,300);
        createdCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0.5f);
        createdCanvas.GetComponent<RectTransform>().localScale = new Vector3(0.002f,0.002f,0.002f);
        createdCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
        createdCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0,0);
        Canvas canvas = createdCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler scaler = createdCanvas.AddComponent<CanvasScaler>();
        scaler.scaleFactor = 1.0f;
        scaler.dynamicPixelsPerUnit = 1.0f;
        scaler.referencePixelsPerUnit = 100.0f;
        GraphicRaycaster graphics = createdCanvas.AddComponent<GraphicRaycaster>();
        graphics.ignoreReversedGraphics = true;
        //createdCanvas.AddComponent<EventSystem>();
        //createdCanvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        //createdCanvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

        GameObject backgroundCanvas = new GameObject();
        backgroundCanvas.transform.parent = createdCanvas.transform;
        backgroundCanvas.AddComponent<RectTransform>();
        backgroundCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(2700,2300);
        backgroundCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0,-21,0);
        backgroundCanvas.GetComponent<RectTransform>().localScale = new Vector3(0.1f,0.1f,1);
        Image background = backgroundCanvas.AddComponent<Image>();
        background.color = Color.gray;

        GameObject resumeButton = new GameObject();
        resumeButton.transform.parent = backgroundCanvas.transform;
        resumeButton.AddComponent<RectTransform>();
        resumeButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,300);
        resumeButton.GetComponent<RectTransform>().localPosition = new Vector3(0,500,-1);
        resumeButton.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        resumeButton.AddComponent<BoxCollider>();
        resumeButton.GetComponent<BoxCollider>().size = new Vector3(350, 300, 2);
        Image resumeButtonImage = resumeButton.AddComponent<Image>();
        resumeButtonImage.color = Color.blue;
        Button actualResumeButton = resumeButton.AddComponent<Button>();
        //actualResumeButton.onClick.AddListener(resumeScene);

        //UnityAction<GameObject> action = new UnityAction<GameObject>(((a) => {resumeScene()};));
        //UnityAction<GameObject> action = new UnityAction<GameObject>(resumeScene);
        //UnityAction<GameObject> action += new UnityAction<GameObject>(resumeScene);
        //action1 += resumeScene;
        //actualResumeButton.onClick.AddListener(action1);
        //UnityAction<GameObject> action = new UnityAction<GameObject>(this.resumeScene);
        //UnityEventTools.AddObjectPersistentListener<GameObject>(actualResumeButton.onClick, action, resumeButton);
        var methodInfo = UnityEvent.GetValidMethodInfo(this, nameof(resumeScene), new System.Type[0]);
        UnityAction methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction), this, methodInfo) as UnityAction;

#if UNITY_EDITOR
        UnityEventTools.AddPersistentListener(actualResumeButton.onClick, methodDelegate);
#endif

        GameObject resumeText = new GameObject();
        resumeText.transform.parent = resumeButton.transform;
        resumeText.AddComponent<RectTransform>();
        resumeText.AddComponent<Text>();
        resumeText.GetComponent<Text>().text = "Resume";
        resumeText.GetComponent<Text>().fontSize = 140;
        resumeText.GetComponent<Text>().fontStyle = FontStyle.Bold;
        resumeText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        resumeText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        resumeText.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        resumeText.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
        resumeText.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
        resumeText.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        SetLeft(resumeText.GetComponent<RectTransform>(), 0);
        SetRight(resumeText.GetComponent<RectTransform>(), 0);
        SetTop(resumeText.GetComponent<RectTransform>(), 0);
        SetBottom(resumeText.GetComponent<RectTransform>(), 0);
        //SetRight(returnText.GetComponent<RectTransform>(), 0);
        //SetTop(returnText.GetComponent<RectTransform>(), 0);
        //SetBottom(returnText.GetComponent<RectTransform>(), 0);
        
        GameObject restartButton = new GameObject();
        restartButton.transform.parent = backgroundCanvas.transform;
        restartButton.AddComponent<RectTransform>();
        restartButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,300);
        restartButton.GetComponent<RectTransform>().localPosition = new Vector3(0,0,-1);
        restartButton.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        restartButton.AddComponent<BoxCollider>();
        restartButton.GetComponent<BoxCollider>().size = new Vector3(350, 300, 2);
        //restartButton.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f,0.15f);
        //restartButton.GetComponent<RectTransform>().localPosition = new Vector3(0,0,-.005f);
        Image restartButtonImage = restartButton.AddComponent<Image>();
        restartButtonImage.color = Color.blue;
        Button actualRestartButton = restartButton.AddComponent<Button>();
        actualRestartButton.onClick.AddListener(restartScene);

        GameObject restartText = new GameObject();
        restartText.transform.parent = restartButton.transform;
        restartText.AddComponent<RectTransform>();
        restartText.AddComponent<Text>();
        restartText.GetComponent<Text>().text = "Restart";
        restartText.GetComponent<Text>().fontSize = 140;
        restartText.GetComponent<Text>().fontStyle = FontStyle.Bold;
        restartText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        restartText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        restartText.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        restartText.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
        restartText.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
        restartText.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

        GameObject returnButton = new GameObject();
        returnButton.transform.parent = backgroundCanvas.transform;
        returnButton.AddComponent<RectTransform>();
        returnButton.AddComponent<BoxCollider>();
        returnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,300);
        returnButton.GetComponent<RectTransform>().localPosition = new Vector3(0,-500,-1);
        returnButton.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        returnButton.GetComponent<BoxCollider>().size = new Vector3(350, 300, 2);
        //returnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f,0.15f);
        //returnButton.GetComponent<RectTransform>().localPosition = new Vector3(0,-.3f,-.005f);
        Image returnButtonImage = returnButton.AddComponent<Image>();
        returnButtonImage.color = Color.blue;
        Button actualReturnButton = returnButton.AddComponent<Button>();
        actualReturnButton.onClick.AddListener(delegate {toGallery();});

        GameObject returnText = new GameObject();
        returnText.transform.parent = returnButton.transform;
        returnText.AddComponent<RectTransform>();
        returnText.AddComponent<Text>();
        returnText.GetComponent<Text>().text = "Gallery";
        returnText.GetComponent<Text>().fontSize = 140;
        returnText.GetComponent<Text>().fontStyle = FontStyle.Bold;
        returnText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        returnText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        returnText.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        returnText.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
        returnText.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
        returnText.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        //SetLeft(returnText.GetComponent<RectTransform>(), 0);
        //SetRight(returnText.GetComponent<RectTransform>(), 0);
        //SetTop(returnText.GetComponent<RectTransform>(), 0);
        //SetBottom(returnText.GetComponent<RectTransform>(), 0);
    } 

    void SetLeft(RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
    void SetRight(RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    void SetTop(RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    void SetBottom(RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public void restartScene(){
        Debug.Log("Restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name ,LoadSceneMode.Single);
    }

    public void toGallery(){
        Debug.Log("Leave");
        Time.timeScale = 1;
        SceneManager.LoadScene("Gallery_Start Scene", LoadSceneMode.Single);
    }

    public void resumeScene(){
        Debug.Log("Resume");
        //createdCanvas.gameObject.SetActive(false);
        //holder.GetComponent<returnToGallery>().paused = false;
        canvas.gameObject.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    public void exitGame(){
        Debug.Log("Exit Game");
        Time.timeScale = 1;
        //will quit now even when testing in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //Actual quitting of game when not editor
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
       /* if(OVRInput.GetUp(OVRInput.RawButton.A)){
            Debug.Log("A");
            if(paused){
                Time.timeScale = 1;
                SceneManager.LoadScene("Gallery_Start Scene", LoadSceneMode.Single);
            }

        }

        if(OVRInput.GetUp(OVRInput.RawButton.B)){
            Debug.Log("B");
            if(paused){
                canvas.gameObject.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
        }

        if(OVRInput.GetUp(OVRInput.RawButton.X)){
            Debug.Log("X");
            if(paused){
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name ,LoadSceneMode.Single);
            }
        }*/

        if(OVRInput.GetUp(OVRInput.RawButton.Y)){
            if(!paused){
                Debug.Log("Pause");
                //createdCanvas.SetActive(true);
                canvas.gameObject.SetActive(true);
                //----showCanvas();
                //canvas.gameObject.transform.position = cammy.transform.position + (cammy.transform.forward * 1.5);
                paused = true;
                Time.timeScale = 0;
            }else if(paused){
                Debug.Log("unPause");
                //createdCanvas.SetActive(false);
                canvas.gameObject.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
            
        }
    }
}
