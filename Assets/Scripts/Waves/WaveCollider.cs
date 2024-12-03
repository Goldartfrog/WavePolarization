using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    int layerMask = 10;
    [SerializeField] private PropogatingWave Wave;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layerMask)
        {
            Debug.Log("Collided (wave)");

            Wave.reflectedWave();
        } 
    }

}
