using UnityEngine;
using System.Collections;

public class FollowPac : MonoBehaviour {
    public GameObject target;
    /*public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;
    Vector3 offsets;*/
    Vector3 relativePos;

    void LateUpdate()
    {
        /*this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);*/
        //transform.position = target.transform.position + offsets;
        /*transform.position.Set(target.transform.position.x + relativePos.x, transform.position.y, 
            target.transform.position.z + relativePos.z);*/
        //transform.position.z = target.transform.position.z + relativePos.z;
        //Debug.Log(relativePos);
        //Debug.Log(offsets);
    }

	// Use this for initialization
	void Start () {
        //offsets = new Vector3(xOffset, yOffset, zOffset);
        relativePos = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
