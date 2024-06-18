
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Animator FondoNegro;

    public void SetDiffMed()
    {
        GameManager.Instance.difficulty = GameManager.Difficulty.MEDIUM;
        GameManager.Instance.Loadscene();
    }

    public void SetDiffHard()
    {
        GameManager.Instance.difficulty = GameManager.Difficulty.HARD;
        GameManager.Instance.Loadscene();
    }
    public void Exit()
    {
        Debug.Log("Saliste");
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }
    public void Reanude()
    {
        Time.timeScale = 1.0f;
    }



}
