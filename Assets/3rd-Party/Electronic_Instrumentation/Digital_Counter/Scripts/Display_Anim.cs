using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Anim : MonoBehaviour {

	public GameObject Display_01;
	public GameObject Display_02;
	public GameObject Display_03;
	public GameObject Display_04;
	public GameObject Display_05;
	public GameObject Display_06;
	public GameObject Display_07;
	public GameObject Display_08;
	public GameObject Display_09;

	void Start () {

		StartCoroutine(Disp());
		Display_01.GetComponent<MeshRenderer> ().enabled = false;
		Display_02.GetComponent<MeshRenderer> ().enabled = false;
		Display_03.GetComponent<MeshRenderer> ().enabled = false;
		Display_04.GetComponent<MeshRenderer> ().enabled = false;
		Display_05.GetComponent<MeshRenderer> ().enabled = false;
		Display_06.GetComponent<MeshRenderer> ().enabled = false;
		Display_07.GetComponent<MeshRenderer> ().enabled = false;
		Display_08.GetComponent<MeshRenderer> ().enabled = false;
		Display_09.GetComponent<MeshRenderer> ().enabled = false;

	}

	IEnumerator Disp()
	{
		while(true)
		{
			Display_01.GetComponent<MeshRenderer> ().enabled = true;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(1);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = true;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(1);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = true;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(1);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = true;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(1);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = true;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(0.05f);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = true;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(0.05f);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = true;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(0.05f);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = true;
			Display_09.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(4);
			Display_01.GetComponent<MeshRenderer> ().enabled = false;
			Display_02.GetComponent<MeshRenderer> ().enabled = false;
			Display_03.GetComponent<MeshRenderer> ().enabled = false;
			Display_04.GetComponent<MeshRenderer> ().enabled = false;
			Display_05.GetComponent<MeshRenderer> ().enabled = false;
			Display_06.GetComponent<MeshRenderer> ().enabled = false;
			Display_07.GetComponent<MeshRenderer> ().enabled = false;
			Display_08.GetComponent<MeshRenderer> ().enabled = false;
			Display_09.GetComponent<MeshRenderer> ().enabled = true;
			yield return new WaitForSeconds(0.25f);

			yield return null;
	}
}
}
