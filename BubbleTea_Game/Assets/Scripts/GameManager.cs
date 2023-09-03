using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;

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

    private diff difficultySetting;
    public diff DifficultySetting { get => difficultySetting; set => difficultySetting = value; }

    [SerializeField] private int orderCounter;
    //public static int OrderCounter { get; set; }
    [SerializeField] private int ordersNumToMenuReset = 5;
    [SerializeField] private float diffMultiplier;
    [HideInInspector] public float DiffMultiplier { get => diffMultiplier; }
    [SerializeField][Range(0.2f, 1.0f)] private float diffMultConst;

    //FOR RANDOM CHECKER
    //[SerializeField][Range(0.1f, 0.9f)] private float randomThresh;
    //[SerializeField][Range(0.1f, 0.9f)] private float mediumThresh, hardThresh;
    //public float MediumThresh => mediumThresh;
    //public float HardThresh => hardThresh;

    [SerializeField] private UnityEvent resetMenuEvent;

    [Serializable] private class weightedEntry
    {
        public float[] weights = new float[3];
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
        resetMenu();
        NewTurn();
    }

    // Update is called once per frame
    private void updateDiffMultiplier()
    {
        diffMultiplier = ((float)difficultySetting + 1 )/ ((float)diff.HARD + 1);
        //Debug.Log("DIFF MULTIPLIER = " + diffMultiplier + ", DIFF MULT WITH CONST = " + diffMultiplier*diffMultConst);
    }

    public void changeDiff(int i)
    {
        bool reset = true;
        difficultySetting += i;
        if (difficultySetting < diff.EASY)
        {
            difficultySetting = diff.EASY;
            reset = false;
        } else if (difficultySetting > diff.HARD)
        {
            difficultySetting = diff.HARD;
            reset = false;
        }
        if(reset)
        {
            resetMenuEvent.Invoke();
        }
    }

    public void NewTurn()
    {
        checkMenuReset();
        updateDiffMultiplier();

        if(Random.value < 0.5f)
        {
            RandomOrder();
        } else
        {
            MenuItemOrder();
        }
    }

    IngredientQuantityData addRandomIng(Ingredient[] ings)
    {
        IngredientQuantityData result = new IngredientQuantityData();
        Ingredient ing = new Ingredient();
        bool diffCheck=false;
        bool contains = false;
        do
        {
            ing = ings[Random.Range(0, ings.Length)];
            contains = containsIng(ing);
            diffCheck = (int) ing.difficulty > (int) difficultySetting;
        } 
        while (diffCheck || contains);
        result.ingredient = ing;
        int quantity = 1;
        if(Random.value>diffMultiplier)
        {
            quantity = 1;
        }
        else
        {
            quantity = Random.Range(1, (int)difficultySetting + 3);
        }
        result.quantity = quantity;
        //Debug.Log("added ... " + result.ToString());
        return result;
    }

    private bool containsIng(Ingredient ingCheck)
    {
        bool result = false;
        foreach(IngredientQuantityData ingQuant in currentIngs)
        {
            if (ingQuant.ingredient.Equals(ingCheck))
            {
                result = true;
            }
        }

        return result;
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
        if(Random.value < diffMultiplier*diffMultConst)
        {
            currentIngs.Add(addRandomIng(ingredients.toppings));

            if (Random.value < 0.5f && difficultySetting > diff.MEDIUM)
            {
                currentIngs.Add(addRandomIng(ingredients.toppings));
            }
        }
        sendIngredients();
        clientManager.createRandomOrderChar(currentIngs.ToArray());
        //client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], currentIngs);

    }

    private void MenuItemOrder()
    {
        currentIngs.Clear();
        MenuItem order = MenuManager.instance.getRandomMenuItem();
        currentIngs = order.ingredientQuantities.ToList<IngredientQuantityData>();
        sendIngredients();
        clientManager.createNamedOrderChar(order);
        //client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], order);
    }


    public void resetMenu()
    {
        resetMenuEvent.Invoke();
    }

    private void checkMenuReset()
    {
        orderCounter++;
        if(orderCounter>=ordersNumToMenuReset/Mathf.Sqrt(diffMultiplier))
        {
            orderCounter = 0;
            Debug.Log("RESET MENU!");
            resetMenu();
        }
    }

    private void sendIngredients()
    {
        OrderChecker.instance.setCurrentOrder(currentIngs);
        Ingredient[] ings = new Ingredient[6];
        Ingredient[] allIngs = ingredients.getAllIngredients();
        for (int i = 0; i < currentIngs.Count; i++)
        {
            ings[i] = currentIngs[i].ingredient;
        }
        for(int i=currentIngs.Count;i<6;i++)
        {
            Ingredient newIng;
            do
            {
                newIng = allIngs[Random.Range(0, allIngs.Length)];
            }
            while (ings.ToList().Contains(newIng));
            ings[i] = newIng;
        }
        GridCreation.Instance.Restart(ings);
    }

    private void OnDestroy()
    {
        if(instance==this)
        {
            instance = null;
        }
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

}
