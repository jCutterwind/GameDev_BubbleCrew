using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreation : MonoBehaviour
{
    [SerializeField] private int rows, cols;
    [SerializeField] private MiniGameIngredient miniGameIngredient;
    [SerializeField] private Ingredient[] ingredients;
    private Vector2 gridSize;
    [HideInInspector] public List<MiniGameIngredient> ingredientList;
    [SerializeField] private Transform upperLeftCorner;
    private float height, width;


    
    void Start()
    {
        height = (upperLeftCorner.transform.position.y - this.transform.position.y) *2;
        width = (upperLeftCorner.transform.position.x - this.transform.position.x) *2;
        gridSize = new Vector2(width/(cols-1), height/(rows-1));

        if (miniGameIngredient != null && ingredients != null)
        {
            Create();
        }
        
    }

   
    //void Update()
    //{
        
    //}

    private void Create()
    {
        for(int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < cols; j++)
            {
                MiniGameIngredient currentIng = Instantiate(miniGameIngredient, new Vector2(upperLeftCorner.position.x - gridSize.x * j, upperLeftCorner.position.y- gridSize.y * i), Quaternion.identity);
                currentIng.setIngredient(ingredients[Random.Range(0,ingredients.Length)], new Vector2(i,j));
                ingredientList.Add(currentIng);
            }
        }
    }
}
