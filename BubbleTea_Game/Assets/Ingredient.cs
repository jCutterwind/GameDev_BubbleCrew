using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    public string type;
    public Sprite icon;
    public int ID;
    public override string ToString()
    {
        return "Ingredient type = " + type + ", ID = " + ID + ".";
            
    }
}
