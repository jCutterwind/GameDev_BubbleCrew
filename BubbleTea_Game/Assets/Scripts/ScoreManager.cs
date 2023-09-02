using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum starTier
{
    OneStar = 1, TwoStars = 2, ThreeStars = 3, FourStars = 4, FiveStars = 5
}

[Serializable] public class FixedFloatQueue
{
    [SerializeField] private int _size;
    public int Size { get => this._size; set => this._size = value; }

    [SerializeField] Queue<float> _queue = new Queue<float>();

    public FixedFloatQueue(int size)
    {
        this._size = size;
    }

    public void Enqueue(float obj)
    {
        _queue.Enqueue(obj);
        while(_queue.Count > _size)
        {
            _queue.Dequeue();
        }
    }

    public float getAverage()
    {
        float result = 0.0f;
        foreach(float obj in _queue)
        {
            result += obj;
        }
        if(result<= 0)
        {
            return 0;
        }
        else
        {
            return result / _queue.Count;
        }
    }
}


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private int upScore = 0;
    [SerializeField] private int downScore = 0;
    [SerializeField] private int maxIncrement = 5;
    [SerializeField] private int scoreMultiplier = 1;
    [SerializeField] private starTier currentTier;

    [SerializeField] private int scoreMult = 0;
    [SerializeField] private FixedFloatQueue oldStarsCounter;
    [SerializeField] private int oldStarMaxNum = 5;
    [SerializeField] private float currentStarNum = 3.3f;

    [SerializeField] private float totalScore = 0;
    public float TotalScore { get => totalScore; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalScore = 0;
        oldStarsCounter = new FixedFloatQueue(oldStarMaxNum);
        FMODController.instance.setStar(starTier.ThreeStars);
        updateStars();
    }   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            updateStarsAverage(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            updateStarsAverage(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            updateStarsAverage(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            updateStarsAverage(4);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            updateStarsAverage(5);
        }
    }

    public void updateDiff(float newPoints)
    {
        if(newPoints<=3)
        {
            if (upScore > 0)
            {
                upScore -= 2;
            }
            if (upScore <= 0)
            {
                upScore = 0;

                this.downScore+=(int)newPoints;
                if (downScore > maxIncrement)
                {
                    downScore = 0;
                    GameManager.instance.changeDiff(-1);
                }
            }
        }
        else
        {
            if (downScore > 0)
            {
                downScore -= 2;
            }
            if (downScore <= 0)
            {
                downScore = 0;

                this.upScore+=((int)newPoints-3);
                if (upScore > maxIncrement)
                {
                    upScore = 0;
                    GameManager.instance.changeDiff(1);
                }
            }
        }
    }

    public void updateStarsAverage(float newPoints)
    {
        this.oldStarsCounter.Enqueue(newPoints);
        this.currentStarNum = this.oldStarsCounter.getAverage();
        updateDiff(newPoints);
        updateStars();
        updateScore(newPoints);
    }   

    private void updateStars()
    {
        this.currentTier = (starTier)Mathf.FloorToInt(this.currentStarNum);
        FMODController.instance.setStar(this.currentTier);
    }


    public void updateScore(float newPoints)
    {
        this.totalScore += newPoints * (int) currentTier;
        MaxScoreCounter.instance.setScore(totalScore);
    }
}
