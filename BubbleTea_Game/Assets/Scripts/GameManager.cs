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

public enum turnType
{
    RANDOM, MENU, PERSON
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private AllIngredients ingredients;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private ClientManager clientManager;

    [SerializeField] private int maxQuantity;

    private List<IngredientQuantityData> currentIngs;

    [SerializeField] private diff difficultySetting;
    private int score;

    //FOR RANDOM CHECKER
    //[SerializeField][Range(0.1f, 0.9f)] private float randomThresh;
    [SerializeField][Range(0.1f, 0.9f)] private static float mediumThresh, hardThresh;
    public static float MediumThresh => mediumThresh;
    public static float HardThresh => hardThresh;

    [Serializable] private class weightedEntry
    {
        public float[] weights = new float[3];
        public turnType type;
    }

    [SerializeField] private weightedEntry[] weights;

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
        if(Input.GetKeyDown("space"))
        {
            NewTurn();
        }
    }

    void NewTurn()
    {
        turnType turn1 = weightedTurnChoice();
        Debug.Log(turn1);
        switch(turn1)
        //switch(weightedTurnChoice())
        {
            case turnType.RANDOM:
                RandomOrder();
                //Debug.Log("RANDOM ORDER");
                break;
            case turnType.MENU:
                MenuItemOrder();
                //Debug.Log("MENU ITEM ORDER");
                break;
            case turnType.PERSON:
                Debug.Log("PERSONALITY ORDER");
                break;  
        }
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
        //Debug.Log("added ... " + result.ToString());
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

    private turnType weightedTurnChoice()
    {
        int index = ((int)difficultySetting);
        //Debug.Log(index);
        turnType turn = 0;
        float totWeight = 0;
        foreach (weightedEntry weight in weights)
        {
            totWeight += weight.weights[index];
        }

        float randomWeight = Random.Range(0, totWeight);

        if (randomWeight < weights[0].weights[index])
        {
            turn = turnType.RANDOM;
        }
        else if (randomWeight < weights[0].weights[index] + weights[1].weights[index])
        {
            turn = turnType.MENU;
        }
        else
        {
            turn = turnType.PERSON;
        }
        return turn;
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

    private void MenuItemOrder()
    {
        currentIngs.Clear();
        MenuItem order = menuManager.getRandomMenuItem();
        currentIngs = order.ingredientQuantities.ToList<IngredientQuantityData>();
        clientManager.createNamedOrderChar(order);
        //client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], order);
    }


    private void setMenu()
    {
        menuManager.newMenu(difficultySetting);
    }


    //Deprecati
    /*
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

    */

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
