using UnityEngine;
using System.Collections;

public class VariableResolutionPixel : MonoBehaviour {

	public float s_baseOrthographicSize;
	public float pixelPerUnit = 32.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		s_baseOrthographicSize = Screen.height / pixelPerUnit / 2.0f;
		Camera.main.orthographicSize = s_baseOrthographicSize;

	}
}
