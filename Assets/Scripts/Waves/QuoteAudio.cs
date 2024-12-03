using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteAudio : MonoBehaviour
{

    AudioSource audioSource;            // This Will Configure the  AudioSource Component, Maxwell quote; 
                                        // Make sure to attach AdioSouce to this gameobject (StartMessage);
    private GameObject dismiss;         // Gameobject displays "Press A to dismiss".

    // Start is called before the first frame update
    // The Maxwell quote will be played once in this scene.
    void Start()
    {
        dismiss = GameObject.Find("Dismiss");           // Deactivate the game object so the player will not see it before the audio ends.
        dismiss.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();                             // Play the audio.
        Debug.Log("played");
    }

    void Update()
    {
        if (!audioSource.isPlaying)                    // Check if audio is finished.
        {                                              // If it is finished, display the dismiss text.
            dismiss.SetActive(true);
        }
    }


}
