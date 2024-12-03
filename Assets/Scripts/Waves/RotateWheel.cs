using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    Coroutine rotateCoroutine;
    public int state = 0;
    public bool isRotating = false;
    [SerializeField] private GameObject rotateSound;
    private AudioSource rotateSoundEffect;
    // 0 is circular, 1 is elliptical, 2 is vertical, 3 is diagonal, 4 is horizontal
    // rotates Antennae Wheel object around its z axis

    private void Start()
    {
        rotateSoundEffect = rotateSound.GetComponent<AudioSource>();
    }
    public void RotateTo(int position)
    {
        if (isRotating == false) {
            rotateCoroutine = StartCoroutine(RotateTheWheel(position));
        }
    }
    IEnumerator RotateTheWheel(int position) {
        isRotating = true;
        int difference = (state - position) % 5;
        if (difference < 0) {
            difference = difference + 5;
        }
        if (difference == 0) {
            isRotating = false;
            yield break;
        }
        rotateSoundEffect.Play();
        if (difference > 2) {
            // rotate counterclockwise
            int amountToRotate = 72 * (5 - difference);
            float amountRotated = 0;
            while (amountRotated < amountToRotate) {
                this.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
                amountRotated = amountRotated + rotationSpeed * Time.deltaTime;
                yield return null;
            }
        } else {
            // rotate clockwise
            int amountToRotate = 72 * difference;
            float amountRotated = 0;
            while (amountRotated < amountToRotate) {
                this.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime, Space.Self);
                amountRotated = amountRotated + rotationSpeed * Time.deltaTime;
                yield return null;
            }
        }
        isRotating = false;
        state = position;
        rotateSoundEffect.Stop();
    }



}
