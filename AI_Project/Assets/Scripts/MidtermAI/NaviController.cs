using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NaviController : MonoBehaviour
{
   [Header("Setup Speed")]
   [SerializeField] private float rotateSpeed = 10f;

   [SerializeField] private GameObject car;

   [Header("Setup Waypoints")]
   [SerializeField] private List<GameObject> waypoints;
   [SerializeField] private WaypointsManager wpManager;
   
   [Header("Setup Tracker")]
   [SerializeField] private float overWPDistance = .5f;
   [SerializeField] private float overTrackerDistance = 5f;
   [SerializeField] private float trackerSpeed = 15f;

   [Header("Do not touch")] //For Debug
   [SerializeField] private GameObject tracker;
   
   private Graph graph;
   private GameObject currentNode;
   private int currentWP;
   private GameObject goal;

   private GameObject startNode;
   private GameObject endNode;

   private void OnEnable()
   {
      //Setup new Destination
      SetupTracker();
   }

   private void OnDisable()
   {
      //For Debug Run at startGame
      if (startNode == null) 
      {
         return;
      }
      
      //Calculate distance to reward Mooney and Time
      var distanceCalculate = Vector3.Distance(startNode.transform.position, endNode.transform.position);
      GameManager.instance.AddMoney((int)distanceCalculate);
      GameManager.instance.AddTime((int)distanceCalculate / 5f);
   }

   private void SetupTracker()
   {
      //For the first time setup only
      if (tracker == null)
      {
         //Setup Tracker
         tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
         Destroy(tracker.GetComponent<Collider>());
         
         //Make Tracker invisible
         tracker.GetComponent<MeshRenderer>().enabled = false;
         
         //Setup get reference from WPManager
         graph = wpManager.graph;
         waypoints = wpManager.waypoints;
      }
      
      //Make sure tracker get this transform
      tracker.transform.position = car.transform.position + new Vector3(0,0,2);
      tracker.transform.rotation = car.transform.rotation;
      
      //Reset Start n End node
      endNode = null;
      startNode = null;

      //Set current node = nearest waypoint
      FindNearestNode();
   }
   
   private void FindNearestNode()
   {
      //Logic to find nearest node
      var nearestNodeIndex = 0;
      var minDistance = Mathf.Infinity;
 
      for (var i = 0; i< waypoints.Count; i++)
      {
         var distanceToNode = (transform.position - waypoints[i].transform.position).sqrMagnitude;
         
         if(distanceToNode < minDistance)
         {
            minDistance = distanceToNode ;
            nearestNodeIndex = i ;
         }
      }
      //Set CurrentNode to nearest waypoint
      currentNode = waypoints[nearestNodeIndex];
      //Set StartNode to Calculate Distance later
      startNode = waypoints[nearestNodeIndex];
      
      //Reset CurrentWP
      currentWP = 0;
   }

   //For Implement at PassengerController
   public int FindDestination(string destination)
   {
      //Find destination that you want
      var temp = waypoints.Find(x => x.CompareTag(destination));
      
      //Return Waypoint index of that destination
      return waypoints.IndexOf(temp);
   }

   private void Update()
   {
      //Implement Code
      HandleTracker();
      HandleRotate();
   }

   private void HandleTracker()
   {
      //Stop If path = 0 or Arrived at target node
      if (graph.getPathLength() == 0 || currentWP == graph.getPathLength())
      {
         return;
      }
      
      var trackerDistance = Vector3.Distance(car.transform.position, tracker.transform.position);
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
      
      //Stop when arrived at Target Waypoint
      if (currentWP < graph.getPathLength())
      {
         //Set goal equal the last waypoint
         goal = graph.getPathPoint(currentWP);

         //Set EndNode to Calculate Distance
         endNode = goal;
         
         //LookAtWayPoint
         tracker.transform.LookAt(goal.transform);
         
         //Move forward with Z Axis
         tracker.transform.Translate((Vector3.forward * trackerSpeed) * Time.deltaTime);
      }
   }
   
   private void HandleRotate()
   {
      //Look at Tracker smoothest
      var lookAtTracker = Quaternion.LookRotation(tracker.transform.position - car.transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTracker, rotateSpeed * Time.deltaTime);
   }
   
   public void MoveTo(int indexWP)
   {
      //A* calculate
      graph.AStar(currentNode, waypoints[indexWP]);
   }
}
