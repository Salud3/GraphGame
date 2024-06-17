using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public int ID;
    public string Station;
    public Node node;
    public Converter[] conexion;

    public void SetNode(Node nod)
    {
        node = nod;
        node.convert = this;
        DrawLine();
    }

    public void ResetNode()
    {
        node.Reset();
    }

    public void DrawLine()
    {
        foreach (Converter obj in conexion)
        {
            Node nodeA = node;
            foreach (Edge edge in nodeA.GetEdges())
            {
                Debug.DrawLine(obj.transform.position, this.transform.position, Color.yellow, 2);
                Debug.Log("Line Drwed");
            }
        }
        
        Invoke("DrawLine", 2);
    }
    public Node GetNode()
    {
        return node;
    }

    public void SetEdges()
    {
        if (node.convert != null)
        {

            foreach (Converter obj in conexion)
            {
                float dist = Vector3.Distance(this.gameObject.transform.position, obj.transform.position);
                Edge temp = new Edge(obj.node, node, dist);
                //Debug.Log(temp.GetFromNode().convert.gameObject.name +" "+ temp.GetToNode().convert.gameObject.name + " Dist: " + temp.GetWeight().ToString());
                node.AddEdges(temp);
            }
        }

    } 
}
