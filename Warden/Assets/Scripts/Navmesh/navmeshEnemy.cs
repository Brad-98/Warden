using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class navmeshEnemy : MonoBehaviour
{
    private NavMeshAgent myAgent;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.SetDestination(target.position);
    }
}
