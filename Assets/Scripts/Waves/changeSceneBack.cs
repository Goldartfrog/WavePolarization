using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneBack : MonoBehaviour
{
    [SerializeField] private float delay;
    private float time = 30f;
    // Start is called before the first frame update
    void Start()
    {
        time = delay;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) {
            SceneManager.LoadScene("WavePolarization");
        }
    }
}
