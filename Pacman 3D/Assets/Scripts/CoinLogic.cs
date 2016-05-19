using UnityEngine;
using System.Collections;

public class CoinLogic : MonoBehaviour {
    //public GameObject player;
    // Use this for initialization
    
    public float vollowRange = 0.5f;
    public float volhighRange = 1.0f;
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 3);
    }
}
