/*This code is intended to be added to objects
* which you would like to act as triggers when clicked on by the player
* 
* Developer(s): Ayush Garg
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemTrigger : MonoBehaviour, DragDropHandler 
{
    /// <summary>
    /// The canvas that pops up when the player clicks on the object
    /// </summary>
    [SerializeField] private GameObject DidYouKnowCanvas;

    /// <summary>
    /// The object that acts as the trigger
    /// </summary>
    private Rigidbody ObjectOfInterest;

    /// <summary>
    /// Light to draw attention to the object of interest
    /// </summary>
    [SerializeField] private GameObject HighLight;

    void Start()
    {
        this.ObjectOfInterest = this.gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Function called by the event system when the user clicks on the handle trigger
    /// </summary>
    /// <param name="isLeft"></param>
    void DragDropHandler.HandleTriggerStart(bool isLeft)
    {
        this.DidYouKnowCanvas.SetActive(true);
        if (this.ObjectOfInterest != null)
        {
            this.ObjectOfInterest.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void activateCanvas() {
        this.DidYouKnowCanvas.SetActive(true);
    }

    public void disableCanvas() {
        this.DidYouKnowCanvas.SetActive(false);
        this.HighLight.SetActive(false);
    }
    public void toggleCanvas() {
        if (this.DidYouKnowCanvas.activeSelf == true) {
            this.DidYouKnowCanvas.SetActive(false);
        } else {
            this.DidYouKnowCanvas.SetActive(true);
        }
        this.HighLight.SetActive(false);
    }

    /// <summary>
    /// Detect if clicks are no longer registering
    /// </summary>
    /// <param name="isLeft"></param>
    void DragDropHandler.HandleTriggerEnd(bool isLeft)
    {
        this.DidYouKnowCanvas.SetActive(false);
        if (this.ObjectOfInterest != null)
        {
            this.ObjectOfInterest.constraints = RigidbodyConstraints.None;
        }
        this.HighLight.SetActive(false);
    }
}
