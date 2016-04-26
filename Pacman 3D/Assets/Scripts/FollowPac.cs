using UnityEngine;
using System.Collections;

public class FollowPac : MonoBehaviour {
    public GameObject pacman;
    
    Vector3 relativePos;

    void LateUpdate()
    {
        /*this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);*/
        transform.position = pacman.transform.position + relativePos;
        /*transform.position.Set(target.transform.position.x + relativePos.x, transform.position.y, 
            target.transform.position.z + relativePos.z);*/
        //transform.position.z = target.transform.position.z + relativePos.z;
        //Debug.Log(relativePos);
        //Debug.Log(offsets);
    }

	// Use this for initialization
	void Start () {
        //offsets = new Vector3(xOffset, yOffset, zOffset);
        relativePos = transform.position - pacman.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
