using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLogic : MonoBehaviour {
    private Rigidbody rb;
    public float constante = 25f;
    public int vidas;
    public bool godMode;

    public AudioClip destroyCoinSound;
    //private bool flechasLados, flechasRectas;
    private bool colisionSuelo, colisionRampa;
    private bool won, lost, translated;
    private int count;
    public Text countText;
    public Text winText;
    private Vector3 velocidad;
    private float gradosDireccion, oldGradosDireccion;
    private float lastHitTime;
    private AudioSource source;
    private Vector3 translacionFinal;
    private float rotacionInicialCamara, yRelativaCamara, zRelativaCamara;
    private int numTranslaciones;
    private AnimationPiernas piernas;
    public GameObject fire;
    private BoxCollider boxColider;
    private FollowPac cameraPacScript;
    public GameObject mainCameraObject;
    //private Transform papa;
    //private Vector3 transformOffset;


    // Use this for initialization
    void Start () {
        
        numTranslaciones = 0;
        won = lost = translated = false;
        
        //flechasLados = false;
        //flechasRectas = false;
        colisionSuelo = false;
        colisionRampa = false;
        lastHitTime = Time.time;

        count = 0;
        SetCountText();
        winText.text = "";

        GetComponent<Collider>().material.staticFriction = 0.0f;
        cameraPacScript = mainCameraObject.GetComponent<FollowPac>();
        boxColider = GetComponent<BoxCollider>();
        piernas = GetComponentInChildren<AnimationPiernas>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        rotacionInicialCamara = -35;//mainCameraObject.transform.rotation.x - 5;
        yRelativaCamara = cameraPacScript.relativePos.y - 15;
        zRelativaCamara = cameraPacScript.relativePos.z + 35;

        gradosDireccion = 0;
        oldGradosDireccion = 0;
        //papa = gameObject.transform.parent;
        //transformOffset = gameObject.transform.localPosition;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //gestion hits
        if (godMode)
        {
            float d2 = Time.time;
            if (d2 - lastHitTime > 1.5f) godMode = false;
        }
        //float moveHorizontal = Input.GetAxis("Horizontal");
        if (!lost && !won){
            if (colisionSuelo == true)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //flechasLados = true;
                    //rb.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
                    velocidad = new Vector3(-1.0f * constante, rb.velocity.y, 0);
                    if (gradosDireccion != 90) oldGradosDireccion = gradosDireccion;
                    gradosDireccion = 90;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    //flechasLados = true;
                    //rb.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * constante, ForceMode.VelocityChange);
                    velocidad = new Vector3(1.0f * constante, rb.velocity.y, 0);
                    if (gradosDireccion != 270) oldGradosDireccion = gradosDireccion;
                    gradosDireccion = 270;
                }
                /*else if (flechasLados == true) //PARA QUE FRENE MAS RAPIDO
                {
                    rb.AddForce(new Vector3(-rb.velocity.x * 0.7f, 0, 0), ForceMode.VelocityChange);
                    flechasLados = false;
                }*/



                //z moves (vertical)
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //flechasRectas = true;
                    //rb.velocity = new Vector3(0, 0, -1.0f * constante);
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
                /*else if (flechasRectas == true) //PARA QUE FRENE MAS RAPIDO
                {
                    rb.AddForce(new Vector3(0, 0, -rb.velocity.z * 0.7f), ForceMode.VelocityChange);
                    flechasRectas = false;
                }*/
                rb.velocity = velocidad;

            }
        }
        
        else if (won)
        {
            if (!translated)
            {
                rb.velocity = Vector3.zero;
                transform.Translate(translacionFinal / 60, Space.World);
                //transform.Translate(-transform.position.x,-transform.position.y,-transform.position.z-30, Space.World); 
                //transform.Translate(0,20,0);
                transform.Rotate(0, 5, 0);
                cameraPacScript.relativePos.Set(cameraPacScript.relativePos.x, cameraPacScript.relativePos.y - yRelativaCamara/60,
                    cameraPacScript.relativePos.z - zRelativaCamara / 60);
                mainCameraObject.transform.Rotate(rotacionInicialCamara/60, 0, 0);
                if (numTranslaciones == 59) translated = true;
                else numTranslaciones++;
            }
            else
            {
                boxColider.enabled = true;
                fire.SetActive(true);
            }
            //Debug.Log("pos win: " + transform.position);
        }
        //Debug.Log("pos antes: " + transform.position);

        if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 25)
        {
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion - 360, 0) * Time.deltaTime * 5);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion + 360, 0) * Time.deltaTime * 5);
            else transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion, 0) * Time.deltaTime * 4);
        }
        else if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 12)
        {
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion - 360, 0) * Time.deltaTime * 1.8f);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion + 360, 0) * Time.deltaTime * 1.8f);
            else transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion, 0) * Time.deltaTime * 1.5f);
        }
        else if (Mathf.Abs(transform.eulerAngles.y - gradosDireccion) > 5)
        {
            if (gradosDireccion - oldGradosDireccion > 180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion - 360, 0) * Time.deltaTime * 0.4f);
            else if (gradosDireccion - oldGradosDireccion < -180)
                transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion + 360, 0) * Time.deltaTime * 0.4f);
            else transform.Rotate(new Vector3(0, gradosDireccion - oldGradosDireccion, 0) * Time.deltaTime * 0.4f);
        }
        //Debug.Log("Euler: " + transform.eulerAngles.y + ". gradosDireccion: " + gradosDireccion + ". oldGradosDireccion: " + oldGradosDireccion);
        rb.AddForce(new Vector3(0.0f, -30.0f, 0.0f)); //gravedad aumentada

        //papa.position = transform.position - transformOffset;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pick Up")
        {
            source.PlayOneShot(destroyCoinSound, 1.0f);
            Destroy(col.gameObject);
            count = count + 1;
            SetCountText();
            EnemyLogic[] enemies = GameObject.Find("Enemies").GetComponentsInChildren<EnemyLogic>();
            foreach (EnemyLogic enemy in enemies)
            {
                enemy.canBeEaten = false;
            }
        }
        else if (col.gameObject.tag == "PowerUpCome")
        {
            Destroy(col.gameObject);
            EnemyLogic[] enemies = GameObject.Find("Enemies").GetComponentsInChildren<EnemyLogic>();
            foreach (EnemyLogic enemy in enemies)
            {
                enemy.canBeEaten = true;
            }
        }
        else if (col.gameObject.tag == "Enemy") {
            if (col.gameObject.GetComponent<EnemyLogic>().canBeEaten)
            {
                col.gameObject.SetActive(false);
            }
            else if (!godMode) --vidas;

            godMode = true;
            lastHitTime = Time.time;
        }
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "Terrain") colisionSuelo = true;
            else if (contact.otherCollider.gameObject.tag == "Rampa")
            {
                colisionRampa = true;
                GetComponent<Collider>().material.dynamicFriction = 0;
            }
        } 
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Terrain") colisionSuelo = false;
        else if (collisionInfo.gameObject.tag == "Rampa")
        {
            colisionRampa = false;
            GetComponent<Collider>().material.dynamicFriction = 0.5f;
        }
        //print("No longer in contact with " + collisionInfo.transform.name);
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 106 && !won)//106 monedas en lvl 1
        {
            //to rotate looking down
            if (gradosDireccion != 0) oldGradosDireccion = gradosDireccion;
            gradosDireccion = 0;

            //parar animacion piernas
            piernas.enabled = false;
            
            //para que no se choque por el camino al centro
            boxColider.enabled = false;

            won = true;

            //vector de donde ha de ir, para despues dividirlo por el numero de frames que queremos que tarde la translacion
            translacionFinal = new Vector3(-transform.position.x, -transform.position.y+40, -transform.position.z - 30);
            winText.text = "You Win!";
        }
    }
}
