using UnityEngine;
using System.Collections;

public class OldMovePac : MonoBehaviour
{
    private Vector3 currentSpeed;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        currentSpeed = new Vector3(0.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //x move
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            //rb.AddForce(new Vector3(0, 0, -9.8f), ForceMode.Acceleration);
            currentSpeed.x = -(30f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentSpeed.x = 30f;
        }
        else currentSpeed.x = 0;

        //z moves (vertical)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed.z = -(30f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed.z = 30f;
        }
        else currentSpeed.z = 0;

        //apply gravity
        rb.AddForce(new Vector3(0, -19.8f, 0), ForceMode.Force);

        //move packman
        this.transform.Translate(Time.fixedDeltaTime * currentSpeed.x, Time.fixedDeltaTime * currentSpeed.y,
            Time.fixedDeltaTime * currentSpeed.z, Space.World);
    }
}
