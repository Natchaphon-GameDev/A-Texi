using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    private int currentIndex = 0;
    private List<GameObject> waypoints;


    public Patrol(GameObject npc, NavMeshAgent agent, Transform player, TextMesh statusText) : base(npc, agent, player, statusText)
    {
        stateName = StateStatus.Patrol;
        agent.speed = 5;
        agent.isStopped = false;
        agent.ResetPath();
        waypoints = npc.GetComponent<AI>().waypoints;
    }

    public override void Enter()
    {
        statusText.text = "Patrol";
        base.Enter();
        var lastDistance = Mathf.Infinity;

        currentIndex = 0;
        for (int i = 0; i < waypoints.Count; i++)
        {
            var thisWP = waypoints[i];
            var distance = Vector3.Distance(thisWP.transform.position, npc.transform.position);
            if (distance < lastDistance)
            {
                currentIndex = i - 1;
                lastDistance = distance;
            }
        }
        

    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= waypoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(waypoints[currentIndex].transform.position);
        }

        if (DistancePlayer() < 10)
        {
            nextState = new Pursue(npc, agent, player, statusText);
            stateEvent = EventState.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
