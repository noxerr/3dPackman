using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
    public Rigidbody rb;
    public float constante = 25f;
    public int vidas;
    public bool powerupComer;
    public bool godMode;
    public float tiempoInvencible = 1.5f;
    public float tiempoEntreParpadeos = 0.05f;
    public float tiempoEnAparecerMenuWin = 3.0f;
    public float tiempoEnAparecerMenuLose = 4.0f;
    public int NumMonedas, monedasPilladas, count;
    public float tiempoPowerUp;
    public AudioClip destroyCoinSound, countScoreSound,GameOverSound,damagedSound;
    public AudioClip gameOverMusic,music1,winMusic;
    public GameObject fire, mainCameraObject;
    public Text countText, winText, finalScoreText;
    public float tiempoColisionSuelo;
    public Canvas winMenu;
    public Canvas GameOverMenu;
    public RawImage vida1, vida2, vida3;
    public bool pararLogica;
    private Vector3 speedMin = new Vector3(1f, 1f, 1f);
    private float timeAux;
    //private bool flechasLados, flechasRectas;
    public bool colisionSuelo;

    private bool colisionHielo, colisionRampa, won, lost, translated, aumentadaSpeed;
    private int numTranslaciones;
    private float scoreSumado;
    private Vector3 velocidad;
    private float gradosDireccion, oldGradosDireccion, lastHitTime,lastTransitionTime,wonTimer,overTimer;
    private float powerTime;
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
    private float time;


    // Use this for initialization
    void Start () {
        pararLogica = false;
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
        time = 0;
        GameOverMenu.enabled = false;
        count = 0;
        
        monedasPilladas = 0;
        winText.text = "";
        GetComponent<Collider>().material.staticFriction = 0.0f;
        cameraPacScript = mainCameraObject.GetComponent<FollowPac>();
        boxColider = GetComponent<BoxCollider>();
        boxColider.material.dynamicFriction = 0.0f;
        boxColider.material.staticFriction = 0.0f;
        piernas = GetComponentInChildren<AnimationPiernas>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        powerTime = 0.0f;
        rotacionInicialCamara = -45;//mainCameraObject.transform.rotation.x - 5;
        yRelativaCamara = cameraPacScript.relativePos.y - 20;
        zRelativaCamara = cameraPacScript.relativePos.z + 35;

        gradosDireccion = 0;
        oldGradosDireccion = 0;
        source.PlayOneShot(music1, 0.5f);
        if (SceneManager.GetActiveScene().name != "scene1")
        {
            count = PlayerPrefs.GetInt("score", count);
            vidas = PlayerPrefs.GetInt("vidas", vidas);
            if (vidas <= 2) vida1.enabled = false;
            if (vidas <= 1) vida2.enabled = false;
        }
        else { vidas = 3;

             PlayerPrefs.SetInt("vidas", vidas);
            PlayerPrefs.SetInt("score", count); }
        SetCountText();
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
    void hazteGrandeyPequeño()
    {
        float d2 = Time.time;
        if (d2 - powerTime > tiempoPowerUp) powerupComer = false;
        if (powerupComer)
        {
            time += Time.deltaTime * 8f;
            float value = Mathf.Sin(time) * 0.25f + 1f;
            transform.localScale = new Vector3(value, transform.localScale.y, value);
        }
        else transform.localScale = new Vector3(1, 1, 1);
    }
    void touchRenderers(bool yesno)
    {
        MeshRenderer[] renders = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in renders)
        {
            part.enabled = yesno;
        }
        if (vidas == 2) vida1.enabled = yesno;
        else if (vidas == 1) vida2.enabled = yesno;
        else if (vidas == 0) vida3.enabled = yesno;
    }

    void botonesDebug()
    {
        if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyUp(KeyCode.Z)) ++vidas;
        if (Input.GetKeyUp(KeyCode.C)) {
            //parar animacion piernas
            piernas.enabled = false;
            //para que no se choque por el camino al centro
            boxColider.enabled = false;
            lost = true;
            source.PlayOneShot(GameOverSound, 1f);
            overTimer = Time.time;
            rb.velocity = new Vector3(0, 30, 0);
            transform.Translate(0, 10, 0, Space.World);
        }
        if (Input.GetKeyUp(KeyCode.X)) { monedasPilladas = NumMonedas;
            SetCountText();
        }
        if (Input.GetKeyUp(KeyCode.V)) { monedasPilladas = 117; }
    }


	// Update is called once per frame
	void Update ()
    {
        if (!pararLogica)
        {
            hazteGrandeyPequeño();
            botonesDebug();
            float d2 = Time.time;
            //COMPROBAR QUE NO ESTE EN UNA RAMPA 
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
                        if (vidas == 2) vida1.enabled = false;
                        else if (vidas == 1) vida2.enabled = false;
                    }
                }

                //MOVER JUGADOR SI NO SE HA ACABADO LA PARTIDA
                if (!lost && !won && tiempoColisionSuelo <= 0) logicaMovimiento();
            }
            //SI ESTA EN LA RAMPA QUE BAJA, LE DAMOS LA VELOCIDAD PERPENDICULAR A LA NORMAL DE LA RAMPA
            else if (colisionBajada)
            {
                //rb.velocity = transform.TransformDirection(-Vector3.forward*50); //el que funciona
                rb.velocity = -planeNormal * 70;
            }
            //COMPROBAR FIN PARTIDA GANADA
            if (won) wonFunction();
            if (lost)
            {
                if (rb.velocity.y < 5 && rb.velocity.y > -10) rb.velocity = new Vector3(0, -120, 0);
                AnimacionMuerto();
                if (d2 - overTimer > tiempoEnAparecerMenuLose && !GameOverMenu.enabled)
                {
                    source.PlayOneShot(gameOverMusic, 0.8f);
                    GameOverMenu.enabled = true;
                }
            }

            //ROTACIONES
            if ((!won || translated) && !lost) gestionRotacionesPacman();
        }
    }



    void logicaMovimiento()
    {
        if (colisionSuelo)
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
        else if (colisionHielo && Mathf.Abs(rb.velocity.x) < speedMin.x && Mathf.Abs(rb.velocity.z) < speedMin.z)
        {
            if (Mathf.Abs(velocidad.x) < 1)
            {
                velocidad.x = 80;
                velocidad.z = 0;
            }
            rb.velocity = velocidad;
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
            if (source.isPlaying && scoreSumado < count) scoreSumado += Time.deltaTime * count / duracionContarScore;
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

            PlayerPrefs.SetInt("score", count);

            monedasPilladas += 1;
            SetCountText();
        }
        else if (col.gameObject.tag == "Teleport")
        {
            monedasPilladas = NumMonedas;
            SetCountText();
        }
        else if (col.gameObject.tag == "PowerUpCome")
        {
            Destroy(col.gameObject);
            powerupComer = true;
            powerTime = Time.time;
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
                PlayerPrefs.SetInt("vidas", vidas);
                if (vidas <= 0)
                {
                    //parar animacion piernas
                    piernas.enabled = false;
                    //para que no se choque por el camino al centro
                    boxColider.enabled = false;
                    lost = true;
                    vida3.enabled = false;
                    source.PlayOneShot(GameOverSound, 1f);
                    overTimer = Time.time;
                    rb.velocity = new Vector3(0,30,0);
                    transform.Translate(0,10,0, Space.World);
                }
                else
                {
                    source.PlayOneShot(damagedSound, 1f);
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
        SceneManager.LoadScene("scene2");
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
            source.Stop();
            source.PlayOneShot(winMusic, 0.5f);
            wonTimer = Time.time;

            //vector de donde ha de ir, para despues dividirlo por el numero de frames que queremos que tarde la translacion
            if (SceneManager.GetActiveScene().name == "scene1") 
                translacionFinal = new Vector3(-transform.position.x, -transform.position.y+40, -transform.position.z - 30);
            else translacionFinal = new Vector3(0.13f-transform.position.x, 134.62f-transform.position.y + 30, -135.18f-transform.position.z);
            winText.text = "You Win!";
        }
    }
}
