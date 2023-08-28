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
    private MiniGameIngredient[,] ingredientsList;
    public MiniGameIngredient[,] IngredientsList { set=>  ingredientsList = value; }    

    private MiniGameIngredient ingr1, ingr2;
    private bool hasChanged = false;
    [SerializeField] private bool isDragging=false, isChecking=false;
    private float offset;
    public float Offset { set=>offset = value; }

    private Ingredient[] ingredients;
    public Ingredient[] Ingredients { set=> ingredients = value; }

    private ArrayList ingredientQuantity;
    [SerializeField] private int seconds=3;
    [SerializeField] private Transform glassPosition;

    private int mosse = 0;
    


    void Start()
    {
        ingredientQuantity= new ArrayList();
        Vector2Int[] vett;

        if (ingredientsList != null && ingredients != null)
        {

            vett=CheckMatch3();
            while (hasChanged)
            {
                IngredientGenerator(vett[0], vett[1], vett[2]);
                vett=CheckMatch3();

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

        if (isDragging && ingr1 != null && !isChecking)
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
                mosse++;
                isDragging = false;
                isChecking = true;
                StartCoroutine(AnimationMatch());

                //addIngredient(CheckMatch3());
                //while (hasChanged)
                //{
                //    //while (isAnimation)
                //    //{

                //    //}
                //    //yield return new WaitForSeconds(seconds);
                //    addIngredient(CheckMatch3());
                //}

            }

        }
        else if (input.x < ingr1.transform.position.x - offset)
        {
            if (ingr1.GridPosition.y > 0)
            {
                SwitchPos(ingr1.GridPosition.x, ingr1.GridPosition.y -1);
                mosse++;
                isDragging = false;
                isChecking = true;
                StartCoroutine(AnimationMatch());

                //addIngredient(CheckMatch3());
                //while (hasChanged)
                //{
                //    //while (isAnimation)
                //    //{

                //    //}
                //    //yield return new WaitForSeconds(seconds);
                //    addIngredient(CheckMatch3());
                //}

            }
        }
        else if (input.y > ingr1.transform.position.y + offset)
        {
            if (ingr1.GridPosition.x > 0)
            {
                SwitchPos(ingr1.GridPosition.x-1, ingr1.GridPosition.y);
                mosse++;
                isDragging = false;
                isChecking = true;
                StartCoroutine(AnimationMatch());

                //addIngredient(CheckMatch3());
                //while (hasChanged)
                //{
                //    //while (isAnimation)
                //    //{

                //    //}
                //    //yield return new WaitForSeconds(seconds);
                //    addIngredient(CheckMatch3());
                //}

            }
        }
        else if (input.y < ingr1.transform.position.y - offset)
        {
            if (ingr1.GridPosition.x + 1 < ingredientsList.GetLength(0))
            {
                SwitchPos(ingr1.GridPosition.x+1, ingr1.GridPosition.y);
                mosse++;
                isDragging = false;
                isChecking = true;
                StartCoroutine(AnimationMatch());

                //addIngredient(CheckMatch3());
                //while (hasChanged)
                //{
                //    //while (isAnimation)
                //    //{

                //    //}
                //    //yield return new WaitForSeconds(seconds);
                //    addIngredient(CheckMatch3());
                //}

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

    
    private Vector2Int[] CheckMatch3()
    {
        hasChanged = false;

        Vector2Int[] vett=new Vector2Int[3];
        

        for (int i = 0; i < ingredientsList.GetLength(0); i++)
        {
            for (int j = 0; j + 2 < ingredientsList.GetLength(1); j++)
            {
                if (ingredientsList[i, j].Ingredient.ID == ingredientsList[i, j + 1].Ingredient.ID)
                {
                    if (ingredientsList[i, j + 1].Ingredient.ID == ingredientsList[i, j + 2].Ingredient.ID)
                    {
                        hasChanged = true;

                        vett[0] = new Vector2Int(i, j);
                        vett[1] = new Vector2Int(i, j + 1);
                        vett[2] = new Vector2Int(i, j + 2);
                        


                        return vett;
                    }
                    //else
                    //    j++;
                }
            }
        }

        for (int j = 0; j < ingredientsList.GetLength(1); j++)
        {
            for (int i = 0; i + 2 < ingredientsList.GetLength(0); i++)
            {
                if (ingredientsList[i, j].Ingredient.ID == ingredientsList[i + 1, j].Ingredient.ID)
                {
                    if (ingredientsList[i + 1, j].Ingredient.ID == ingredientsList[i + 2, j].Ingredient.ID)
                    {
                        hasChanged = true;

                        vett[0] = new Vector2Int(i, j);
                        vett[1] = new Vector2Int(i + 1, j);
                        vett[2] = new Vector2Int(i + 2, j);

                        return vett;

                    }
                    //else
                    //    i++;
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

    private void IngredientGenerator(Vector2Int vec1, Vector2Int vec2, Vector2Int vec3)
    {
        ingredientsList[vec1.x, vec1.y].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
        ingredientsList[vec2.x, vec2.y].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
        ingredientsList[vec3.x, vec3.y].setIngredient(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
    }
   
    

    IEnumerator AnimationMatch()
    {
        yield return new WaitForSeconds(seconds);

        Vector3 pos1, pos2, pos3, scala;

        Vector2Int[] vett = CheckMatch3();
        
        while (hasChanged)
        {
            addIngredient(ingredientsList[vett[0].x, vett[0].y].Ingredient);

            pos1 = ingredientsList[vett[0].x, vett[0].y].transform.position;
            pos2 = ingredientsList[vett[1].x, vett[1].y].transform.position;
            pos3 = ingredientsList[vett[2].x, vett[2].y].transform.position;
            scala= ingredientsList[vett[2].x, vett[2].y].transform.localScale;

            ingredientsList[vett[0].x, vett[0].y].CurrentPosition = glassPosition.position;
            ingredientsList[vett[1].x, vett[1].y].CurrentPosition = glassPosition.position;
            ingredientsList[vett[2].x, vett[2].y].CurrentPosition = glassPosition.position;

            yield return new WaitForSeconds(seconds);

            ingredientsList[vett[0].x, vett[0].y].transform.position = pos1;
            ingredientsList[vett[1].x, vett[1].y].transform.position = pos2;
            ingredientsList[vett[2].x, vett[2].y].transform.position = pos3;

            ingredientsList[vett[0].x, vett[0].y].CurrentPosition = pos1;
            ingredientsList[vett[1].x, vett[1].y].CurrentPosition = pos2;
            ingredientsList[vett[2].x, vett[2].y].CurrentPosition = pos3;


            //// yield return new WaitForSeconds(seconds);

            IngredientGenerator(vett[0], vett[1], vett[2]);

            ingredientsList[vett[0].x, vett[0].y].transform.localScale = Vector3.zero;
            ingredientsList[vett[1].x, vett[1].y].transform.localScale = Vector3.zero;
            ingredientsList[vett[2].x, vett[2].y].transform.localScale = Vector3.zero;

            yield return new WaitForSeconds(seconds);

            vett = CheckMatch3();
        }

        isChecking = false;
        StopCoroutine(AnimationMatch());
    }
}
