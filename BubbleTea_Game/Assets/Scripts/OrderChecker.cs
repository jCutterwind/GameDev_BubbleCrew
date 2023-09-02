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

    [SerializeField] private Timer timer;

    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private int minMoves;
    [SerializeField] private int maxMoves;
    [SerializeField] private int orderMalus;

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
        int diffMult = 1;

        if (currentOrder!=null)
        {
            foreach(IngredientQuantityData ing in currentOrder)
            {
                diffMult += (int) ing.ingredient.difficulty;
            }
        }

        minMoves = totOrderScore + (int)(totOrderScore * minTolerance * diffMult);
        maxMoves = minMoves * maxMult;   
    }

    private void getTimes()
    {
        minTime = minMoves * timePerMove;
        maxTime = maxMoves * timePerMove;
        this.orderMalus = getTotScore(currentOrder) * minMoves / 2;
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
        float maxScore = maxMoves + maxTime + getTotScore(currentOrder)*minMoves/1.2f;
        //float currentScore = Mathf.Clamp(moves, minMoves, maxMoves) + Mathf.Clamp(timeTook, minTime, maxTime);
        float currentScore = Mathf.Clamp(moves, minMoves, maxMoves) + minTime + getPlayerOrderScore()*minMoves/1.2f;
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

    private int getPlayerOrderScore()
    {
        if(playerOrder!=null && currentOrder!=null)
        {
            int counter = getTotScore(currentOrder);
            foreach(IngredientQuantityData ing1 in playerOrder)
            {
                foreach(IngredientQuantityData ing2 in currentOrder)
                {
                    if (ing1.ingredient.ingredient == ing2.ingredient.ingredient)
                    {
                        //int newMesure = (int)Mathf.Abs(ing1.quantity - ing2.quantity);

                        counter -= ing1.quantity;
                        extraTime += 0.35f * getTimeBonus() * ((float)ing1.ingredient.difficulty + 1) * ing1.quantity;
                        //Debug.Log(ing1.ingredient.name + " found in currOrder. Counter is " + counter + ", incremented by " + newMesure);
                    }
                    else
                    {
                        Debug.Log(ing1.ingredient.name + " is EXTRA");
                        extraTime += getTimeBonus() * ((float)ing1.ingredient.difficulty + 1) * ing1.quantity;
                    }


                }
            }
            sendExtraTime();
            Debug.Log("Counter FULL is " + counter);
            return counter;

        //    List<IngredientQuantityData> newList = new List<IngredientQuantityData>(playerOrder);

        //    foreach(IngredientQuantityData ing in playerOrder)
        //    {
        //        foreach(IngredientQuantityData currIng in currentOrder)
        //        {
        //            if(ing.ingredient == currIng.ingredient)
        //            {
        //                ing.quantity--;
        //                if(ing.quantity <= 0)
        //                {
        //                    playerOrder
        //                }

        //            }
        //        }
        //        //if(currentOrder.Contains(ing.ingredient))
        //        //{
        //        //    Debug.Log("Removed " + ing.ingredient.name + " Because not contained in order as seen by " + !currentOrder.Contains(ing));
        //        //    playerOrder.Remove(ing);
        //        //    extraTime += 0.35f * getTimeBonus() * ((float) ing.ingredient.difficulty + 1);
        //        //}
        //    }

        }
        return -1;
    }

    private bool containsIngredient(List<IngredientQuantityData> ings, Ingredient ing1)
    {
        bool result = false;
        foreach(IngredientQuantityData ing in ings)
        {
            if (ing.ingredient == ing1)
            {
                result = true;
            }
        }
        return result;
    }
    public void setCurrentOrder(List<IngredientQuantityData> ings)
    {
        this.currentOrder = ings;
        Debug.Log("CurrentOrder has " + currentOrder.Count + " ings");
        setTotScore();
        getMoves();
        getTimes();
    }

    private float getTimeBonus()
    {
        return (GameManager.instance.DiffMultiplier*-1)+1;
    }

    private void sendExtraTime()
    {
        timer.addTime(extraTime);
    }
}
