using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCarController : MonoBehaviour
{
    [SerializeField] private CityCarSO statProfile;
    
    [Header("Setup Waypoints")]
    public List<GameObject> waypoints;

    private GameObject tracker;

    //Set CurrentWaypoint
    public int currentWP;
    
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
        if (trackerDistance > statProfile.overTrackerDistance)
        {
            return;
        }

        //LookAtWayPoint
        track.LookAt(waypoints[currentWP].transform);

        //Move forward with Z Axis
        track.Translate((Vector3.forward * statProfile.trackerSpeed) * Time.deltaTime);

        var distance = Vector3.Distance(track.position, waypoints[currentWP].transform.position);

        //Setup next waypoint
        if (distance < statProfile.overDistance)
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
        transform.rotation = Quaternion.Slerp(transform.rotation,lookAtTracker,statProfile.rotateSpeed * Time.deltaTime);
        
        //Move forward with Z Axis
        transform.Translate((Vector3.forward * statProfile.carSpeed) * Time.deltaTime);
    }
}
