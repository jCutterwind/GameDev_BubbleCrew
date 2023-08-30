using UnityEngine;

public enum type
{
    TEA, TOPPING
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ingredient", order = 1)]

public class Ingredient : ScriptableObject
{
    public string ingredient;
    public type type;
    public diff difficulty;
    public Sprite icon;
    public int ID;
    public override string ToString()
    {
        return "Ingredient type = " + ingredient + ", ID = " + ID + ".";
            
    }
}
