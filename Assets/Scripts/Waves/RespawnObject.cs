using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    //
    [HideInInspector]
    public bool hasMoved = false;
    public float range = 0.25f;
    //
    public GameObject PrefabInstantiate;
    //public GameObject respawnEffect;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMoved)
            return;

        float distance = Vector3.Distance(initialPosition, transform.position);
        if (distance > range && !hasMoved)
        {
            Respawn();
            hasMoved = true;
        }
    }

    public void Respawn()
    {
        Instantiate(PrefabInstantiate, initialPosition, initialRotation);

        //GameObject effect = (GameObject)Instantiate(respawnEffect, initialPosition, initialRotation);
        //Destroy(effect, 2f);
    }
}
