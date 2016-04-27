﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
    private Rigidbody rb;
    public float constante = 2f;
    private bool flechasLados, flechasRectas;
    private bool colisionSuelo, colisionRampa;
    private int count;
    public Text countText;
    public Text winText;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        flechasLados = false;
        flechasRectas = false;
        colisionSuelo = true;
        colisionRampa = false;
        count = 0;
        SetCountText();
        winText.text = "";
        GetComponent<Collider>().material.staticFriction = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        if (colisionSuelo == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                flechasLados = true;
                rb.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                flechasLados = true;
                rb.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
            }
            else if (flechasLados == true) //PARA QUE FRENE MAS RAPIDO
            {
                rb.AddForce(new Vector3(-rb.velocity.x * 0.7f, 0, 0), ForceMode.VelocityChange);
                flechasLados = false;
            }



            //z moves (vertical)
            if (Input.GetKey(KeyCode.DownArrow))
            {
                flechasRectas = true;
                rb.AddForce(new Vector3(0.0f, 0.0f, -1.0f) * constante, ForceMode.VelocityChange);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                flechasRectas = true;
                rb.AddForce(new Vector3(0.0f, 0.0f, 1.0f) * constante, ForceMode.VelocityChange);
            }
            else if (flechasRectas == true) //PARA QUE FRENE MAS RAPIDO
            {
                rb.AddForce(new Vector3(0, 0, -rb.velocity.z * 0.7f), ForceMode.VelocityChange);
                flechasRectas = false;
            }
        }


        rb.AddForce(new Vector3(0.0f, -30.0f, 0.0f)); //gravedad aumentada
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
