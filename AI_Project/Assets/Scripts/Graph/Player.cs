using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [Header("Setup Speed")]
   [SerializeField] private float speed = 5f;
   [SerializeField] private float rotateSpeed = 10f;
   
   [Header("Setup Waypoints")]
   [SerializeField] private List<GameObject> waypoints;
   [SerializeField] private WPManager wpManager;
   
   [Header("Setup Tracker")]
   [SerializeField] private float overWPDistance = .5f;
   [SerializeField] private float overTrackerDistance = .5f;
   [SerializeField] private float trackerSpeed = 15f;

   [Header("Do not touch")] //For Debug
   [SerializeField] private GameObject tracker;
   
   private Graph graph;
   private GameObject currentNode;
   private int currentWP = 0;
   private GameObject goal;

   private void Start()
   {
      //Setup Tracker
      tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
      DestroyImmediate(tracker.GetComponent<Collider>());
      //Make sure tracker get this transform
      tracker.transform.position = transform.position;
      tracker.transform.rotation = transform.rotation;
      //Make Tracker invisible
      tracker.GetComponent<MeshRenderer>().enabled = false;
      
      //Setup get reference from WPManager
      graph = wpManager.graph;
      waypoints = wpManager.waypoints;
      //Set current node = node 0
      currentNode = waypoints[0];
   }

   private void Update()
   {
      //Implement Code
      TrackerHandle();
      MoveHandle();
   }

   private void TrackerHandle()
   {
      //Stop If path = 0 or Arrived at target node
      if (graph.getPathLength() == 0 || currentWP == graph.getPathLength())
      {
         return;
      }
      
      var trackerDistance = Vector3.Distance(transform.position, tracker.transform.position);
      //Stop when Distance more than overTrackerDistance
      if (trackerDistance > overTrackerDistance)
      {
         return;
      }

      currentNode = graph.getPathPoint(currentWP);
      
      //Check Distance from Tracker and CurrentNode
      var distanceWP = Vector3.Distance(tracker.transform.position, currentNode.transform.position);

      //Setup next waypoint
      if (distanceWP < overWPDistance)
      {
         currentWP++;
      }
      
      //Debug Stop when arrived at Target Waypoint
      if (currentWP < graph.getPathLength())
      {
         goal = graph.getPathPoint(currentWP);
         
         //LookAtWayPoint
         tracker.transform.LookAt(goal.transform);
         
         //Move forward with Z Axis
         tracker.transform.Translate((Vector3.forward * trackerSpeed) * Time.deltaTime);
      }
   }
   
   private void MoveHandle()
   {
      //if OverDistanceTracker Stop Ai
      var distanceTracker = Vector3.Distance(tracker.transform.position, transform.position);
      if (distanceTracker < overTrackerDistance)
      {
         return;
      }
      
      //LookAtTarget Waypoint
      var lookAtTracker = Quaternion.LookRotation(tracker.transform.position - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTracker, rotateSpeed * Time.deltaTime);
      
      //Move forward with Z Axis
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
   }
   
   public void MoveTo(int indexWP)
   {
      //Return if Click the same Target
      if (goal == waypoints[indexWP])
      {
         Debug.Log("You are arrived!");
         return;
      }
      
      //A* calculate
      graph.AStar(currentNode, waypoints[indexWP]);
      
      //Reset CurrentWP
      currentWP = 0;
   }
}
