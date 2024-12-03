using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkinmatized : MonoBehaviour
{

    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
