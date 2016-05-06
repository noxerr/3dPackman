using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
    private Rigidbody rb;
    public float constante = 25f;
    //private bool flechasLados, flechasRectas;
    private bool colisionSuelo, colisionRampa;
    private int count;
    public Text countText;
    public Text winText;
    private Vector3 velocidad;
    private float gradosDireccion, oldGradosDireccion;
    //private Transform papa;
    //private Vector3 transformOffset;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //flechasLados = false;
        //flechasRectas = false;
        colisionSuelo = false;
        colisionRampa = false;
        count = 0;
        SetCountText();
        winText.text = "";
        GetComponent<Collider>().material.staticFriction = 0.0f;
        gradosDireccion = 0;
        oldGradosDireccion = 0;
        //papa = gameObject.transform.parent;
        //transformOffset = gameObject.transform.localPosition;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        if (colisionSuelo == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //flechasLados = true;
                //rb.velocity = new Vector3(-1.0f * constante, rb.velocity.y, rb.velocity.z);
                //rb.velocity = new Vector3(-1.0f * constante, 0, 0);
                //rb.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
                velocidad = new Vector3(-1.0f * constante, rb.velocity.y, 0);
                if (gradosDireccion != 90) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 90;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //flechasLados = true;
                //rb.velocity = new Vector3(1.0f * constante, rb.velocity.y, rb.velocity.z);
               // rb.velocity = new Vector3(1.0f * constante, 0, 0);
                //rb.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
                velocidad = new Vector3(1.0f * constante, rb.velocity.y, 0);
                if (gradosDireccion != 270) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 270;
            }
            /*else if (flechasLados == true) //PARA QUE FRENE MAS RAPIDO
            {
                rb.AddForce(new Vector3(-rb.velocity.x * 0.7f, 0, 0), ForceMode.VelocityChange);
                flechasLados = false;
            }*/



            //z moves (vertical)
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //flechasRectas = true;
                //rb.AddForce(new Vector3(0.0f, 0.0f, -1.0f) * constante, ForceMode.VelocityChange);
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -1.0f * constante);
                //rb.velocity = new Vector3(0, 0, -1.0f * constante);
                velocidad = new Vector3(0, rb.velocity.y, -1.0f * constante);
                if (gradosDireccion != 0) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 0;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                //flechasRectas = true;
                //rb.AddForce(new Vector3(0.0f, 0.0f, 1.0f) * constante, ForceMode.VelocityChange);
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 1.0f * constante);
                //rb.velocity = new Vector3(0, 0, 1.0f * constante);
                velocidad = new Vector3(0, rb.velocity.y, 1.0f * constante);
                if (gradosDireccion != 180) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 180;
            }
            /*else if (flechasRectas == true) //PARA QUE FRENE MAS RAPIDO
            {
                rb.AddForce(new Vector3(0, 0, -rb.velocity.z * 0.7f), ForceMode.VelocityChange);
                flechasRectas = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(0, 50, 0);
               // rb.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
            }*/
            rb.velocity = velocidad;

        }
        if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 10)
        {
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion - 360, 0) * Time.deltaTime * 3);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion + 360, 0) * Time.deltaTime * 3);
            else transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion, 0) * Time.deltaTime * 1);
        }
        if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 3)
        {
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion - 360, 0) * Time.deltaTime * 0.8f);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion + 360, 0) * Time.deltaTime * 0.8f);
            else transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion, 0) * Time.deltaTime * 1);
        }
        //Debug.Log("Euler: " + transform.eulerAngles.y + ". gradosDireccion: " + gradosDireccion + ". oldGradosDireccion: " + oldGradosDireccion);
        rb.AddForce(new Vector3(0.0f, -30.0f, 0.0f)); //gravedad aumentada

        //papa.position = transform.position - transformOffset;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pick Up") {
            Destroy(col.gameObject);
            count = count + 1;
            SetCountText();
        } 
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "Terrain") colisionSuelo = true;
            else if (contact.otherCollider.gameObject.tag == "Rampa")
            {
                colisionRampa = true;
                GetComponent<Collider>().material.dynamicFriction = 0;
            }
        } 
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Terrain") colisionSuelo = false;
        else if (collisionInfo.gameObject.tag == "Rampa")
        {
            colisionRampa = false;
            GetComponent<Collider>().material.dynamicFriction = 0.5f;
        }
        //print("No longer in contact with " + collisionInfo.transform.name);
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 6)
        {
            winText.text = "You Win!";
        }
    }
}
