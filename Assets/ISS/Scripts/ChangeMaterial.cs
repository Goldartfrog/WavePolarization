using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    public bool startOnEnable = false;
    public float changeAfterTime = 2f;

    public Texture2D emissionStartTransitionTexture;
    public Texture2D emissionEndTransitionTexture;

    public Renderer thisMaterial;

    private void OnEnable()
    {
        thisMaterial = GetComponent<Renderer>();        

        if (startOnEnable)
        {
            StartCoroutine(TranstionEmissionImages());
        }
    }

    IEnumerator TranstionEmissionImages()
    {
        while (true)
        {
            thisMaterial.material.SetTexture("_EmissionMap", emissionStartTransitionTexture);
            yield return new WaitForSeconds(changeAfterTime);
            thisMaterial.material.SetTexture("_EmissionMap", emissionEndTransitionTexture);
            yield return new WaitForSeconds(changeAfterTime);
        }
    }    
}

//thisMaterial.material.SetColor("_EmissionColor", Color.blue);