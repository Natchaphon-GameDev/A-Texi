using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    private State currentState;
    private TextMesh statusText;
    public List<GameObject> waypoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        statusText = GetComponentInChildren<TextMesh>();
        currentState = new Patrol(this.gameObject, agent, player, statusText);
    }

    private void Update()
    {
        transform.GetChild(0).LookAt(Camera.main.transform);
        currentState = currentState.Process();
    }
}
