using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController chc;
    public Rigidbody rb;
    public float speed = 4f;
    public Camera cam;
    private new Transform camera;

    //public SpriteSimpleChanges sprites;

    public float gravity = -9.8f;

    [Header("Animacion")]

    public Animator animator;

    private void Start()
    {
        chc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();

    }

    void Movement()
    {
        float gradesx = Camera.main.transform.rotation.eulerAngles.y;
    



        // Movimiento
        float hor;
        float ver;
        if (ismoveactive())
        {
            hor = Input.GetAxis("Horizontal");
            ver = Input.GetAxis("Vertical");
        }
        else
        {
            hor = 0;
            ver = 0;
        }
         
         

        Vector3 movement = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();


            Vector3 direction = forward * ver + right * hor;
            direction.Normalize();

            movement = direction * speed * Time.deltaTime;

            //transform.rotation = Quaternion.LookRotation(direction);

            //transform.rotation = Quaternion.Euler(direction);
        }

        //Activar Animaciones
        animator.SetBool("Walking", movement != new Vector3(0,0,0));

        transform.LookAt(movement+this.transform.position);
        movement.y += gravity * Time.deltaTime;
        chc.Move(movement);
        //transform.LookAt(new Vector3(movement.x + this.transform.position.x, 0.7f,movement.z + this.transform.position.z) );

    }

    bool canmove()
    {
        bool a = GameControl.Instance.canvasHide.activeInHierarchy;
        bool b = GameControl.Instance.pauseMenu.activeInHierarchy;
        bool c;

        if (a || b)
        {
            c = false;
        } else {
            
            c = true;
        
        }

        return c;
    }

    void Update()
    {

        if(canmove()) 
        {
            Movement();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        Detect();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameControl.Instance.PauseMenu();
        }

    }

    bool ismoveactive()
    {
        return ListManager.Instance.canMove; 

    }
    [Header("Detectores")]
    public float timeToCollect = 1.5f;
    public float timeCollecting;
    public Converter converter;
    public bool inrange;
    public int objColect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objeto")
        {
            converter = other.GetComponent<ObjRef>().Converter;
            inrange = true;
        }

        if (other.tag == "Finish" )
        {
            Debug.Log("Detected Collision");
            if (objColect > ListManager.Instance.buylist.Count - 2)
            {
                GameManager.Instance.SetWin();
                GameControl.Instance.Finish();

            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Objeto")
        {
            converter = null;
            inrange = false;
            timeCollecting = 0;
        }
    }

    void Detect()
    {
        if (inrange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                timeCollecting += Time.deltaTime;
                if (timeCollecting >= timeToCollect)
                {
                    if (GameManager.Instance.difficulty == GameManager.Difficulty.HARD)
                    {
                        ListManager.Instance.removeObj(converter);
                        Debug.Log("Removed");
                    }
                    else
                    {
                        ListManager.Instance.checkObj(converter);
                    }
                    converter.col.gameObject.SetActive(false);
                    timeCollecting = 0;
                    converter = null;
                    objColect++;
                    if (objColect >= ListManager.Instance.buylist.Count-1)
                    {
                        GameControl.Instance.fin[0].GetComponent<Collider>().isTrigger = true;
                        GameControl.Instance.fin[1].GetComponent<Collider>().isTrigger = true;
                    }
                }
            }
        }
    }

}