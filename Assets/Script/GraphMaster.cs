using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GraphMaster : MonoBehaviour
{
    public static GraphMaster Instance;
    
    
    public Graph grafo;
    public List<Converter> gameObjects = new List<Converter>();

    private Dictionary<Node, GameObject> nodeMap = new Dictionary<Node, GameObject>();
    private Dictionary<GameObject,Node> gameObjMap = new Dictionary<GameObject, Node>();

    public List<Vector3> vectors = new List<Vector3>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    
    }
    public Node GetDictNode(GameObject gm)
    {
        return gameObjMap[gm];
    }
    public GameObject GetDictGm(Node gm)
    {
        return nodeMap[gm];
    }

    public void MarkNode(Node node)
    {
        GameObject nod = nodeMap[node];
        nod.GetComponent<Renderer>().material.color = Color.green;
    }
    public void MarkNodeR(Node node)
    {
        GameObject nod = nodeMap[node];
        nod.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Start()
    {
        Converter[] Nodos = FindObjectsOfType<Converter>();

        foreach (Converter nod in Nodos)
        {
            gameObjects.Add(nod);
        }

        GenGraph();

    }

    public void Regenerate()
    {
        foreach(Converter obj in gameObjects)
        {
            obj.ResetNode();
            MarkNodeR(obj.node);
        }
    }

    private void GenGraph()
    {

        List<Node> nodes = new List<Node>();

        foreach (Converter obj in gameObjects)
        {
            Node tempnode = new Node();
            nodes.Add(tempnode);
            obj.SetNode(tempnode);
            nodeMap.Add(tempnode, obj.gameObject);
            gameObjMap.Add(obj.gameObject, tempnode);
        }

        grafo = new Graph(nodes);

        gameObjects.Sort(delegate (Converter n1, Converter n2) { return n1.ID.CompareTo(n2.ID); });

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetEdges();
        }

    }

    public void Dijkstras(Node n1, Node n2)
    {
        switch (GameManager.Instance.difficulty)
        {
            case GameManager.Difficulty.MEDIUM:
                grafo.Dijktras(n1, n2);
                break;
            case GameManager.Difficulty.HARD:
                //grafo.Astar(n1, n2);
                break;
            default:
                grafo.Dijktras(n1, n2);
                break;

        }

    }

                



}
