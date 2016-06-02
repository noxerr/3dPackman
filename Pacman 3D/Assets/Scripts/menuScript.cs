﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuScript : MonoBehaviour {
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public Scene scene1;

	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu.enabled = false;
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }
    public void NoPress() {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    public void StartLevel() {
        SceneManager.LoadScene("scene1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
