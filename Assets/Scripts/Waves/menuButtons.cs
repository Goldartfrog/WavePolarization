using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform canvas;
    public GameObject holder;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartScene(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name ,LoadSceneMode.Single);
    }

    public void toGallery(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Gallery_Start Scene", LoadSceneMode.Single);
    }

    public void resumeScene(){
        canvas.gameObject.SetActive(false);
        //holder.GetComponent<returnToGallery>().paused = false;
        Time.timeScale = 1;
    }
}
