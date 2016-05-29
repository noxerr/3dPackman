using UnityEngine;
using System.Collections;

public class Dissapear : MonoBehaviour {
    public GameObject pacman;
    public int MonedasAntesCueva;
    private PlayerLogic logica;

	// Use this for initialization
	void Start () {
        logica = pacman.GetComponent<PlayerLogic>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (logica.monedasPilladas >= MonedasAntesCueva){
            gameObject.SetActive(false);
            enabled = false;
        }
	}
}
