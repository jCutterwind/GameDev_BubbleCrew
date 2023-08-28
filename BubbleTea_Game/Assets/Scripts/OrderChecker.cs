using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    public static OrderChecker instance;

    private List<IngredientQuantityData> currentOrder;
    private List<IngredientQuantityData> playerOrder;

    public List<IngredientQuantityData> CurrentOrder { get => currentOrder; set => currentOrder = value; }
    public List<IngredientQuantityData> PlayerOrder { get => playerOrder; set => playerOrder = value; }

    private ScoreManager scoreManager;

    [SerializeField] private float timeBonusMult;
    private float extraTime;
    private int moves;
    private float timeTook;

    private int minMoves;
    private float minTime;

    private int maxMoves;
    private float maxTime;
    [SerializeField] private int starScoreMultiplier;
    [SerializeField][Range(0.1f, 1.0f)] private float minTolerance;
    [SerializeField][Range(0.1f, 1.0f)] private float maxTolerance;
    [SerializeField] private int maxSeconds;
    private int totOrderScore;
    private int playerOrderScore;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        } else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        this.scoreManager = ScoreManager.instance;
    }
    private void setTotScore()
    {
        if(currentOrder!=null)
        {
            totOrderScore = getTotScore(currentOrder);
        }
    }

    private void setPlayerOrderScore()
    {
        if(playerOrder!=null)
        {
            playerOrderScore = getTotScore(playerOrder);   
        }
    }
    private void getMoves()
    {
        minMoves = totOrderScore + (int)(totOrderScore * minTolerance);
        maxMoves = (minMoves * 5) + (int) ((minMoves*5) * maxTolerance);       
    }

    private void getTimes()
    {
        minTime = minMoves * maxSeconds;
        maxTime = maxMoves * maxSeconds;
    }

    private void setInfo(int moves, float timeTook)
    {
        this.moves = moves;
        this.timeTook = timeTook;
    }

    private void checkScore()
    {
        extraTime = 0;
        setTotScore();
        getMoves();
        getTimes();
        checkPlayerOrder();
        float totScore = minMoves + minTime;
        float maxScore = maxMoves + maxTime + totOrderScore;
        float currentScore = Mathf.Clamp(moves, minMoves, maxMoves) + Mathf.Clamp(timeTook, minTime, maxTime) + orderAccuracyMalus();

        int starScore = ((int)Mathf.Floor((totScore / maxScore) * 5));
        Debug.Log("StarScore = " + starScore);

        extraTime += (starScore + maxSeconds) * getTimeBonus();
        scoreManager.updateStarsAverage(starScore);
    }

    private int orderAccuracyMalus()
    {
        return getTotScore(currentOrder) - getTotScore(playerOrder);
    }

    private int getTotScore(List<IngredientQuantityData> ings)
    {
        int result = 0;

        foreach(IngredientQuantityData ing in ings)
        {
            result++;
            result += ing.quantity;
        }

        return result;
    }

    private void checkPlayerOrder()
    {
        if(playerOrder!=null && currentOrder!=null)
        {
            foreach(IngredientQuantityData ing in playerOrder)
            {
                if(!currentOrder.Contains(ing))
                {
                    playerOrder.Remove(ing);
                    extraTime += 0.35f * getTimeBonus() * ((float) ing.ingredient.difficulty + 1);
                }
            }
        }
    }

    public void setCurrentOrder(List<IngredientQuantityData> ings)
    {
        this.currentOrder = ings;
    }

    private float getTimeBonus()
    {
        return (GameManager.instance.DiffMultiplier*-1)+1;
    }
}
