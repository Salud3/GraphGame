using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRef : MonoBehaviour
{
    public Converter Converter;
    public GameObject arrow;
    public bool deactived;

    private void Start()
    {
        arrow = transform.GetChild(0).gameObject;
        arrow.SetActive(false);
        Converter = transform.parent.GetComponent<Converter>();
        if(GameManager.Instance.difficulty == GameManager.Difficulty.MEDIUM )
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }

        StartCoroutine(waitList());
    }

    IEnumerator waitList()
    {
        yield return new WaitForSeconds(2.5f);
        
        if (!ListManager.Instance.buylist.Contains(Converter))
        {
            this.gameObject.SetActive(false);
            deactived = true;
        }
        else
        {
            this.gameObject.SetActive(true);
            Debug.Log("Ya desactivado una vez antes");
        }

    }

}
