using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleControllerWinlose : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorPl;
    [SerializeField] Animator animatorIA;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        animator.SetTrigger("FadeOut");
        if (GameManager.Instance.GetLose())
        {
            text.text = "You Lose :(";
            animatorIA.SetBool("Victory",true);
            animatorPl.SetBool("Defeat",true);
        }else if (GameManager.Instance.GetWin())
        {
            text.text = "You Win";
            animatorIA.SetBool("Defeat",true);
            animatorPl.SetBool("Victory",true);
        }
        else
        {
            animatorPl.SetBool("Defeat",true);
            animatorIA.SetBool("Defeat",true);
            text.text = "Error 404";
            Debug.Log("Error");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Credits()
    {
        animator.SetTrigger("Fadein");
        GameManager.Instance.Loadscene();
    }
    //Aqui son cargas de scena
    public void MainMenu()
    {
        animator.SetTrigger("Fadein");
        Invoke("sceneman", 1f);
    }
    private void sceneman()
    {
        StartCoroutine(SceneLoad(0));
        GameManager.Instance.AutoDestruction();
    }
    public IEnumerator SceneLoad(int sceneIndex)
    {
        AudioManager.Instance.DecreaseVolume();
        yield return new WaitForSeconds(3.5f);
        AudioManager.Instance.musicSource.Stop();
        SceneManager.LoadScene(sceneIndex);
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.ChargeMusicLevel();

    }

}
