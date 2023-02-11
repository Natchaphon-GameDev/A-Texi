using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public LineRenderer lineRenderer;

    public Pursue(GameObject npc, NavMeshAgent agent, Transform player, TextMesh statusText) : base(npc, agent, player, statusText)
    {
        stateName = StateStatus.Pursue;
        agent.speed = 10;
        agent.isStopped = false;
        agent.ResetPath();
        lineRenderer = npc.GetComponent<LineRenderer>();
    }

    public override void Enter()
    {
        lineRenderer.enabled = true;
        statusText.text = "Pursue";
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.transform.position);
        lineRenderer.SetPosition(0,npc.transform.position);
        lineRenderer.SetPosition(1,player.transform.position);
        
        if (DistancePlayer() < 1)
        {
            nextState = new Attack(npc, agent, player, statusText);
            stateEvent = EventState.Exit;
        }
        else if (DistancePlayer() > 10)
        {
            nextState = new Patrol(npc, agent, player, statusText);
            stateEvent = EventState.Exit;
        }
        
    }

    public override void Exit()
    {
        lineRenderer.enabled = false;
        base.Exit();
    }

    
}