using UnityEngine;
using System.Collections;

public class OldCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime*3);
    }

}
