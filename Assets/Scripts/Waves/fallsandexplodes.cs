using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallsandexplodes : MonoBehaviour
{

    [SerializeField] private GameObject explosions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(-30* Time.deltaTime, 80* Time.deltaTime, -30 * Time.deltaTime); //translates units per second
        if(this.transform.position.y< -708.9075) { this.explosions.SetActive(true); }
    }

    
}
