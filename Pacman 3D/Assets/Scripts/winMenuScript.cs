using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class winMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame

    public void pasaLevel()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void resetLevel()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void menuPrincipal()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
