using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxForce = 8;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;
        float volume = 1;
        if (force < maxForce)
        {
            volume = force / maxForce;
        }
        audioSource.PlayOneShot(audioSource.clip, volume);
    }
}
