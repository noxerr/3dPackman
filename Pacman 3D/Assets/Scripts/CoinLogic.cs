using UnityEngine;
using System.Collections;

public class CoinLogic : MonoBehaviour {
    //public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 3);

    }
    /*void OnTriggerEnter(Collision col)
    {
        if (col.gameObject.name == player.gameObject.name) {
            Destroy(this.gameObject);
        }
    }*/
}
