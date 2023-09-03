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
    public Ingredient[] Ingredients { set => ingredients = value; }
    private Vector2 gridSize;
    private MiniGameIngredient[,] ingredientsList;
    [SerializeField] private Transform upperLeftCorner;
    private float height, width;
    [SerializeField] private GridManager gridManager;
    [SerializeField][Range(1.1f, 1.9f)] private float toll;
    [SerializeField] private IngredientCounter ingrCounter;
    public IngredientCounter IngrCounter { get => ingrCounter; }

    [SerializeField] private ChangeGlassColor glass;


    
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
        gridSize = new Vector3(width/(cols-1), height/(rows-1),0);
        gridManager.Offset = Mathf.Abs(Mathf.Min(gridSize.x, gridSize.y))/2;

        ingredientsList= new MiniGameIngredient[rows, cols];

        Create();


        //if (miniGameIngredient != null && ingredients != null)
        //{
        //    //Create();
        //    gridManager.IngredientsList = this.ingredientsList;
        //    //gridManager.Ingredients=this.ingredients;
        //}

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    public void setIngredients(Ingredient[] ings )
    {
        this.ingredients = ings;
    }
    public void Create()
    {
        for(int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < cols; j++)
            {
                ingredientsList[i, j] = Instantiate(miniGameIngredient, new Vector3(upperLeftCorner.position.x - gridSize.x * j, upperLeftCorner.position.y- gridSize.y * i,upperLeftCorner.position.z), Quaternion.identity);
                //IngredientGenerator(new Vector2Int (i,j));
                //ingredientsList[i, j].name = "Ingredient #" + count;
                //count++;
            }
        }

        gridManager.IngredientsList = this.ingredientsList;
    }


    public void Restart(Ingredient[] ingredients)
    {
        glass.changeColor();
        this.ingredients = ingredients;
        for (int i = 0;i < rows; i++)
        {
            for (int j=0; j < cols; j++)
            {
                IngredientGenerator(new Vector2Int(i, j));
            }
        }
        this.ingrCounter.Ingredients = this.ingredients;
        this.ingrCounter.Restart();

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
