using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Ingredient[] teas; 
    [SerializeField] private Ingredient[] toppings;
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private int maxQuantity;
    [SerializeField] private Character characterModel;
    // Start is called before the first frame update
    void Start()
    {
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewTurn()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients.Add(addRandomIng(teas));
        ingredients.Add(addRandomIng(toppings));
        Character character = Instantiate(characterModel);
        character.setCharacter(characterSprites[Random.Range(0, characterSprites.Length - 1)], ingredients);


    }

    Ingredient addRandomIng(Ingredient[] ings)
    {
        int index = Random.Range(0, ings.Length);
        Debug.Log(index);
        Ingredient result = ings[index];
        result.quantity = Random.Range(1, maxQuantity);
        Debug.Log("added ... " + result.ToString());
        return result;
    }

    
}
