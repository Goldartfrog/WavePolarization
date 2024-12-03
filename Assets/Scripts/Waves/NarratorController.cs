using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorController : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    [SerializeField] AudioClip GameStartAnnouncement;
    [SerializeField] AudioClip GameFailedAnnouncement;
    [SerializeField] AudioClip GameSuccessAnnouncement;
    [SerializeField] AudioClip OneMinWarning;
    [SerializeField] AudioClip TwoMinWarning;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStartingTalk() {
        audioSource.PlayOneShot(GameStartAnnouncement);
    }

    public void Play2MinWarning()
    {
        audioSource.PlayOneShot(TwoMinWarning);
    }

    public void Play1MinWarning()
    {
        audioSource.PlayOneShot(OneMinWarning);
    }
    public void PlayGameSuccess()
    {
        audioSource.PlayOneShot(GameSuccessAnnouncement);
    }
}
