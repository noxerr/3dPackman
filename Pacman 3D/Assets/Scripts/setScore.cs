using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setScore : MonoBehaviour {
    public Text countText;
    private int count;

    public AudioClip menuMusic;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();

        source.PlayOneShot(menuMusic, 1f);
        count = PlayerPrefs.GetInt("score", count);
    }
	
	// Update is called once per frame
	void Update () {

        countText.text = "Score: " + count.ToString();
    }
}
