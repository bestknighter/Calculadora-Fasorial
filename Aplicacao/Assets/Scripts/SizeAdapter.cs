using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(LayoutElement)), ExecuteInEditMode]
public class SizeAdapter : MonoBehaviour {

	[Range(0f,100f)]
	public float percentage = 60f;

	public void Awake () {
		GetComponent<LayoutElement> ().minHeight = Screen.height*percentage/100f;
	}

}
