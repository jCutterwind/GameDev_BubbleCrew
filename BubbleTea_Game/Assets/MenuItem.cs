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
        int quantityCounter = 0;
        foreach(IngredientQuantityData quant in ingredientQuantities)
        {
            if(quant.ingredient.difficulty > startDiff)
            {
                startDiff = quant.ingredient.difficulty;
                quantityCounter += quant.quantity;
            }
        }
        startDiff += (quantityCounter/4);  
        if(startDiff>diff.HARD)
        {
            startDiff = diff.HARD;
        }
        return startDiff;
    }
 
}
