using UnityEngine;
using System.Collections;

public class Dissapear : MonoBehaviour {
    public GameObject pacman;
    public GameObject puntoAround;
    public int MonedasAntesCueva;
    private PlayerLogic logica;
    private bool animate, finishAnimate;
    private int count, finCount;

	// Use this for initialization
	void Start () {
        logica = pacman.GetComponent<PlayerLogic>();
        animate = false;
        finishAnimate = false;
        count = 0;
        finCount = 90 / 3; //grados que giramos en total, entre los k giramos por segundo
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (logica.monedasPilladas >= MonedasAntesCueva && !animate && !finishAnimate){
            //finishAnimate = true;
            animate = true;
            //gameObject.SetActive(false);
            //enabled = false;
        }
        else if (animate && !finishAnimate)
        {
            transform.RotateAround(puntoAround.transform.position, Vector3.up, 3);
            count++;
            if (count >= finCount) finishAnimate = true;
        }
        else if (finishAnimate)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //gameObject.SetActive(false); //hare que rote la puerta para simular que se abre
            enabled = false;
        }
	}
}
