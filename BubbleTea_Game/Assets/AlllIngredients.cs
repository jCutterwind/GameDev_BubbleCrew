using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllIngredients")]

public class AllIngredients : ScriptableObject
{
    public Ingredient[] teas;
    public Ingredient[] toppings;

    public Ingredient[] getAllIngredients()
    {
        Ingredient[] result = teas.Concat(toppings).ToArray();
        return result;
    }


}

