using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour {
    Transform trans;
    Vector3 Position;
    public float div;
    private float time;
    private float y;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        Position = trans.localPosition;
        time = 0;
        y = Position.y;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime*1.5f;
        Position.y = y + Mathf.Sin(time)/div + 0.1f;
        trans.localPosition = Position;
	}
}
