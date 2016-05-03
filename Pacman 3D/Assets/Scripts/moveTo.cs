using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour {
    public Transform goal;
    private NavMeshAgent agent;
    // Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.position);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        agent.SetDestination(goal.position);

    }
}
