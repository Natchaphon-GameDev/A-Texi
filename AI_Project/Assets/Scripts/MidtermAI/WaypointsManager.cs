using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LinkNode
{
    public enum direction
    {
        Uni,
        UniLoop, //Same as Uni but link first node with end node
        BI
    };

    [Header("Setup Main Node")]
    public GameObject mainNode;
    
    [Header("Setup Pair Node")]
    public List<GameObject> connectNodes;
    
    [Header("Choose Direction link these nodes")]
    public direction dir;
}

public class WaypointsManager : MonoBehaviour
{
    [Header("Setup Waypoints")]
    public List<GameObject> waypoints;
    
    [Header("Setup Node Link")]
    [SerializeField] private List<LinkNode> links;
    
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
            
            //Classified by Direction 
            foreach (var link in links)
            {
                //Check Direction
                if (link.dir == LinkNode.direction.Uni)
                {
                    //Add Edge main node to first pair node
                    graph.AddEdge(link.mainNode,link.connectNodes[0]);

                    //Add Edge inorder classified by index
                    for (int i = 0; i < link.connectNodes.Count; i++)
                    {
                        //if code run at before the last index => Continue to next link 
                        if (i == link.connectNodes.Count - 1)
                        {
                            continue;
                        }
                        
                        graph.AddEdge(link.connectNodes[i],link.connectNodes[i + 1]);
                    }
                }
                else  if (link.dir == LinkNode.direction.UniLoop)
                {
                    graph.AddEdge(link.mainNode,link.connectNodes[0]);

                    for (int i = 0; i < link.connectNodes.Count; i++)
                    {
                        if (i == link.connectNodes.Count - 1)
                        {
                            //Link Last node to main node 
                            graph.AddEdge(link.connectNodes[link.connectNodes.Count - 1],link.mainNode);
                            continue;
                        }
                        
                        graph.AddEdge(link.connectNodes[i],link.connectNodes[i + 1]);
                    }
                }
                else if (link.dir == LinkNode.direction.BI)
                {
                    //link every pair node to main node with 2 direction    
                    foreach (var node in link.connectNodes)
                    {
                        graph.AddEdge(link.mainNode,node);
                        graph.AddEdge(node,link.mainNode);
                    }
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
