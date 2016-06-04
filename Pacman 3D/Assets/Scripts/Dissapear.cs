using UnityEngine;
using System.Collections;

public class Dissapear : MonoBehaviour {
    public GameObject pacman, puntoAround, camara, fant1, fant2;
    public int MonedasAntesCueva;
    private PlayerLogic logica;
    private bool animate, finishAnimate, camaraAnimada, animarCamara, loseTime;
    private int count, finCount, countCamera;
    private Vector3 posInicialCamara, posPuertaCamara;
    private float rotacionAntes, rotacionDurante;
    private float time;
    public AudioClip musicOpenPuerta;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        logica = pacman.GetComponent<PlayerLogic>();
        posPuertaCamara = new Vector3(-2.5f, 115, -283.7f);
        animate = false;
        finishAnimate = false;
        camaraAnimada = false;
        animarCamara = false;
        rotacionAntes = 55;
        rotacionDurante = 10;
        count = 0;
        countCamera = 0;
        finCount = 90 / 3; //grados que giramos en total, entre los k giramos por segundo
        time = 0;
        loseTime = true;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (logica.monedasPilladas >= MonedasAntesCueva && !animate && !finishAnimate){
            //finishAnimate = true;


            source.PlayOneShot(musicOpenPuerta, 1f);
            animate = true;
            animarCamara = true;
            pacman.GetComponent<PlayerLogic>().rb.velocity = Vector3.zero;
            pacman.GetComponent<PlayerLogic>().pararLogica = true;
            camara.GetComponent<FollowPac>().enabled = false;
            posInicialCamara = camara.transform.position;
            fant1.SetActive(false);
            fant2.SetActive(false);
        }
        else if (animarCamara)
        {
            //Debug.Log("Despues2: " + camara.GetComponent<FollowPac>().enabled);
            if (countCamera < (rotacionAntes - rotacionDurante))
            {
                camara.transform.Rotate(Vector3.right, -1);
                camara.transform.Translate((posPuertaCamara - posInicialCamara) / (rotacionAntes - rotacionDurante), Space.World);
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
        else if (!camaraAnimada && finishAnimate && loseTime)
        {
            time += Time.deltaTime;
            if (time >= 0.75f) loseTime = false; 
        }
        else if (!camaraAnimada && finishAnimate)
        {
            if (countCamera < (rotacionAntes - rotacionDurante))
            {
                camara.transform.Rotate(Vector3.right, 1);
                camara.transform.Translate((-posPuertaCamara + posInicialCamara) / (rotacionAntes - rotacionDurante), Space.World);
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
