using UnityEngine;
using System.Collections;

public class DissableFirstFloor : MonoBehaviour {
    public GameObject primerPiso;
    public Collider baseCollider;
    public GameObject pacmanBody;
    private Collider waterCollider;
    private PlayerLogic pacLogic;
    private bool haColisionado;

	// Use this for initialization
	void Start () {
        waterCollider = GetComponent<Collider>();
        pacLogic = pacmanBody.GetComponent<PlayerLogic>();
        haColisionado = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (haColisionado) 
        {
            pacLogic.colisionSuelo = false;
            pacLogic.tiempoColisionSuelo -= Time.deltaTime;
            if (pacLogic.tiempoColisionSuelo < -0.5f)
            {
                MeshRenderer[] rend = primerPiso.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in rend)
                {
                    mr.enabled = false;
                }
                enabled = false;
            }
        }
        
	}

    void OnCollisionEnter(Collision collInfo)
    {
        haColisionado = true;
        waterCollider.enabled = false;
        baseCollider.enabled = false;
        pacLogic.colisionSuelo = false;
        pacLogic.rb.velocity = Vector3.zero;
        pacLogic.tiempoColisionSuelo = 1;
        
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        pacLogic.colisionSuelo = false;
        pacLogic.rb.velocity = Vector3.zero;
    }
}
