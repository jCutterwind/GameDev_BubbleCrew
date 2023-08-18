using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllIngredients")]

public class AllIngredients : ScriptableObject
{
    public Ingredient[] teas;
    public Ingredient[] toppings;
}
