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
        int diffCounter = 0;
        foreach (IngredientQuantityData quant in ingredientQuantities)
        {
            quantityCounter += quant.quantity;
            diffCounter += (int)quant.ingredient.difficulty;
        }
        startDiff = (diff)(diffCounter / ingredientQuantities.Length);
        startDiff += (quantityCounter / 4);
        if (startDiff > diff.HARD)
        {
            startDiff = diff.HARD;
        }
        return startDiff;
    }

}
