using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum Difficulty { MEDIUM = 1, HARD = 2 };
    public Difficulty difficulty;
    [SerializeField] bool lose;
    [SerializeField] bool win;

    private void Awake()
    {
        //difficulty = Difficulty.MEDIUM;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        AudioManager.Instance.ChargeMusicLevel();

    }
    public bool GetWin()
    {
        return win;
    }
    public bool GetLose()
    {
        return lose;
    }
    public void SetWin()
    {
        if (lose)
        {
            win = false;
        }
        else
        {
            win = true;
        }
    }
    public void SetLose()
    {
        if (win)
        {
            lose = false;
        }
        else
        {
            lose = true;
        }
    }

    public void AutoDestruction()
    {
        Destroy(gameObject);
    }
    //Aqui son cargas de scena
    public void Loadscene()
    {
        Invoke("sceneman", 1f);
    }
    private void sceneman()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nextSceneIndex));

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
