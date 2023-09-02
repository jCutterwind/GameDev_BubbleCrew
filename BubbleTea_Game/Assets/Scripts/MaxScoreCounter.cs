using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreCounter : MonoBehaviour
{
    public static MaxScoreCounter instance;
    private int score = 0;
    public int Score { get => score; }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        } else if (instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void setScore(float score)
    {
        if(this.score<score)
        {
            this.score = (int)score;
        }
    }
}
