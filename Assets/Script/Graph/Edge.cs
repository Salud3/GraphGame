using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Edge
{
    private float distance;
    private float weight;

    private bool visited;
    
    public Node toNode;
    public Node fromNode;

    public Edge(Node ToNode, Node FromNode, float w)
    {
        weight = w;
        this.toNode = ToNode;
        this.fromNode = FromNode;
        visited = false;
    }

    public Node GetToNode()
    {
        return toNode;
    }

    public Node GetFromNode()
    {
        return fromNode;
    }

    public void SetWeight(float w)
    {
        weight = w;
    }
    public float GetWeight()
    {
        return weight;
    }

    public void SetDistance(float dist)
    {
        distance = dist;
    }
    public float GetDistance()
    {
        return distance;
    }

    public void SetVisited(bool v)
    {
        visited = v;
    }

    public bool GetVisited()
    {
        return visited;
    }

    public static  bool operator < (Edge edgeI, Edge edgeJ)
    {
        return edgeI.weight < edgeJ.weight;
    }

    public static  bool operator > (Edge edgeI, Edge edgeJ)
    {
        return edgeI.weight > edgeJ.weight;
    }


}
