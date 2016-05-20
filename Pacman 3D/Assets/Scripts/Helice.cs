using UnityEngine;
using System.Collections;

public class Helice : MonoBehaviour {
    Transform trans;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("posParent: " + transform.parent.parent.position);
        Debug.Log("pos: " + transform.position);
        Debug.Log("posLocal: " + transform.localPosition);
        trans.RotateAround(transform.parent.parent.position - new Vector3(0,0,-4.5f), new Vector3(0, 1, 0), 2);
	}
}
