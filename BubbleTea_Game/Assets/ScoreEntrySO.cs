using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreEntrySO", order = 3)]
public class ScoreEntrySO : ScriptableObject
{
    private string nameText;
    private int score;


    public string NameText { get => nameText; set => nameText = value; }
    public int Score { get => score; set => score = value; }

}
