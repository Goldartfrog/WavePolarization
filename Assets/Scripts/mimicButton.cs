using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimicButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject buttonModel;
    [SerializeField] public float secs = 1f;
    [SerializeField] public float depth = 0.01f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonlyForPickyButtons(int i = 0)
    {
        this.Buttonly();
    }

    public void Buttonly()
    {
        this.StartCoroutine(this.ActLikeButton(this.secs, this.depth));
    }

    private IEnumerator ActLikeButton(float secs , float depth )
    {
        this.buttonModel.transform.localPosition = this.buttonModel.transform.localPosition - new Vector3(0, 0, -depth);
        //foreach(Transform childSymbol in this.transform)
        //{
        //    childSymbol.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        //}
        //foreach (Renderer childRenderer in this.buttonModel.GetComponentsInChildren<Renderer>())
        //{
        //    childRenderer.material = Instantiate(Resources.Load("Material") as Material);
            //childRenderer.GetComponent().material = Instantiate(Resources.Load("Material") as Material);
            //childRenderer.sharedMaterial.color = Color.red; //Symbol.GetComponent<Renderer>()
        //    childRenderer.material.color = Color.cyan;
        //}
        yield return new WaitForSeconds(secs);
        this.buttonModel.transform.localPosition = this.buttonModel.transform.localPosition - new Vector3(0, 0, depth);
        //foreach (Renderer childRenderer in this.buttonModel.GetComponentsInChildren<Renderer>())
        //{
            //childRenderer.material.color = Color.white; //Symbol.GetComponent<Renderer>()
        //    childRenderer.material.color = Color.white;
        //}
    }

    
}
