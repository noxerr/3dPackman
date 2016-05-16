using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
    private Transform[] childs;
    private float elapsedTime;

	// Use this for initialization
	void Start () {
        childs = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        childs[1].position = new Vector3(transform.position.x ,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime*3) * 3);
        childs[5].position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime * 3) * 3);
        childs[22].position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime * 3) * 3);
        Debug.Log("Cosin: " + Mathf.Cos(elapsedTime));
        Debug.Log("Time: " + elapsedTime);  
        Debug.Log(childs[1].localPosition.x);
        Debug.Log(childs[1].name);
	}
}
