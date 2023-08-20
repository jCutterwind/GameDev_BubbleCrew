using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int upScore = 0;
    [SerializeField] private int downScore = 0;
    [SerializeField] private int maxIncrement = 5;
    [SerializeField] private ScoreMultiplier scoreMultiplier;
    [Serializable] public class ScoreMultiplier
    {
        [SerializeField] private int scoreMult;
        public int ScoreMult => scoreMult;
        [SerializeField] private int maxScoreMult;
        [SerializeField] private int maxScoreCounter;
        [SerializeField] private int scoreMultCounter;

        public void upMult()
        {
            scoreMultCounter++;

            if(scoreMultCounter>maxScoreCounter)
            {
                if(scoreMult<=maxScoreMult)
                {
                    scoreMult++;
                    scoreMultCounter = 0;
                }
            }
        }

        public void resetMult()
        {
            scoreMult = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScoreUp(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScoreDown(1);
        }

    }

    public void ScoreUp(int i)
    {
        scoreMultiplier.upMult();
        if(downScore>0)
        {
            downScore -= 2*i;
        }
        if(downScore<=0)
        {
            downScore = 0;

            this.upScore += i;
            if (upScore > maxIncrement)
            {
                upScore = 0;
                GameManager.upDiff();
            }
        }
    }

    public void ScoreDown(int i)
    {
        scoreMultiplier.resetMult();
        if(upScore>0)
        {
            upScore -= 2*i;
        }
        if(upScore<=0)
        {
            upScore = 0;

            this.downScore += i;
            if (downScore > maxIncrement)
            {
                downScore = 0;
                GameManager.downDiff();
            }
        }
    }


}
