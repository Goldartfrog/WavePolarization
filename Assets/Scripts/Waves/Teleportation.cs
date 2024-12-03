using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private GameObject player;
    private GameObject teleportPlacesHolder;
    private GameObject animations;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotation;
    void Start()
    {
        // grabs and stores the player object
        // find is expesive, only use it on start or rarely called functions
        this.player = GameObject.Find("XR Origin");
        // grab the parent of all the teleportation canvases
        this.teleportPlacesHolder = this.transform.parent.gameObject;
        animations = this.transform.GetChild(1).gameObject;
    }
    
    // function to teleport the player to the location of the button that was clicked
    public void Teleport() {
        Debug.Log("button clicked");
        // apparently all the offsets that come before cancel out except for a -6 in the x direction
        this.player.transform.position = this.transform.position + this.offset;
        this.player.transform.eulerAngles = new Vector3(this.player.transform.rotation.x, this.rotation, this.player.transform.rotation.z);
        foreach (Transform child in teleportPlacesHolder.transform)
        {
            child.gameObject.active = true;
        }
        this.transform.gameObject.active = false;
    }
}
