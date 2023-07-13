using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    public string type;
    public Sprite icon;
    public int ID;
    public int quantity = 1;
    public override string ToString()
    {
        return "Ingredient type = " + type + ", quantity = " + quantity + ", ID = " + ID + ".";
            
    }
}
