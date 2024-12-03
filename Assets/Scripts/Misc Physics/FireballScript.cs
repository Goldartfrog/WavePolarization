using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private float time = 0f;
    private float decay = 10f;
    public bool left;
    private List<Collider> hit;
    public GameObject parentObject;
    // Update is called once per frame
    void Awake()
    {
        hit = new List<Collider>();
        int scale;
        if (left) scale = 20;
        else scale = -20;
        GetComponent<Rigidbody>().velocity = transform.right * scale;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= decay) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hit.Contains(other)) return;
        else hit.Add(other);
        if(other.tag == "Pushable" && !left)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.GetComponent<Rigidbody>().velocity * 5);
        }
        if(other.tag == "Pushable" && left)
        {
            OVRDebugConsole.Log("Left contact");
            parentObject.GetComponent<FireFireball>().pulling.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
