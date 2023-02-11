using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class State
{
   public enum StateStatus
   {
      Patrol,
      Pursue,
      Attack
   }
   
   public enum EventState
   {
      Enter,
      Update,
      Exit
   }

   public StateStatus stateName;
   protected EventState stateEvent;
   protected GameObject npc;
   protected Transform player;
   protected State nextState;
   protected NavMeshAgent agent;
   protected TextMesh statusText;

   public State(GameObject npc, NavMeshAgent agent,Transform player, TextMesh statusText)
   {
      this.npc = npc;
      this.agent = agent;
      stateEvent = EventState.Enter;
      this.player = player;
      this.statusText = statusText;
   }

   public virtual void Enter()
   {
      stateEvent = EventState.Update;
   }
   
   public virtual void Update()
   {
      stateEvent = EventState.Update;
   }
   
   public virtual void Exit()
   {
      stateEvent = EventState.Exit;
   }

   public State Process()
   {
      if (stateEvent == EventState.Enter)
      {
         Enter();
      }
      else if (stateEvent == EventState.Update)
      {
         Update();
      }
      else if (stateEvent == EventState.Exit)
      {
         Exit();
         return nextState;
      }

      return this;
   }

   public float DistancePlayer()
   {
      return Vector3.Distance(npc.transform.position, player.position);
   }
}
