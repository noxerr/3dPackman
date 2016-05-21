using UnityEngine;
using System.Collections;

public class FollowPac : MonoBehaviour {
    public GameObject pacman;
    //private Rigidbody rb;
    public Vector3 relativePos;

    void LateUpdate()
    {
        transform.position = pacman.transform.position + relativePos;
        //Debug.Log(pacman.transform.position);
        //Debug.Log("RB: " + rb.position); 
    }

	// Use this for initialization
	void Start () {
        relativePos = transform.position - pacman.transform.position;
        //rb = pacman.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
