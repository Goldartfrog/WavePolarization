using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Display_Anim : MonoBehaviour {

	public GameObject GO_01;
	public GameObject GO_02;
	public GameObject GO_03;
	public GameObject GO_04;
	public GameObject GO_05;
	public GameObject GO_06;
	public GameObject GO_07;
	public GameObject GO_08;

	void Start () {

		StartCoroutine(Disp());
		GO_01.GetComponent<MeshRenderer> ().enabled = false;
		GO_02.GetComponent<MeshRenderer> ().enabled = false;
		GO_03.GetComponent<MeshRenderer> ().enabled = false;
		GO_04.GetComponent<MeshRenderer> ().enabled = false;
		GO_05.GetComponent<MeshRenderer> ().enabled = false;
		GO_06.GetComponent<MeshRenderer> ().enabled = false;
		GO_07.GetComponent<MeshRenderer> ().enabled = false;
		GO_08.GetComponent<MeshRenderer> ().enabled = false;
	}

	IEnumerator Disp()
	{
		while(true)
		{
			GO_01.GetComponent<MeshRenderer> ().enabled = true;
			GO_02.GetComponent<MeshRenderer> ().enabled = false;
			GO_03.GetComponent<MeshRenderer> ().enabled = true;
			GO_04.GetComponent<MeshRenderer> ().enabled = false;
			GO_05.GetComponent<MeshRenderer> ().enabled = true;
			GO_06.GetComponent<MeshRenderer> ().enabled = false;
			GO_07.GetComponent<MeshRenderer> ().enabled = true;
			GO_08.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(1.0f);
			GO_01.GetComponent<MeshRenderer> ().enabled = false;
			GO_02.GetComponent<MeshRenderer> ().enabled = true;
			GO_03.GetComponent<MeshRenderer> ().enabled = false;
			GO_04.GetComponent<MeshRenderer> ().enabled = true;
			GO_05.GetComponent<MeshRenderer> ().enabled = false;
			GO_06.GetComponent<MeshRenderer> ().enabled = true;
			GO_07.GetComponent<MeshRenderer> ().enabled = false;
			GO_08.GetComponent<MeshRenderer> ().enabled = true;
			yield return new WaitForSeconds(1.0f);
			yield return null;
	}
}
}
