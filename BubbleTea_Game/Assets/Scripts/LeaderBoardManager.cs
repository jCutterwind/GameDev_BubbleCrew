using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private int numOfEntries;
    [SerializeField] private ScoreEntry scoreEntry;
    [SerializeField] private Transform panel;
    [SerializeField] private int maxScore = 2400;
    private List<ScoreEntry> entries = new List<ScoreEntry>();
    private int playerScore = 0;

    private void Start()
    {
        if (MaxScoreCounter.instance != null)
        {
            playerScore = MaxScoreCounter.instance.Score;
        }

        fillLeaderboard();
        sortList();
    }

    private void fillLeaderboard()
    {
        for(int i=0;i<numOfEntries-1;i++)
        {
            ScoreEntry currEntry = new ScoreEntry();
            currEntry.setInfo("User#" + UnityEngine.Random.Range(0, 400), (int)UnityEngine.Random.Range(10,maxScore), 0);
            entries.Add(currEntry);
        }
        ScoreEntry playerEntry = new ScoreEntry();
        playerEntry.setInfo("PLAYER", playerScore, 0);
        entries.Add(playerEntry);
    }

    static int SortByScore(ScoreEntry entry1, ScoreEntry entry2)
    {
        return entry2.Score.CompareTo(entry1.Score);
    }

    private void sortList()
    {
        entries.Sort(SortByScore);

        for(int i=0;i<entries.Count;i++)
        {
            entries[i].setIndex(i+1);
            ScoreEntry entry = Instantiate(scoreEntry, panel);
            entry.setInfo(entries[i]);
            entry.displayInfo();
        }
    }
}
