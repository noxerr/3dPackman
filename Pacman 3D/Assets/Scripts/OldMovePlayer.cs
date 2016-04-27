using UnityEngine;
using System.Collections;

public class OldMovePlayer : MonoBehaviour
{
    public GameObject platformCube;
    public GameObject cameraScene;
	// Use this for initialization
    private Vector3 currentSpeed;
    private Vector3 platformCenter;
    private Vector3 radiusCenter;
    private Rigidbody rb;
    //private Vector3 boxLength;
    //private float width;
    //private Vector3 boxMin;
    private Vector3 newPos;

	void Start () {
        currentSpeed = new Vector3(0.0f, 0.0f, 0.0f);
        platformCenter = platformCube.transform.position;
        rb = GetComponent<Rigidbody>();
        //RectTransform rt = (RectTransform)platformCube.transform;
        //platformCube.GetComponent.<Renderer>().bounds.size
        //boxLength = platformCube.GetComponent<Renderer>().bounds.size;
        //Debug.Log(sizes);
        //platformCube.GetComponent<Renderer>().bounds.center;
        //width = boxLength.z;
      //  boxMin = platformCube.GetComponent<Renderer>().bounds.min;
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

        //cameraScene.transform.rotation = Quaternion.Euler(( 180 * (transform.position.z - boxMin.z) / width), 0, 0);
        /*newPos = new Vector3(0, Mathf.Sin(cameraScene.transform.eulerAngles.x * Mathf.Deg2Rad) * 35 + 25,
            Mathf.Sin((cameraScene.transform.eulerAngles.x-90)* Mathf.Deg2Rad) * 35);
        cameraScene.transform.position = newPos;*/
        /*Debug.Log(Mathf.Sin(cameraScene.transform.rotation.x) * 35);
        Debug.Log(cameraScene.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
        Debug.Log(Mathf.Sin(90* Mathf.Deg2Rad));*/

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
