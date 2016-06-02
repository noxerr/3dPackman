using UnityEngine;
using System.Collections;

public class Dissapear : MonoBehaviour {
    public GameObject pacman, puntoAround, camara, fant1, fant2;
    public int MonedasAntesCueva;
    private PlayerLogic logica;
    private bool animate, finishAnimate, camaraAnimada, animarCamara;
    private int count, finCount, countCamera;
    private Vector3 posInicialCamara, posPuertaCamara;
    private float rotacionAntes, rotacionDurante;

	// Use this for initialization
	void Start () {
        logica = pacman.GetComponent<PlayerLogic>();
        posPuertaCamara = new Vector3(-2.5f, 165, -243.7f);
        animate = false;
        finishAnimate = false;
        camaraAnimada = false;
        animarCamara = false;
        rotacionAntes = 55;
        rotacionDurante = 10;
        count = 0;
        countCamera = 0;
        finCount = 90 / 3; //grados que giramos en total, entre los k giramos por segundo
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (logica.monedasPilladas >= MonedasAntesCueva && !animate && !finishAnimate){
            //finishAnimate = true;
            posInicialCamara = camara.transform.position;
            animate = true;
            animarCamara = true;
            pacman.GetComponent<PlayerLogic>().pararLogica = true;
            camara.GetComponent<FollowPac>().enabled = false;
            fant1.SetActive(false);
            fant2.SetActive(false);
        }
        else if (animarCamara)
        {
            //Debug.Log("Despues2: " + camara.GetComponent<FollowPac>().enabled);
            if (countCamera < (rotacionAntes - rotacionDurante))
            {
                camara.transform.Rotate(Vector3.right, -1);
                camara.transform.Translate((posPuertaCamara - posInicialCamara) / (rotacionAntes - rotacionDurante));
            }
            else animarCamara = false;
            countCamera++;
        }
        else if (animate && !finishAnimate)
        {
            transform.RotateAround(puntoAround.transform.position, Vector3.up, 3);
            count++;
            if (count >= finCount)
            {
                finishAnimate = true;
                countCamera = 0;
            }
        }
        else if (!camaraAnimada && finishAnimate)
        {
            if (countCamera < (rotacionAntes - rotacionDurante))
            {
                camara.transform.Rotate(Vector3.right, 1);
                camara.transform.Translate((-posPuertaCamara + posInicialCamara) / (rotacionAntes - rotacionDurante));
            }
            else camaraAnimada = true;
            countCamera++;
        }
        else if (finishAnimate)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //gameObject.SetActive(false); //hare que rote la puerta para simular que se abre
            pacman.GetComponent<PlayerLogic>().pararLogica = false;
            camara.GetComponent<FollowPac>().enabled = true;
            enabled = false;
        }
	}
}
