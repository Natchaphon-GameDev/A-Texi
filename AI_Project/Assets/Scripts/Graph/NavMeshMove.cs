using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<GameObject> waypoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(int index)
    {
        agent.SetDestination(waypoints[index].transform.position);
    }
}
