using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer_WP : MonoBehaviour
{
    /*This code is intended to be added to objects
     * which you would like to rotate to face the player
     * 
     * Developer(s): Cain Gonzalez
     */
    public Transform player; //the player
    Quaternion defaultRotation;//the original rotation of the object

    private void Start()
    {
        defaultRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        //gets the position of the player, but keeps the y height
        //the same as the object to prevent it from tilting
        Vector3 playerPos = new Vector3(player.position.x,
                                        this.transform.position.y,
                                        player.position.z);
        transform.LookAt(playerPos);
        //this if statement fixes the issue of the object rotating into the wall
        //when the player is to the left of them
        if (transform.rotation.y > defaultRotation.y && gameObject.CompareTag("Quiz"))
        {
            transform.rotation = defaultRotation;
        }
    }
}
