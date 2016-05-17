using UnityEngine;
using System.Collections;

public class IAPredicePac : MonoBehaviour
{

    public Transform goal;
    public float step = 1;
    private NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        float ay = goal.gameObject.transform.rotation.eulerAngles.y;
        Vector3 moveAhead = new Vector3(-Mathf.Sin(ay), 0, -Mathf.Cos(ay));
        agent.destination = goal.position + step * moveAhead;// -offset;
    }

    // Update is called once per frame
    void Update()
    {
        float ay = goal.gameObject.transform.rotation.eulerAngles.y;
        Vector3 moveAhead = new Vector3(Mathf.Sin(ay), 0, -Mathf.Cos(ay));
        agent.destination = goal.position + step * moveAhead;// -offset;
    }
}
