using UnityEngine;
using System.Collections;

public class Dissapear : MonoBehaviour {
    public GameObject pacman;
    public int MonedasAntesCueva;
    private PlayerLogic logica;
    private bool animate, finishAnimate;

	// Use this for initialization
	void Start () {
        logica = pacman.GetComponent<PlayerLogic>();
        animate = false;
        finishAnimate = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (logica.monedasPilladas >= MonedasAntesCueva && !animate && !finishAnimate){
            finishAnimate = true;
            animate = true;
            //gameObject.SetActive(false);
            //enabled = false;
        }
        else if (animate && !finishAnimate)
        {

        }
        else if (finishAnimate)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false); //hare que rote la puerta para simular que se abre
            enabled = false;
        }
	}
}
