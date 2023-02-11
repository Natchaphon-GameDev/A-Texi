using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Setup Speed")]
    [SerializeField] private float carSpeed = 10f;
    [SerializeField] private float trackerSpeed = 20f;
    [SerializeField] private float rotateSpeed = 1f;
    
    [Header("Setup Distance")]
    [SerializeField] private float overDistance = 1f;
    [SerializeField] private float overTrackerDistance = 5f;
    
    [Header("Setup Waypoints")]
    [SerializeField] private List<GameObject> waypoints;
    
    [Header("Do not touch")] //For Debug
    [SerializeField] private GameObject tracker;

    //Set CurrentWaypoint
    private int currentWP = 0;
    
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
    }
    
    private void Update()
    {
        TrackerHandle();
        MoveHandle();
    }

    private void TrackerHandle()
    {
        var track = tracker.transform;

        //Check Distance from Track and Car
        var trackerDistance = Vector3.Distance(transform.position, track.position);

        //Stop when Distance more than overTrackerDistance
        if (trackerDistance > overTrackerDistance)
        {
            return;
        }

        //LookAtWayPoint
        track.LookAt(waypoints[currentWP].transform);

        //Move forward with Z Axis
        track.Translate((Vector3.forward * trackerSpeed) * Time.deltaTime);

        var distance = Vector3.Distance(track.position, waypoints[currentWP].transform.position);

        //Setup next waypoint
        if (distance < overDistance)
        {
            currentWP++;
        }

        //Reset to first Waypoint
        if (currentWP >= waypoints.Count)
        { 
            currentWP = 0;
        }
    }

    private void MoveHandle()
    {
        //LookAtTracker
        var lookAtTracker = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookAtTracker,rotateSpeed * Time.deltaTime);
        
        //Move forward with Z Axis
        transform.Translate((Vector3.forward * carSpeed) * Time.deltaTime);
    }
}

