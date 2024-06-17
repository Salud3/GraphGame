using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovement : MonoBehaviour
{
    public static IAMovement Instance;
    [SerializeField] Rigidbody rg;

    [SerializeField] float speed;
    [SerializeField] float MaxForce;

    public Node ActNode;//Nodo Apuntador desde donde iniciamos
    public Node Salida;//Nodo Final para salir del supermercado
    public Node Objective;//Nodo siguiente del array para dirigirnos

    public List<Converter> objects = new List<Converter>();//Converter de los nodos de la lista
    public List<Node> PathNodes = new List<Node>();//Nodos por recorrer de punto A a punto B
    public int objCompra = -1; // index del objeto que va a buscar de la lista
    public int index;//index de los nodos en el path
    public bool canMove = false;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        Invoke("InitAll", 0.1f);
    }

    public void InitAll()
    {
        rg = GetComponent<Rigidbody>();
        CurrentVel = new Vector3(5, 0, 1);
        objects = ListManager.Instance.buylist;
        Salida = GraphMaster.Instance.gameObjects[GraphMaster.Instance.gameObjects.Count - 1].node;
        InitBusq();

    }
    IEnumerator objmas()
    {
        yield return new WaitForSeconds(3);
    }
    bool canup;
    public void InitBusq()
    {
        GraphMaster.Instance.Regenerate();
        if (canup)
        {
            objCompra++;
            canup = false;
            StartCoroutine(objmas());
        }
        objects.Sort(delegate (Converter n1, Converter n2) { return n1.ID.CompareTo(n2.ID); });

        ActNode = GraphMaster.Instance.gameObjects[0].node;

        Node inicio;
        Node meta = GraphMaster.Instance.GetDictNode(objects[0].gameObject);

        if (objCompra < objects.Count)
        {
            inicio = ActNode;
            GraphMaster.Instance.Dijkstras(inicio, meta);
        }

        PathNodes = GraphMaster.Instance.grafo.GeneratePath(meta);
        Objective = PathNodes[index];
        canMove = true;
    }
    Node meta2;
    public void InitBusq(int a)
    {
        GraphMaster.Instance.Regenerate();

        objCompra++;
        if (objCompra > ListManager.Instance.buylist.Count-1)
        {
            canMove = false;
            Debug.LogWarning("All objectives have been completed.");
            index = 0;
            GraphMaster.Instance.Regenerate();
            GraphMaster.Instance.Dijkstras(objects[objCompra-1].node, Salida);
            ActNode = Objective;
            Objective = Salida;


            PathNodes = GraphMaster.Instance.grafo.GeneratePath(Salida);

            if (PathNodes != null && PathNodes.Count > 0)
            {
                index = 0;
                Objective = PathNodes[index];
                canMove = true;
            }
            else
            {
                Debug.LogError("Failed to generate a valid path.");
                canMove = false;
            }

            return;
        }

        ActNode = Objective;
        Node meta2 = ListManager.Instance.buylist[objCompra].node;

        if (index < PathNodes.Count)
        {
            GraphMaster.Instance.Dijkstras(ActNode, meta2);
        }
        else
        {
            index = 0;
            GraphMaster.Instance.Dijkstras(ActNode, meta2);
        }

        PathNodes = GraphMaster.Instance.grafo.GeneratePath(meta2);

        if (PathNodes != null && PathNodes.Count > 0)
        {
            index = 0;
            Objective = PathNodes[index];
            canMove = true;
        }
        else
        {
            Debug.LogError("Failed to generate a valid path.");
            canMove = false;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (Vector3.Distance(this.transform.position, Objective.convert.transform.position) < 0.8f)
            {
                if (index < PathNodes.Count)
                {
                    index++;
                    if (index < PathNodes.Count)
                    {
                        //canMove = false;
                        Objective = PathNodes[index];

                        rg.ResetInertiaTensor();
                        rg.velocity = Vector3.zero;
                        rg.angularVelocity = Vector3.zero;
                    }
                }
                else
                {
                    canMove = false;
                    InitBusq(0);

                }

            }
            Movement();
        }

        
    }


    Vector3 CurrentVel= new Vector3 (01, 0, 01);
    void Movement()
    {
        Vector3 FinalCurrent = new Vector3();

        Vector3 des = DesiredVel();
        Debug.DrawLine(this.transform.position, this.transform.position+des, Color.red,0.01f);

        Vector3 steering = Steeringforce(des);
        Debug.DrawLine(this.transform.position, this.transform.position+steering, Color.yellow,0.01f);

        Vector3 temp1 = Objective.convert.transform.position - this.transform.position;
           
        if (temp1.magnitude > radius)
        {
            FinalCurrent = CurrentVel + steering;
            FinalCurrent = FinalCurrent.normalized * speed;

            rg.velocity = FinalCurrent;
            Debug.DrawLine(this.transform.position, this.transform.position+FinalCurrent, Color.magenta,0.01f);

        }
        else
        {
            rg.velocity = des;
        }


    }

    public float radius = .5f;

    Vector3 Steeringforce(Vector3 desired)
    {

        Vector3 temp = desired - CurrentVel;

        Vector3 steering = temp.normalized * MaxForce;

        return steering;
    }

    Vector3 DesiredVel()
    {
        Vector3 desired = new Vector3();
        Vector3 temp = Objective.convert.transform.position -this.transform.position;
        if (temp.magnitude > radius)
        {
            desired = temp.normalized * speed;
        }
        else
        {
            float x = Mathf.Pow(temp.x, 2);
            float y = Mathf.Pow(temp.y, 2);
            float z = Mathf.Pow(temp.z, 2);

            float temp_ = Mathf.Sqrt(x + y + z);

            temp_ /= radius;

            desired = temp.normalized * (temp_ * speed);
        }

        return desired;
    }

}
