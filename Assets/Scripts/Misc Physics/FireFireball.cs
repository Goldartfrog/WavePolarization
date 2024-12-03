using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireball : MonoBehaviour
{
    public GameObject fireball;
    public GameObject handAnchor;

    private List<GameObject> fireballs;

    private float time = 0f;
    private float decay = 100f;

    public List<Rigidbody> pulling;

    public bool left;


    private void Start()
    {
        fireballs = new List<GameObject>();
        fireball.GetComponent<FireballScript>().parentObject = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (!left && OVRInput.GetDown(OVRInput.Button.One))
        {
            fireballs.Add(Instantiate(fireball, this.transform.position, this.transform.rotation));
        }
        if (left && OVRInput.GetDown(OVRInput.Button.Four))
        {
            fireballs.Add(Instantiate(fireball, this.transform.position, this.transform.rotation));
        }
        for(int i = 0; i < pulling.Count; i++)
        {
            time += Time.deltaTime;
            if (time >= decay)
            {
                pulling.RemoveAt(i);
                if (i >= pulling.Count) break;
            }
            if (!OVRInput.Get(OVRInput.Button.Four))
            {
                pulling.Clear();
                break;
            }
            pulling[i].AddForce((transform.position - pulling[i].gameObject.transform.position).normalized);
        }
    }
}
