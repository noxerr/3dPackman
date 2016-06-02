using UnityEngine;
using System.Collections;

public class sonidosPrincipal : MonoBehaviour {

    public AudioClip menuMusic;
    private AudioSource source;
    // Use this for initialization
    void Start () {

        source = GetComponent<AudioSource>();

        source.PlayOneShot(menuMusic, 1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
