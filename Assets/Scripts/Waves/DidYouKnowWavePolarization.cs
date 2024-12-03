using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidYouKnowWavePolarization : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject didYouKnow;
    //public GameObject numFoundCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onCloseClicked()
    {
        didYouKnow.SetActive(false);
        //numFoundCanvas.SetActive(false);
    }

    public void onAppear()
    {
        didYouKnow.SetActive(true);
        //numFoundCanvas.SetActive(true);
        //numFoundCanvas.transform.position = new Vector3(didYouKnow.transform.position.x, didYouKnow.transform.position.y + 0.82f, didYouKnow.transform.position.z);
        //numFoundCanvas.transform.rotation = Quaternion.Euler(numFoundCanvas.transform.eulerAngles.x, didYouKnow.transform.eulerAngles.y, numFoundCanvas.transform.eulerAngles.z);
    }
}
