using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices.WindowsRuntime;

public enum diff
{
    EASY, MEDIUM, HARD
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private AllIngredients ingredients;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private ClientManager clientManager;




    [SerializeField] private Ingredient[] teas;
    [SerializeField] private Ingredient[] toppings;

    [SerializeField] private Sprite[] characterSprites;

    [SerializeField] private MenuItem[] fullMenu;

    private MenuItem[] currMenu;

    [SerializeField] private int maxQuantity;

    [SerializeField] private Client client;

    private List<IngredientQuantityData> currentIngs;

    [SerializeField] private diff difficultySetting;
    private int score;

    //FOR RANDOM CHECKER
    //[SerializeField][Range(0.1f, 0.9f)] private float randomThresh;
    [SerializeField][Range(0.1f, 0.9f)] private static float mediumThresh, hardThresh;
    public static float MediumThresh => mediumThresh;
    public static float HardThresh => hardThresh;
 
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIngs = new List<IngredientQuantityData>();
        setMenu();
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewTurn()
    {
        RandomOrder();  
    }

    IngredientQuantityData addRandomIng(Ingredient[] ings)
    {
        IngredientQuantityData result = new IngredientQuantityData();
        Ingredient ing = new Ingredient();
        do
        {
            ing = ings[Random.Range(0, ings.Length)];
        } 
        while (ing.difficulty > difficultySetting && returnDiffThresh(ing)<Random.value);
        result.ingredient = ing;
        result.quantity = Random.Range(1, maxQuantity);
        Debug.Log("added ... " + result.ToString());
        return result;
    }


    private float returnDiffThresh(Ingredient ing)
    {
        switch(ing.difficulty)
        {
            case diff.EASY:
                return 1;
            case diff.MEDIUM:
                return mediumThresh;
            case diff.HARD:
                return hardThresh;
            default:
                return 1;
        }
    }

    private float returnDiffThresh(diff diff)
    {
        switch (diff)
        {
            case diff.EASY:
                return 1;
            case diff.MEDIUM:
                return mediumThresh;
            case diff.HARD:
                return hardThresh;
            default:
                return 1;
        }
    }

    private bool randomChecker()
    {
        return Random.Range(0.0f, 1.0f) > 1;
    }


    private void RandomOrder()
    {
        currentIngs.Clear();
        currentIngs.Add(addRandomIng(ingredients.teas));
        currentIngs.Add(addRandomIng(ingredients.toppings));
        if(Random.value < ((float)difficultySetting)/((float)diff.HARD))
        {
            currentIngs.Add(addRandomIng(ingredients.toppings));
        }

        clientManager.createRandomOrderChar(currentIngs.ToArray());
        //client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], currentIngs);

    }

    private void NamedOrder()
    {
        currentIngs.Clear();
        //currMenu = newMenu(fullMenu);
        //menuManager.setMenu(currMenu);
        MenuItem order = currMenu[Random.Range(0, currMenu.Length)];
        currentIngs = order.ingredientQuantities.ToList<IngredientQuantityData>();
        //client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], order);
    }


    private void setMenu()
    {
        menuManager.newMenu(difficultySetting);
    }

    private List<Ingredient> item2Ing (MenuItem item)
    {
        List<Ingredient> result = new List<Ingredient>();
    
        foreach(IngredientQuantityData quant in item.ingredientQuantities)
        {
            result.Add(quant.GetIngredient());
        }

        return result;
    }


    private diff checkItemDifficulty(MenuItem item)
    {
        int difficulty=0;
        foreach(IngredientQuantityData quant in item.ingredientQuantities)
        {
            difficulty++;
            difficulty += quant.quantity;
        }
        return diff.EASY;
    }

    private bool checkContains(MenuItem item, MenuItem[] menu)
    {
        return menu.ToList<MenuItem>().Contains(item);
    }


    //FUNZIONI PER ORDER CHECKER

    private void FillArrays(ref int[] array1, ref int[] array2)
    {
        int maxLength = Math.Max(array1.Length, array2.Length);

        Array.Resize(ref array1, maxLength);
        Array.Resize(ref array2, maxLength);

        // Fill empty spaces with zeroes using Array.Clear()
        Array.Clear(array1, array1.Length, maxLength - array1.Length);
        Array.Clear(array2, array2.Length, maxLength - array2.Length);
    }

}
