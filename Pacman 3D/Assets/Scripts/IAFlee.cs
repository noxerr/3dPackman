using UnityEngine;
using System.Collections;

public class IAFlee : MonoBehaviour {

    public Transform goal;
    private NavMeshAgent agent;
    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        //offset = new Vector3(25.25f,10.0f ,-168.0f);
        agent.destination = goal.position;// -offset;
    }

    // Update is called once per frame
    void Update ()
    {
        agent.destination = -goal.position;// -offset;
    }
}
