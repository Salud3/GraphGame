using System;
using System.Collections.Generic;
using UnityEngine;

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
        cEdge = null;
        visited = false;
        weight = 0;
        foreach (Edge e in edges)
        {
            e.SetVisited(false);
        }
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
        if (GameManager.Instance.difficulty == GameManager.Difficulty.HARD)
        {
            GameObject gmtemp;
            if (IAMovement.Instance.objCompra < IAMovement.Instance.objects.Count)
            {
                gmtemp = IAMovement.Instance.objects[IAMovement.Instance.objCompra].gameObject;
            }
            else
            {
                gmtemp = IAMovement.Instance.Salida.convert.gameObject;
            }
            weight = _weight + (Math.Abs(convert.gameObject.transform.position.x - GraphMaster.Instance.GetDictNode(gmtemp).convert.transform.position.x) 
                + Math.Abs(convert.gameObject.transform.position.z - GraphMaster.Instance.GetDictNode(gmtemp).convert.transform.position.z));
        }
        else
        {
            weight = _weight;
        }
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
    
    public static bool operator >= (Node a, float b)
    {
        return a.GetWeight() >= b;
    }

    public static bool operator <= (Node a, float b)
    {
        return a.GetWeight() <= b;
    }



}
