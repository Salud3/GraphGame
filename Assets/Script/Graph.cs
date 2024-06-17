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

        while (target != null && target.GetCEdge() != null && target.GetCEdge().GetFromNode() != null)
        {
            target = target.GetCEdge().GetFromNode();

            pathNodes.Add(target);
        }

        pathNodes.Reverse();

        return pathNodes;
    }

    bool djrunning = false;
    public void Dijktras(Node inicio, Node meta)
    {
        if (!djrunning)
        {
            Debug.Log(inicio.weight.ToString() + " " + meta.weight.ToString());
            VisitNode(inicio, 0, null);
            bool searching = true;


            while (searching)
            {
                Edge FromEdge = GetNxtEdge();

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
    public void VisitNode(Node visited, float val,Edge fromEdge)
    {
        float valo = val;

        if (fromEdge != null)
        {
            fromEdge.SetVisited(true);
            valo += fromEdge.GetFromNode().GetWeight();//+Heuristica en A*
        }


        if (!visited.GetVisit())
        {
            visited.SetVisit(true);
            visited.SetWeight(valo);
            GraphMaster.Instance.MarkNode(visited);


            if (fromEdge != null)
            {
                visited.SetCEdge(fromEdge);
            }

        }
        else
        {
            if (visited > valo)
            {
                visited.SetWeight(valo);

                if (fromEdge != null)
                {
                    visited.SetCEdge(fromEdge);
                }

            }
        }

        GetEdgesAlt(visited);
    
    }


    public void GetEdgesAlt(Node n)//Agregamos los Edges del nodo visitado a la lista
    {
        List<Edge> edges = n.edges;

        foreach (Edge edge in edges)
        {
            if (!edge.GetVisited() && !edgesToVisit.Contains(edge))
            {
                edgesToVisit.Add(edge);
            }
        }
    }

    public Edge GetNxtEdge()//Revisamos el Edge hasta arriba de la lista
    {
        Edge edgeTempMax;
        if (edgesToVisit.Count == 0)
        {
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
                    //calcular distancia para meta A*
                }
            }
        }
        edgesToVisit.Remove(edgeTempMax);

        return edgeTempMax;
    }
   
}
