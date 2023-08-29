using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GridCreation : MonoBehaviour
{
    public static GridCreation Instance;

    [SerializeField] private int rows, cols;
    [SerializeField] private MiniGameIngredient miniGameIngredient;
    [SerializeField] private Ingredient[] ingredients;
    private Vector2 gridSize;
    private MiniGameIngredient[,] ingredientsList;
    [SerializeField] private Transform upperLeftCorner;
    private float height, width;
    [SerializeField] private GridManager gridManager;
    private int count = 1;
    [SerializeField][Range(1.1f, 1.9f)] private float toll;



    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        height = (upperLeftCorner.transform.position.y - this.transform.position.y) *2;
        width = (upperLeftCorner.transform.position.x - this.transform.position.x) *2;
        gridSize = new Vector2(width/(cols-1), height/(rows-1));
        gridManager.Offset = Mathf.Abs( Mathf.Min(gridSize.x, gridSize.y))/2;

        ingredientsList= new MiniGameIngredient[rows, cols];
        

        if (miniGameIngredient != null && ingredients != null)
        {
            Create();
            gridManager.IngredientsList=this.ingredientsList;
            //gridManager.Ingredients=this.ingredients;
        }
        
    }

    private void Create()
    {
        for(int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < cols; j++)
            {
                ingredientsList[i, j] = Instantiate(miniGameIngredient, new Vector2(upperLeftCorner.position.x - gridSize.x * j, upperLeftCorner.position.y- gridSize.y * i), Quaternion.identity);
                IngredientGenerator(new Vector2Int (i,j));
                ingredientsList[i, j].name = "Ingredient #" + count;
                count++;
            }
        }
    }


    public void Restart(Ingredient[] ingredients)
    {
        this.ingredients = ingredients;
        for (int i = 0;i < rows; i++)
        {
            for (int j=0; j < cols; j++)
            {
                IngredientGenerator(new Vector2Int(i, j));
            }
        }

        gridManager.Restart();
    }

    public void IngredientGenerator(Vector2Int vett)
    {
        Ingredient ingr;
        float valore;
        

        do
        {
           
            ingr = ingredients[UnityEngine.Random.Range(0, ingredients.Length)];
            valore = UnityEngine.Random.value;
            
            
        }
        while (valore < ((float)ingr.difficulty + 1) / ((float)diff.HARD + toll));

        ingredientsList[vett.x, vett.y].setIngredient(ingr, vett);

    }
}
