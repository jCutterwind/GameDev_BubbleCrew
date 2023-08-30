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

    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private int minMoves;
    [SerializeField] private int maxMoves;
    [SerializeField] private int starScoreMultiplier;
    [SerializeField][Range(0.1f, 1.0f)] private float minTolerance;
    [SerializeField][Range(1, 4)] private int maxMult;
    [SerializeField] private float timePerMove;

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
        maxMoves = minMoves * maxMult;   
    }

    private void getTimes()
    {
        minTime = minMoves * timePerMove;
        maxTime = maxMoves * timePerMove;
    }

    public void setInfo(int moves, float timeTook, List<IngredientQuantityData> playerIngs)
    {
        this.moves = moves;
        this.timeTook = timeTook;
        this.playerOrder = playerIngs;
        checkScore();
    }

    private void checkScore()
    {
        extraTime = 0;
        float totScore = minMoves + minTime;
        float maxScore = maxMoves + maxTime + getTotScore(currentOrder) * maxMoves;
        //float currentScore = Mathf.Clamp(moves, minMoves, maxMoves) + Mathf.Clamp(timeTook, minTime, maxTime);
        float currentScore = Mathf.Clamp(moves, minMoves, maxMoves) + minTime + orderAccuracyMalus() * maxMoves;
        float floatScore = totScore / currentScore;
        int starScore = (int)Mathf.Clamp(floatScore * 5, 1, 5);

        //int starScore = ((int)Mathf.Floor((floatScore)));


        Debug.Log("FloatScorw = " + floatScore + ", Current Score = " + currentScore + "StarScore = " + starScore);

        extraTime += (starScore + timePerMove) * getTimeBonus();
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
            List<IngredientQuantityData> oldList = new List<IngredientQuantityData>(playerOrder);

            foreach(IngredientQuantityData ing in oldList)
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
        setTotScore();
        getMoves();
        getTimes();
        checkPlayerOrder();
    }

    private float getTimeBonus()
    {
        return (GameManager.instance.DiffMultiplier*-1)+1;
    }
}
