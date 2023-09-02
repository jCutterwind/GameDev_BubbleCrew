using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text indexText;

    private int index;
    private int score;
    private string userName;

    public int Score { get => score; set => score = value; }

    
    public void setIndex(int i)
    {
        this.index = i;
    }

    public void setInfo(string name, int score, int index)
    {
        this.userName = name;
        this.score = score;
        this.index = index;
    }

    public void displayInfo()
    {
        this.nameText.text = this.userName;
        this.scoreText.text = this.score.ToString();
        this.indexText.text = "#" + this.index.ToString();
    }

    public void setInfo(ScoreEntry entry)
    {
        this.userName = entry.userName;
        this.score = entry.score;
        this.index = entry.index;
        displayInfo();
    }
}
