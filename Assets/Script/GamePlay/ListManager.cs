using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public static ListManager Instance;
    public List<Converter> Stations;
    public List<Converter> buylist;
    public List<string> list;
    public List<Image> buyImglist;
    public Sprite check;
    public int index = 0;

    private void Awake()
    {
        Instance = this;
        list = new List<string>();
        buylist = new List<Converter>();
        
    }
    private void Start()
    {
        GenList();

    }
 
    public void GenList()
    {
        int rand;
        switch (GameManager.Instance.difficulty)
        {
            case GameManager.Difficulty.MEDIUM:
                rand = Random.Range(3, 5);
                break;
            case GameManager.Difficulty.HARD:
                rand = Random.Range(4, 9);
                break;
            default:
                rand = Random.Range(3, 5);
                break;
        }

        for (int i = 0; i < rand; i++)
        {
            int a = Random.Range(0, Stations.Count);
            Addlist(a);
        }
        //buylist.Add(Stations[0]);
        
        indeximg = 0;

    }
    public bool canMove = false;
    int indeximg = 0;
    int indeximgMax = 0;

    public void SetReady()
    {
        canMove = true;
    }

    public void Addlist(int rand)
    {

        if (!buylist.Contains(Stations[rand]))
        {
            buylist.Add(Stations[rand]);
            list.Add(Stations[rand].Station);

            buyImglist[indeximg].sprite = Stations[rand].sprite;

            Stations[rand].col = Stations[rand].transform.GetChild(0).gameObject;

            Stations[rand].col.gameObject.SetActive(true);

            if (GameManager.Instance.difficulty == GameManager.Difficulty.HARD)
            {
                Stations[rand].col.GetComponent<ObjRef>().arrow = Stations[rand].col.transform.GetChild(0).gameObject;
                Stations[rand].col.GetComponent<ObjRef>().arrow.SetActive(false);
            }

            indeximg++;
            indeximgMax = indeximg;
        }
        else
        {
            Addlist(Random.Range(0, Stations.Count));
        }
    }

    public void checkObj(Converter ID)
    {   
        for (int i = 0; i < buylist.Count; i++)
        {
            if (ID == buylist[i])
            {
                foreach(Image img in buyImglist)
                {
                    if (img.sprite == ID.sprite)
                    {
                        img.sprite = check;
                        break;

                    }
                }
                break;
            }
        }

    } 
    public void removeObj(Converter ID)
    {   
        for (int i = 0; i < buylist.Count; i++)
        {
            if (ID == buylist[i])
            {
                foreach(Image img in buyImglist)
                {
                    if (img.sprite == ID.sprite)
                    {
                        img.gameObject.SetActive(false);
                        Debug.Log("Removed");
                        break;

                    }
                }
                break;
            }
        }

    } 




}
