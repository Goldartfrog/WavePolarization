using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Lamp_2_3_Anim_V : MonoBehaviour {

	public GameObject Lamp;

	void Start () {

		StartCoroutine(Disp());
		Lamp.GetComponent<MeshRenderer> ().enabled = false;
	}

	IEnumerator Disp()
	{
		while(true)
		{
			Lamp.GetComponent<MeshRenderer> ().enabled = true;
			yield return new WaitForSeconds(0.3f);
			Lamp.GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds(0.3f);
			yield return null;
	}
}
}
