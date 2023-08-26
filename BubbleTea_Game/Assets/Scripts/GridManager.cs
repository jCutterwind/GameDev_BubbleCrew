using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [HideInInspector] public MiniGameIngredient[,] ingredientsList;
    private MiniGameIngredient ingr1, ingr2;
    private bool hasChanged = false;
    [SerializeField] private bool isDragging=false;
    public float offset = 400;
    [HideInInspector] public Ingredient[] ingredients;


    void Start()
    {
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //    if (hit.collider != null)
        //    {
        //        ingr1 = hit.collider.gameObject.GetComponent<MiniGameIngredient>();
        //        isDragging = true;
        //    } 
        //}

        //if (isDragging)
        //{
        //    BorderCheck();
        //}
        
    }

    private void BorderCheck()
    {
        
        if (Input.mousePosition.x > ingr1.transform.position.x + offset)
        {
            Debug.Log("uscito dx");
            //SwitchPos(ingr1.GridPosition.x+1, ingr1.GridPosition.y);
            isDragging = false;
        }
        //else if ()
        //{

        //}
        //else if ()
        //{

        //}
        //else if ()
        //{

        //}
    }

    private void SwitchPos(int i, int j)
    {
        Vector2 oldPosition = ingr1.transform.position;
        Vector2 newPosition = ingredientsList[i,j].transform.position;
        ingredientsList[i, j].CurrentPosition = oldPosition;
        ingr1.CurrentPosition= newPosition;

        MiniGameIngredient tmp = ingr1;
        ingredientsList[ingr1.GridPosition.x, ingr1.GridPosition.y] = ingredientsList[i, j];
        ingredientsList[i, j] = tmp;

        //scambiare i gridPos
    }

    private void CheckMatch3()
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
                        Debug.Log("Match 3 !!");
                        return;
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
                        Debug.Log("Match 3 !!");
                        return;
                    }
                    else
                        i++;
                }
            }
        }

        return;
    }
}
