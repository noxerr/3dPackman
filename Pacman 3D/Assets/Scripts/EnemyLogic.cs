using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    public bool canBeEaten;
    private MonoBehaviour regularIA;
    private MonoBehaviour fleeIA;
	// Use this for initialization
	void Start () {
        canBeEaten = false;
        regularIA = gameObject.GetComponent<IASiguePac>();
        if (!regularIA.enabled) regularIA = gameObject.GetComponent<IAPatrullaPuntos>();
        if (regularIA == null | !regularIA.enabled) regularIA = gameObject.GetComponent<IAPredicePac>();
        //falta flee IA
	}
	
	// Update is called once per frame
	void Update () {

        if (canBeEaten)
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 3);
            regularIA.enabled = false;
            GetComponent<NavMeshAgent>().destination = transform.position;
        }
        else regularIA.enabled = true;
    }
}
