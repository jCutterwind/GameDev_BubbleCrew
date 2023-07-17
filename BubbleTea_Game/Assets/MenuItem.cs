using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MenuItem", order = 3)]
public class MenuItem : ScriptableObject
{
    public string itemName;
    public IngredientQuantityData[] ingredientQuantities;

    public diff getDiff()
    {
        diff startDiff = diff.EASY;
        foreach(IngredientQuantityData quant in ingredientQuantities)
        {
            if(quant.ingredient.difficulty > startDiff)
            {
                startDiff = quant.ingredient.difficulty;
            }
        }
        return startDiff;
    }
 
}
