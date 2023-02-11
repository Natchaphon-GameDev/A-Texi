using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Link
{
    public enum direction
    {
        Uni,
        BI
    };

    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    [Header("Setup Waypoints")]
    public List<GameObject> waypoints;
    [Header("Setup Node Link")]
    [SerializeField] private List<Link> links;
    
    public Graph graph = new Graph();

    private void Start()
    {
        //Setup Node
        if (waypoints.Count > 0)
        {
            //Add Note to Each Waypoint
            foreach (var wp in waypoints)
            {
                graph.AddNode(wp);
            }
            
            //Add Edge to node 1, 2
            foreach (var link in links)
            {
                //Check Direction Bi or Uni
                if (link.dir == Link.direction.Uni)
                {
                    graph.AddEdge(link.node1,link.node2);
                }
                else if (link.dir == Link.direction.BI)
                {
                    graph.AddEdge(link.node1,link.node2);
                    graph.AddEdge(link.node2,link.node1);
                }
            }
        }
    }

    private void Update()
    {
        //DrawGraph
        graph.debugDraw();
    }
}
