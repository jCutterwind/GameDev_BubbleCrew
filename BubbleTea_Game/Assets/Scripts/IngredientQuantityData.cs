[System.Serializable]

public class IngredientQuantityData
{
    public Ingredient ingredient;
    public int quantity;

    public Ingredient GetIngredient()
    {
        return ingredient;
    }
}
