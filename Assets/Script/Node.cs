using System;
using System.Collections.Generic;
[Serializable]
public class Node
{
    public float weight;
    public bool visited;
    public  List<Edge> edges;
    private Edge cEdge;
    public Converter convert;

    public Node()
    {
        visited = false;
        weight = 0;
        edges = new List<Edge>();
    }

    public Node(float _weight)
    {
        visited = false;
        weight = _weight;
        edges = new List<Edge>();
        cEdge = null;
    }
    public void Reset()
    {
        visited = false;
        weight = 0;
        cEdge = null;
    }

    public void SetConver(Converter conver)
    {
        convert = conver;
    }
    public void SetCEdge(Edge _edge)
    {
        cEdge = _edge;
    }

    public Edge GetCEdge()
    {
        return cEdge;
    }

    public void SetWeight(float _weight)
    {
        weight = _weight;
    }

    public float GetWeight()
    {
        return weight;
    }

    public void SetVisit(bool valu)
    {
        visited = valu;
    }

    public bool GetVisit()
    {
        return visited;
    }

    public void AddEdges(Edge edge)
    {
        edges.Add(edge);
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }

    public static bool operator > (Node a, Node b)
    {
        return a.GetWeight() > b.GetWeight();
    }

    public static bool operator < (Node a, Node b)
    {
        return a.GetWeight() < b.GetWeight();
    }
    
    public static bool operator > (Node a, float b)
    {
        return a.GetWeight() > b;
    }

    public static bool operator < (Node a, float b)
    {
        return a.GetWeight() < b;
    }



}
