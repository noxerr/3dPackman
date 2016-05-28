using UnityEngine;
using System.Collections;

public class AnimationPiernas : MonoBehaviour {
    private Transform[] childs;
    private float elapsedTime;
    //private Transform pivot;

	// Use this for initialization
	void Start () {
        childs = GetComponentsInChildren<Transform>();
        //pivot = transform.Find("bola");
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        /*childs[1].position = new Vector3(transform.position.x ,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime*8) * 3);
        childs[5].position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime * 8) * 3);
        childs[22].position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + Mathf.Cos(elapsedTime * 8) * 3);*/
        /*
         * elapsedTime += Time.deltaTime;
        childs[1].localPosition = new Vector3(childs[1].transform.localPosition.x,
            childs[1].transform.localPosition.y, childs[1].transform.localPosition.z + Mathf.Cos(elapsedTime * 2) * 3);
        childs[5].localPosition = new Vector3(childs[5].transform.localPosition.x,
            childs[5].transform.localPosition.y, childs[5].transform.localPosition.z + Mathf.Cos(elapsedTime * 2) * 3);
        childs[22].localPosition = new Vector3(childs[22].transform.position.x,
            childs[22].transform.localPosition.y, childs[22].transform.localPosition.z + Mathf.Cos(elapsedTime * 2) * 3);*/
        elapsedTime += Time.deltaTime;
        childs[1].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8) * 1.2f);
        childs[5].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8) * 1.2f);
        childs[22].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8) * 1.2f);

        childs[2].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8 - Mathf.PI) * 1.2f);
        childs[6].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8 - Mathf.PI) * 1.2f);
        childs[23].localPosition = new Vector3(0, 0, Mathf.Cos(elapsedTime * 8 - Mathf.PI) * 1.2f);

        //childs[14].localRotation = Quaternion.AngleAxis(90,new Vector3(0,1,0));
        //childs[14].RotateAround(pivot.position, Vector3.up, 20);
        //Debug.Log("Cosin: " + Mathf.Cos(elapsedTime));
        //Debug.Log("Time: " + elapsedTime);  
        //Debug.Log(childs[1].localPosition.x);
       // Debug.Log(childs[1].name);
	}
}
