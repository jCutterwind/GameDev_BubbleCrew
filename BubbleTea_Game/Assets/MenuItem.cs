using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MenuItem", order = 3)]
public class MenuItem : ScriptableObject
{
    public string itemName;
    public IngredientQuantityData[] ingredientQuantities;
    
}
