using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    public static ListManager Instance;
    public List<Converter> Stations;
    public List<Converter> buylist;
    public List<string> list;
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
                rand = Random.Range(1, 2);
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
        buylist.Add(Stations[0]);

    }

    public void Addlist(int rand)
    {
        if (!buylist.Contains(Stations[rand]))
        {
            buylist.Add(Stations[rand]);
            list.Add(Stations[rand].Station);
        }
        else
        {
            Addlist(Random.Range(0, Stations.Count));
        }
    }






}
