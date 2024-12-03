using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismAnimator : MonoBehaviour
{
    public Vector2 animSpeed = new Vector2(0.0f, 1.0f);
    
    Renderer rend;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaOffset = (Time.timeSinceLevelLoad ) * animSpeed;
        rend.material.SetTextureOffset("_MainTex", deltaOffset);
    }
}
