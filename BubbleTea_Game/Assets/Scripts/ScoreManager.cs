using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum starTier
{
    OneStar, TwoStar, ThreeStar
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int upScore = 0;
    [SerializeField] private int downScore = 0;
    [SerializeField] private int maxIncrement = 5;
    [SerializeField] private ScoreMultiplier scoreMultiplier;
    [SerializeField] private starTier currentTier = starTier.TwoStar;
    [SerializeField] private int starCounter = 0;
    [SerializeField] private int penaltyMult;


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
        updateTier(1);
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
        updateTier(-1);
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

    private void updateTier(int i)
    {
        if (i < 0) i *= penaltyMult;
        this.starCounter += i;
        if(starCounter>maxIncrement)
        {
            starCounter = 0;
            this.currentTier++;
            if (currentTier > starTier.ThreeStar) this.currentTier = starTier.ThreeStar;
        }
        if(starCounter<0)
        {
            starCounter = maxIncrement;
            this.currentTier--;
            if (currentTier < starTier.OneStar) this.currentTier = starTier.OneStar;
        }
    }

}
