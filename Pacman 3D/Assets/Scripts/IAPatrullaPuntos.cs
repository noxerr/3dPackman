using UnityEngine;
using System.Collections;

public class IAPatrullaPuntos : MonoBehaviour
{
    public GameObject SetOfPoints;
    private Transform[] points;
    private int currentDestination;
    private NavMeshAgent agent;
    private bool derecha;
    private int npoints;
    //private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        points = SetOfPoints.GetComponentsInChildren<Transform>();
        currentDestination = 0;
        npoints = points.Length;
        derecha = true;
        agent.destination = points[0].position;
        //offset = new Vector3(25.25f,10.0f ,-168.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {//go to next point
            if (derecha)
            {
                ++currentDestination;
                if (currentDestination >= npoints)
                {
                    derecha = false;
                    --currentDestination;
                }
            }
            if (!derecha)
            {
                --currentDestination;
                if(currentDestination <= 0){
                    derecha = true; currentDestination += 2; }
            }
            agent.destination = points[currentDestination].position;// -offset;
        }
    }
}
