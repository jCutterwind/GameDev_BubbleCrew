using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [HideInInspector] public MiniGameIngredient[,] ingredientsList;
    private MiniGameIngredient ingr1, ingr2;
    private bool hasChanged = false;
    [SerializeField] private bool isDragging=false;
    public float offset;
    [HideInInspector] public Ingredient[] ingredients;
    private ArrayList ingredientQuantity;


    void Start()
    {
        ingredientQuantity= new ArrayList();

        if (ingredientsList != null && ingredients != null)
        {
            CheckMatch3();
            while (hasChanged)
            {
                CheckMatch3();
            }
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                ingr1 = hit.collider.gameObject.GetComponent<MiniGameIngredient>();
                isDragging = true;
            }
        }

        if (isDragging && ingr1 != null)
        {
            BorderCheck();
        }

    }

    private void BorderCheck()
    {
        Vector2 input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (input.x > ingr1.transform.position.x + offset)
        {
            if (ingr1.GridPosition.y + 1 < ingredientsList.GetLength(1))
            {
                SwitchPos(ingr1.GridPosition.x, ingr1.GridPosition.y+1);
                isDragging = false;

                addIngredient(CheckMatch3());
                while (hasChanged)
                {
                    addIngredient(CheckMatch3());
                }
            }
           
        }
        else if (input.x < ingr1.transform.position.x - offset)
        {
            if (ingr1.GridPosition.y > 0)
            {
                SwitchPos(ingr1.GridPosition.x, ingr1.GridPosition.y -1);
                isDragging = false;

                addIngredient(CheckMatch3());
                while (hasChanged)
                {
                    addIngredient(CheckMatch3());
                }
            }
        }
        else if (input.y > ingr1.transform.position.y + offset)
        {
            if (ingr1.GridPosition.x > 0)
            {
                SwitchPos(ingr1.GridPosition.x-1, ingr1.GridPosition.y);
                isDragging = false;

                addIngredient(CheckMatch3());
                while (hasChanged)
                {
                    addIngredient(CheckMatch3());
                }
            }
        }
        else if (input.y < ingr1.transform.position.y - offset)
        {
            if (ingr1.GridPosition.x + 1 < ingredientsList.GetLength(0))
            {
                SwitchPos(ingr1.GridPosition.x+1, ingr1.GridPosition.y);
                isDragging = false;

                addIngredient(CheckMatch3());
                while (hasChanged)
                {
                    addIngredient(CheckMatch3());
                }
            }
        }
    }

    private void SwitchPos(int i, int j)
    {
        Vector3 newPosition = ingredientsList[i, j].transform.position;
        Vector3 oldPosition = ingr1.transform.position;
      
        ingredientsList[i, j].CurrentPosition = oldPosition;
        ingr1.CurrentPosition = newPosition;
      
        MiniGameIngredient tmp = ingr1;
        ingredientsList[ingr1.GridPosition.x, ingr1.GridPosition.y] = ingredientsList[i, j];
        ingredientsList[i, j] = tmp;
      
        ingredientsList[ingr1.GridPosition.x, ingr1.GridPosition.y].GridPosition = ingr1.GridPosition;
        ingredientsList[i, j].GridPosition = new Vector2Int (i,j);
    }

    private Ingredient CheckMatch3()
    {
        hasChanged = false;

        for (int i = 0; i < ingredientsList.GetLength(0); i++)
        {
            for (int j = 0; j + 2 < ingredientsList.GetLength(1); j++)
            {
                if (ingredientsList[i,j].Ingredient.ID == ingredientsList[i, j + 1].Ingredient.ID)
                {
                    if (ingredientsList[i, j + 1].Ingredient.ID == ingredientsList[i, j + 2].Ingredient.ID)
                    {
                        ingredientsList[i, j].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
                        ingredientsList[i, j+1].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
                        ingredientsList[i, j + 2].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
                        
                        hasChanged = true;
                        return ingredientsList[i,j].Ingredient;
                    }
                    else
                        j++;
                }
            }
        }

        for (int j = 0; j < ingredientsList.GetLength(1); j++)
        {
            for (int i= 0; i + 2 < ingredientsList.GetLength(0); i++)
            {
                if (ingredientsList[i, j].Ingredient.ID == ingredientsList[i+1,j].Ingredient.ID)
                {
                    if (ingredientsList[i+1, j].Ingredient.ID == ingredientsList[i+2, j].Ingredient.ID)
                    {
                        ingredientsList[i, j].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
                        ingredientsList[i+1, j].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
                        ingredientsList[i+2, j].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);

                        hasChanged=true;
                        return ingredientsList[i, j].Ingredient;
                    }
                    else
                        i++;
                }
            }
        }

        return null;
    }

    private void addIngredient(Ingredient ingredient)
    {
        if (ingredient != null)
        {
            foreach (IngredientQuantityData element in  ingredientQuantity)
            {
                if (element.ingredient == ingredient)
                {
                    element.quantity++;
                    //Debug.Log("aumentato "+ element.ingredient.name + " " +  element.quantity);
                    return;
                }
            }
           
            IngredientQuantityData ingr= new IngredientQuantityData();
            ingr.ingredient=ingredient;
            ingr.quantity = 1;

            ingredientQuantity.Add(ingr);
            //Debug.Log("aggiunto " + ingr.ingredient.name);
        }

    }
}
