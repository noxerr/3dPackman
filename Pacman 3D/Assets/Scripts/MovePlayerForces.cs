using UnityEngine;
using System.Collections;

public class MovePlayerForces : MonoBehaviour {
    private Rigidbody rb;
    public float potensia = 50;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //rb.AddForce(new Vector3(0, 0, -9.8f), ForceMode.Acceleration);
            rb.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * potensia);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * potensia);
        }
        else //currentSpeed.x = 0;

        //z moves (vertical)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //currentSpeed.z = -(30f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //currentSpeed.z = 30f;
        }
        else //currentSpeed.z = 0;
    }
}
