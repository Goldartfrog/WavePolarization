using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public Transform canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
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
        canvas.gameObject.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetUp(OVRInput.RawButton.Y)){
            if(!paused){
                Debug.Log("Pause");
                canvas.gameObject.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }else if(paused){
                Debug.Log("unPause");
                canvas.gameObject.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
            
        }
    }
}
