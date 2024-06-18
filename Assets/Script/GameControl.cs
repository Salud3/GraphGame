using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI textCountDown;
    public GameObject canvasHide;
    public GameObject[] fin;
    [SerializeField] Animator animator;

    private void Start()
    {
        Instance = this;

    }

    public void PauseMenu()
    {
        if (pauseMenu.activeInHierarchy) {
        
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
    }

    public void Finish()
    {
        animator.SetTrigger("Fadein");
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.Loadscene();

    }

    public void Ready()
    {
        canvasHide.SetActive(false);
        textCountDown.gameObject.SetActive(true);
        textCountDown.text = "Ready?";
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2);
        textCountDown.text = "3";
        yield return new WaitForSeconds(1);
        textCountDown.text = "2";
        yield return new WaitForSeconds(1);
        textCountDown.text = "1";
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        textCountDown.text = "GO!!!";
        yield return new WaitForSeconds(1);
        textCountDown.text = "";
        ListManager.Instance.SetReady();
    }


}
