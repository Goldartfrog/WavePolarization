using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    //public GameObject destroyEffect;

    void OnTriggerEnter(Collider colliderInfo)
    {
        if (colliderInfo.gameObject.tag != "PointCharge")
            return;

        RespawnObject respawnObject = colliderInfo.gameObject.GetComponent<RespawnObject>();
        if (!respawnObject.hasMoved)
        {
            respawnObject.Respawn();
        }

        Destroy(colliderInfo.gameObject);

        //GameObject effect = (GameObject)Instantiate(destroyEffect, colliderInfo.transform.position, colliderInfo.transform.rotation);
        //Destroy(effect, 2f);
    }
}
