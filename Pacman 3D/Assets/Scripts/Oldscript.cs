using UnityEngine;
using System.Collections;

public class Oldscript : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKey (KeyCode.UpArrow))
			transform.rotation = transform.rotation * (Quaternion.AngleAxis(1, new Vector3(1.0f, 0.0f, 0.0f)));
		if (Input.GetKey (KeyCode.DownArrow))
			transform.rotation = transform.rotation * (Quaternion.AngleAxis(-1, new Vector3(1.0f, 0.0f, 0.0f)));

		if (Input.GetKey (KeyCode.LeftArrow))
			transform.rotation = transform.rotation * (Quaternion.AngleAxis(1, new Vector3(0.0f, 1.0f, 0.0f)));
		if (Input.GetKey (KeyCode.RightArrow))
			transform.rotation = transform.rotation * (Quaternion.AngleAxis(-1, new Vector3(0.0f, 1.0f, 0.0f)));
		*/
		
		if (Input.GetKey (KeyCode.UpArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.right, 0.3f);
		if (Input.GetKey (KeyCode.DownArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.right, -0.3f);
	
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), 0.3f);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), -0.3f);
		
		/*if (Input.GetKey (KeyCode.UpArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), 1);
		if (Input.GetKey (KeyCode.DownArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), -1);

		if (Input.GetKey (KeyCode.LeftArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.up, 1);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.up, -1);*/
		
		/*
		if (Input.GetKey (KeyCode.UpArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.right, 1);
		if (Input.GetKey (KeyCode.DownArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.right, -1);

		if (Input.GetKey (KeyCode.LeftArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.up, 1);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), transform.up, -1);
		*/
	}
}
