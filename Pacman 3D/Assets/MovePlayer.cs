using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
    public GameObject platformCube;
	// Use this for initialization
    private Vector3 currentSpeed;
    private Vector3 platformCenter;
    private Vector3 radiusCenter;
    private Rigidbody rb;

	void Start () {
        currentSpeed = new Vector3(0.0f, 0.0f, 0.0f);
        platformCenter = platformCube.transform.position;
        rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
        radiusCenter = platformCenter - transform.position;
        if (Mathf.Abs(radiusCenter.x) > Mathf.Abs(radiusCenter.y)){
            if (Mathf.Abs(radiusCenter.x) > Mathf.Abs(radiusCenter.z))
            {
                //plano x
                rb.AddForce(new Vector3(radiusCenter.x, 0, 0), ForceMode.Acceleration); //Physics.gravity
                if (radiusCenter.x > 0) transform.rotation = Quaternion.Euler(0, 0, 90);
                else transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                rb.AddForce(new Vector3(0, 0, radiusCenter.z), ForceMode.Acceleration); //plano z
                if (radiusCenter.z > 0) transform.rotation = Quaternion.Euler(-90, 0, 0);
                else transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
        else if (Mathf.Abs(radiusCenter.y) > Mathf.Abs(radiusCenter.z))
        {
            rb.AddForce(new Vector3(0, radiusCenter.y, 0), ForceMode.Acceleration); //Plano y
            //transform.rotation.SetLookRotation(new Vector3(0, 1, 0), new Vector3(0, radiusCenter.y, 0));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else {
            rb.AddForce(new Vector3(0, 0, radiusCenter.z), ForceMode.Acceleration); //plano z
            if (radiusCenter.z > 0) transform.rotation = Quaternion.Euler(-90, 0, 0);
            else transform.rotation = Quaternion.Euler(90, 0, 0);
        }


        /*Renderer rend = platformCube.GetComponent<Renderer>();
        Vector3 center = rend.bounds.center;*/
    }

	// Update is called once per frame
	void FixedUpdate () {
        //x move
        if (Input.GetKey(KeyCode.LeftArrow)){
            currentSpeed.x = -(10f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentSpeed.x = 10f;
        }
        else currentSpeed.x = 0;

        //z moves (vertical)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed.z = -(10f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed.z = 10f;
        }
        else currentSpeed.z = 0;

        //move packman
        this.transform.Translate(Time.fixedDeltaTime * currentSpeed.x, Time.fixedDeltaTime * currentSpeed.y,
            Time.fixedDeltaTime * currentSpeed.z);
	}
}
