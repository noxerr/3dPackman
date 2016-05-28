using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
    public Rigidbody rb;
    public float constante = 25f;
    public int vidas;
    public bool godMode;
    public float tiempoInvencible = 1.5f;
    public float tiempoEntreParpadeos = 0.05f;
    public float tiempoEnAparecerMenuWin = 3.0f;
    public float tiempoEnAparecerMenuLose = 4.0f;
    public int NumMonedas;
    public AudioClip destroyCoinSound, countScoreSound;
    public GameObject fire, mainCameraObject;
    public Text countText, winText, finalScoreText;
    public float tiempoColisionSuelo;
    public Canvas winMenu;
    public Canvas GameOverMenu;

    private float timeAux;
    //private bool flechasLados, flechasRectas;
    public bool colisionSuelo;

    private bool colisionHielo, colisionRampa, won, lost, translated, aumentadaSpeed;
    private int count, numTranslaciones, monedasPilladas;
    private float scoreSumado;
    private Vector3 velocidad;
    private float gradosDireccion, oldGradosDireccion, lastHitTime,lastTransitionTime,wonTimer,overTimer;
    private bool tocaEsconderte;
    private AudioSource source;
    private Vector3 translacionFinal;
    private float rotacionInicialCamara, yRelativaCamara, zRelativaCamara;
    private AnimationPiernas piernas;
    private BoxCollider boxColider;
    private FollowPac cameraPacScript;
    private float duracionContarScore;
    private bool soundFinalPlayed = false, colisionBajada;
    private Vector3 planeNormal;


    // Use this for initialization
    void Start () {
        tiempoColisionSuelo = 0;
        colisionBajada = false;
        aumentadaSpeed = false;
        duracionContarScore = countScoreSound.length;
        numTranslaciones = 0;
        won = lost = translated = false;
        colisionSuelo = false;
        colisionHielo = false;
        colisionRampa = false;
        lastHitTime = Time.time;
        winMenu.enabled = false;
        GameOverMenu.enabled = false;
        count = 0;
        monedasPilladas = 0;
        SetCountText();
        winText.text = "";
        GetComponent<Collider>().material.staticFriction = 0.0f;
        cameraPacScript = mainCameraObject.GetComponent<FollowPac>();
        boxColider = GetComponent<BoxCollider>();
        boxColider.material.dynamicFriction = 0.0f;
        boxColider.material.staticFriction = 0.0f;
        piernas = GetComponentInChildren<AnimationPiernas>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        rotacionInicialCamara = -45;//mainCameraObject.transform.rotation.x - 5;
        yRelativaCamara = cameraPacScript.relativePos.y - 20;
        zRelativaCamara = cameraPacScript.relativePos.z + 35;

        gradosDireccion = 0;
        oldGradosDireccion = 0;

    }
	
    void Parpadea()
    {
        float d2 = Time.time;
        if (d2 - lastTransitionTime > tiempoEntreParpadeos) {
            lastTransitionTime = Time.time;
            tocaEsconderte = !tocaEsconderte;
        }
        if (tocaEsconderte) touchRenderers(false);
        else touchRenderers(true);
    }
    void touchRenderers(bool yesno)
    {
        MeshRenderer[] renders = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in renders)
        {
            part.enabled = yesno;
        }
    }


	// Update is called once per frame
	void Update ()
    {
        float d2 = Time.time;
        if (transform.position.y > 90 || transform.position.y < 2)
        {
            //gestion hits
            if (godMode)
            {
                Parpadea();
                if (d2 - lastHitTime > tiempoInvencible)
                {
                    godMode = false;
                    tocaEsconderte = false;
                    touchRenderers(true);
                }
            }

            //MOVER JUGADOR SI NO SE HA ACABADO LA PARTIDA
            if (!lost && !won && tiempoColisionSuelo <= 0) logicaMovimiento();
        }
        else if (colisionBajada)
        {
            //rb.velocity = transform.InverseTransformDirection(Vector3.forward)*60;
            //rb.velocity = transform.TransformDirection(-Vector3.forward*50); //el que funciona
            rb.velocity = -planeNormal * 70;
        }
        //COMPROBAR FIN PARTIDA GANADA
        if (won) wonFunction();
        if (lost)
        {
            if (rb.velocity.y < 5) rb.velocity = new Vector3(0, -150, 0);
            AnimacionMuerto();
            if (d2 - overTimer > tiempoEnAparecerMenuLose) GameOverMenu.enabled = true;
        }

        //ROTACIONES
        if ((!won || translated) && !lost) gestionRotacionesPacman();
    }



    void logicaMovimiento()
    {
        if (colisionSuelo == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                velocidad = new Vector3(-1.0f * constante, rb.velocity.y, 0);
                if (gradosDireccion != 90) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 90;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                velocidad = new Vector3(1.0f * constante, rb.velocity.y, 0);
                if (gradosDireccion != 270) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 270;
            }

            //z moves (vertical)
            if (Input.GetKey(KeyCode.DownArrow))
            {
                velocidad = new Vector3(0, rb.velocity.y, -1.0f * constante);
                if (gradosDireccion != 0) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 0;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                velocidad = new Vector3(0, rb.velocity.y, 1.0f * constante);
                if (gradosDireccion != 180) oldGradosDireccion = gradosDireccion;
                gradosDireccion = 180;
            }
            rb.velocity = velocidad;

        }
        else if (!aumentadaSpeed && colisionHielo == true)
        {
            GetComponent<Collider>().material.dynamicFriction = 0.0f;
            rb.velocity = rb.velocity * 3;
            aumentadaSpeed = true;
        }
    }



    void wonFunction()
    {
        float d2 = Time.time;
        if (d2 - wonTimer > tiempoEnAparecerMenuWin) winMenu.enabled = true;
        if (!translated)
        {
            rb.velocity = Vector3.zero;
            transform.Translate(translacionFinal / 60, Space.World);
            transform.Rotate(0, 20, 0);
            cameraPacScript.relativePos.Set(cameraPacScript.relativePos.x, cameraPacScript.relativePos.y - yRelativaCamara / 60,
                cameraPacScript.relativePos.z - zRelativaCamara / 60);
            mainCameraObject.transform.Rotate(rotacionInicialCamara / 60, 0, 0);
            if (numTranslaciones == 59) translated = true;
            else numTranslaciones++;
        }
        else
        {
            finalScoreText.enabled = true;
            if (!soundFinalPlayed)
            {
                source.PlayOneShot(countScoreSound, 1f);
                soundFinalPlayed = true;
            }
            if (source.isPlaying) scoreSumado += Time.deltaTime * count / duracionContarScore;
            else scoreSumado = count;
            finalScoreText.text = "Score = " + (int)scoreSumado;
            boxColider.enabled = true;
            fire.SetActive(true);
        }
    }




    void gestionRotacionesPacman()
    {
        if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 25)
        {
            timeAux = Mathf.Min(Time.deltaTime * 5f, 25f);
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(0, (gradosDireccion - oldGradosDireccion - 360) * timeAux, 0);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(0, (gradosDireccion - oldGradosDireccion + 360) * timeAux, 0);
            else transform.Rotate(0, (gradosDireccion - oldGradosDireccion) * timeAux * 4/5, 0);
        }
        else if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 10)
        {
            timeAux = Mathf.Min(Time.deltaTime * 0.8f, 9.8f);
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(0, (gradosDireccion - oldGradosDireccion - 360) * timeAux, 0);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(0, (gradosDireccion - oldGradosDireccion + 360) * timeAux, 0);
            else transform.Rotate(0, (gradosDireccion - oldGradosDireccion) * timeAux, 0);
        }
    }




    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pick Up")
        {
            source.PlayOneShot(destroyCoinSound, 1.0f);
            Destroy(col.gameObject);
            count += 5;
            monedasPilladas += 1;
            SetCountText();
        }
        else if (col.gameObject.tag == "PowerUpCome")
        {
            Destroy(col.gameObject);
            EnemyLogic[] enemies = GameObject.Find("Enemies").GetComponentsInChildren<EnemyLogic>();
            foreach (EnemyLogic enemy in enemies)
            {
                enemy.canBeEaten = true;
                enemy.startEatenTimer();
            }
        }
        else if (col.gameObject.tag == "Enemy") {
            if (col.gameObject.GetComponent<EnemyLogic>().canBeEaten)
            {
                col.gameObject.GetComponent<EnemyLogic>().killGhost();
                count += 50;
                SetCountText();
            }
            else if (!godMode) {
                --vidas;
                if (vidas <= 0)
                {
                    //parar animacion piernas
                    piernas.enabled = false;
                    //para que no se choque por el camino al centro
                    boxColider.enabled = false;
                    lost = true;
                    overTimer = Time.time;
                    rb.velocity = new Vector3(0,30,0);
                    transform.Translate(0,10,0, Space.World);
                }
                else
                {
                    godMode = true;
                    tocaEsconderte = true;
                    lastTransitionTime = Time.time;
                    lastHitTime = Time.time;
                }
            }
            
        }
    }


    void OnCollisionEnter(Collision collInfo)
    {
        foreach (ContactPoint contact in collInfo.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "Rampa")
            {
                if (!colisionRampa)
                {
                    rb.velocity = -rb.velocity / 2;
                }
                colisionRampa = true;
                GetComponent<Collider>().material.dynamicFriction = 0;

            }
            else if (contact.otherCollider.gameObject.tag == "Bajada")
            {
                colisionRampa = true;
                GetComponent<Collider>().material.dynamicFriction = 0;
                planeNormal = contact.otherCollider.transform.forward;
                colisionBajada = true;
            }
            else if (contact.otherCollider.gameObject.tag == "Hielo") colisionHielo = true;
            /*else if (contact.otherCollider.gameObject.tag == "Agujero")
            {
                getC
            }*/
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "Terrain") colisionSuelo = true;
            else if (contact.otherCollider.gameObject.tag == "Bajada")
            {
                GetComponent<Collider>().material.dynamicFriction = 0;
                planeNormal = contact.otherCollider.transform.forward;

            }
        } 
    }



    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Terrain") colisionSuelo = false;
        else if (collisionInfo.gameObject.tag == "Rampa")
        {
            colisionRampa = false;
            GetComponent<Collider>().material.dynamicFriction = 0.2f;
        }
        else if (collisionInfo.gameObject.tag == "Hielo")
        {
            colisionHielo = false;
            aumentadaSpeed = false;
            rb.velocity = rb.velocity / 2;
            GetComponent<Collider>().material.dynamicFriction = 0.2f;
        }
        else if (collisionInfo.gameObject.tag == "Bajada") colisionBajada = false;
    }



    void AnimacionMuerto()
    {
        transform.Rotate(15 * Time.deltaTime * 8,
            30 * Time.deltaTime * 8, -45 * Time.deltaTime * 8);
    }
    public void pasaNivel() {
        SceneManager.LoadScene("Scene2");
    }

    //GESTION MONEDAS Y PUNTUACION
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (monedasPilladas >= NumMonedas && !won)//108 monedas en lvl 1
        {
            //to rotate looking down
            if (gradosDireccion != 0) oldGradosDireccion = gradosDireccion;
            gradosDireccion = 0;

            //parar animacion piernas
            piernas.enabled = false;
            
            //para que no se choque por el camino al centro
            boxColider.enabled = false;

            won = true;
            wonTimer = Time.time;

            //vector de donde ha de ir, para despues dividirlo por el numero de frames que queremos que tarde la translacion
            translacionFinal = new Vector3(-transform.position.x, -transform.position.y+40, -transform.position.z - 30);
            winText.text = "You Win!";
        }
    }
}
