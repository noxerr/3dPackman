using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    public bool canBeEaten;
    public float canBeEatenTime = 7.0f;
    private MonoBehaviour regularIA;
    private MonoBehaviour fleeIA;
    private float timeSinceLastEaten;
	// Use this for initialization
	void Start () {
        canBeEaten = false;
        regularIA = gameObject.GetComponent<IASiguePac>();
        if (!regularIA.enabled) regularIA = gameObject.GetComponent<IAPatrullaPuntos>();
        if (regularIA == null | !regularIA.enabled) regularIA = gameObject.GetComponent<IAPredicePac>();
        fleeIA = gameObject.GetComponent<IAFlee>();
        fleeIA.enabled = false;
        timeSinceLastEaten = 0.0f;
        //falta flee IA
	}
    public void startEatenTimer() {
        timeSinceLastEaten = Time.time;
    }
	// Update is called once per frame
	void Update () {
        float d2 = Time.time;
        if (d2 - timeSinceLastEaten > canBeEatenTime) canBeEaten = false;
        if (canBeEaten)
        {
            regularIA.enabled = false;
            fleeIA.enabled = true;
        }
        else {
            regularIA.enabled = true;
            fleeIA.enabled = false;
        }
    }
}
