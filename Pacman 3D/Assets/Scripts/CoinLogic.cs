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
        transform.Rotate(15 * Time.deltaTime * 3, 
            30 * Time.deltaTime * 3, 45 * Time.deltaTime * 3);
    }
}
