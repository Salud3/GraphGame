using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]

public class Graph
{
    public List<Node> nodes;
    public Node final;

    public Graph()
    {
        nodes = new List<Node>();
    }public void SetFinal(Node f)
    {
        final = f;
    }

    public Graph(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    private List<Edge> edgesToVisit = new List<Edge>();

    public List<Node> GeneratePath(Node target)
    {
        List<Node> pathNodes = new List<Node>();

        pathNodes.Add(target);
        bool search = true;
        while (search)
        {
            if (target != null && target.GetCEdge() != null && target.GetCEdge().GetFromNode() != null)
            {
                target = target.GetCEdge().GetFromNode();
                
                pathNodes.Add(target);
            }
            else
            {
                search = false;
            }
        }

        pathNodes.Reverse();

        return pathNodes;
    }

    bool djrunning = false;
    public void Dijktras(Node inicio, Node meta)// Astar implementado desde la busqueda de nodo
    {
        GraphMaster.Instance.Regenerate();
        if (!djrunning)
        {
            Debug.Log(inicio.weight.ToString() + " " + meta.weight.ToString());
            VisitNode(inicio, 0, null);
            bool searching = true;


            while (searching)
            {
                Edge FromEdge = GetNxtEdge();
                //Debug.Log(FromEdge.GetFromNode().convert.ID.ToString() + " apunta a " + FromEdge.GetToNode().convert.ID.ToString());
                Node next = FromEdge.GetToNode();

                if (next == meta)
                {
                    GraphMaster.Instance.MarkNode(next);
                    VisitNode(next, FromEdge.GetWeight(), FromEdge);
                    searching = false;
                }
                else
                {
                    GraphMaster.Instance.MarkNode(next);
                    VisitNode(next, FromEdge.GetWeight(), FromEdge);

                }
            }
            djrunningOff();
        }
        

    }

    public IEnumerator djrunningOff()
    {
        yield return new WaitForSeconds(1);
        djrunning = false;
    }
    public void VisitNode(Node visited, float val, Edge fromEdge)
    {
        AddEdgesToVisit(visited);
        float newWeight = val;

        if (fromEdge != null)
        {
            fromEdge.SetVisited(true);
            newWeight += fromEdge.GetToNode().GetWeight();

        }

        bool isVisited = visited.GetVisit();
        float currentWeight = visited.GetWeight();

        if (!isVisited || newWeight < currentWeight)
        {
            visited.SetVisit(true);
            visited.SetWeight(newWeight); // Asigna la heurística en A*
            GraphMaster.Instance.MarkNode(visited);

            if (fromEdge != null)
            {
                visited.SetCEdge(fromEdge);
            }
        }

    }

    public void AddEdgesToVisit(Node node)
    {
        List<Edge> edges = node.edges;

        foreach (Edge edge in edges)
        {
            if (!edge.GetVisited() && !edgesToVisit.Contains(edge))
            {
                edgesToVisit.Add(edge);
            }
        }
        edgesToVisit.Sort(delegate (Edge n1, Edge n2) { return n1.GetWeight().CompareTo(n2.GetWeight()); });


    }


    public Edge GetNxtEdge()//Revisamos el Edge hasta arriba de la lista
    {
        Edge edgeTempMax;
        if (edgesToVisit.Count == 0)
        {
            Debug.Log("edgesToVisit vacio");

            return null;
        }
        else
        {
            edgeTempMax = edgesToVisit[0];
        }

        for (int i = 0; i < edgesToVisit.Count; i++)
        {
            if (i != 0)
            {
                if (edgesToVisit[i] < edgesToVisit[i - 1])
                {
                    edgeTempMax = edgesToVisit[i];
                }
            }
        }
        edgesToVisit.Remove(edgeTempMax);

        return edgeTempMax;
    }
   
}
