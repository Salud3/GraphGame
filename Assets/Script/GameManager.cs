using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum Difficulty { MEDIUM = 1, HARD = 2 };
    public Difficulty difficulty;

    private void Awake()
    {
        Instance = this;
        //difficulty = Difficulty.MEDIUM;
    }

    public void SetDiff(Difficulty d)
    {
        difficulty = d;
    }

}
