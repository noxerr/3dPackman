using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setScore : MonoBehaviour {
    public Text countText;
    private int count;
    // Use this for initialization
    void Start () {

        count = PlayerPrefs.GetInt("score", count);
    }
	
	// Update is called once per frame
	void Update () {

        countText.text = "Score: " + count.ToString();
    }
}
