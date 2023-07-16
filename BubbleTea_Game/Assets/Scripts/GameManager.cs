using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private Ingredient[] teas; 
    [SerializeField] private Ingredient[] toppings;
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private int maxQuantity;
    [SerializeField] private Character client;
    private List<IngredientQuantityData> currentIngs;

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
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewTurn()
    {
        currentIngs.Clear();
        currentIngs.Add(addRandomIng(teas));
        currentIngs.Add(addRandomIng(toppings));
        client.setCharacter(characterSprites[Random.Range(0, characterSprites.Length)], currentIngs);


    }

    IngredientQuantityData addRandomIng(Ingredient[] ings)
    {
        IngredientQuantityData result = new IngredientQuantityData();
        int index = Random.Range(0, ings.Length);
        //Debug.Log(index);
        result.ingredient = ings[index];
        result.quantity = Random.Range(1, maxQuantity);
        Debug.Log("added ... " + result.ToString());
        return result;
    }

    
}
