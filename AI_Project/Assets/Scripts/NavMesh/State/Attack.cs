using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject npc, NavMeshAgent agent, Transform player, TextMesh statusText) : base(npc, agent, player, statusText)
    {
        stateName = StateStatus.Attack;
        agent.isStopped = true;
        agent.ResetPath();
        
    }

    public override void Enter()
    {
        statusText.text = "Attack";
        base.Enter();
    }
    
    public override void Update()
    {
        if (DistancePlayer() > 10)
        {
            nextState = new Patrol(npc, agent, player, statusText);
            stateEvent = EventState.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
