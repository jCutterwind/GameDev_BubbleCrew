using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    [SerializeField] private float zOffset;

    public float Offset { set=>offset = value; }


    private List<IngredientQuantityData> ingredientQuantity;
    [SerializeField] private float seconds=3;
    [SerializeField] private Transform glassPosition;

    private bool isTimer = false;
    private float time = 0;

    private int mosse = 0;
    


    void Start()
    {
        //Vector2Int[] vett;
        //this.ingredientQuantity = new List<IngredientQuantityData>();
        //if (ingredientsList != null)
        //{

        //    vett=CheckMatch3();
        //    while (hasChanged)
        //    {
        //        GridCreation.Instance.IngredientGenerator(vett[0]);
        //        GridCreation.Instance.IngredientGenerator(vett[1]);
        //        GridCreation.Instance.IngredientGenerator(vett[2]);

        //        vett=CheckMatch3();

        //    }
        //}
        Restart();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                ingr1 = hit.collider.gameObject.GetComponent<MiniGameIngredient>();
                isDragging = true;
                isTimer = true;
            }
        }

        if (isDragging && ingr1 != null && !isChecking)
        {
            BorderCheck();
        }

        if(isTimer)
        {
            time += Time.deltaTime; 
        }

    }

    public void Restart()
    {
        mosse = 0;
        ingredientQuantity = new List<IngredientQuantityData>();
        Vector2Int[] vett;
        if (ingredientsList != null)
        {

            vett = CheckMatch3();
            while (hasChanged)
            {
                GridCreation.Instance.IngredientGenerator(vett[0]);
                GridCreation.Instance.IngredientGenerator(vett[1]);
                GridCreation.Instance.IngredientGenerator(vett[2]);

                vett = CheckMatch3();

            }
        }
    }

    private void BorderCheck()
    {
        Vector3 input = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset));
        //Vector2 input = Input.mousePosition;
        //Debug.Log("BorderCheck_INPUT X = " + input.x + " ings pos x = " + (ingr1.transform.position.x + offset  ));
        //Debug.Log("BorderCheck_INPUT Y = " + input.y + " ings pos y = " + (ingr1.transform.position.y + offset));
        if (input.x > ingr1.transform.position.x + offset)
        {
            if (ingr1.GridPosition.y + 1 < ingredientsList.GetLength(1))
            {
                SwitchPos(ingr1.GridPosition.x, ingr1.GridPosition.y+1);
                mosse++;
                isDragging = false;
                isChecking = true;
                StartCoroutine(AnimationMatch());

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
                    Debug.Log("aumentato "+ element.ingredient.name + " " +  element.quantity);
                    return;
                }
            }
           
            IngredientQuantityData ingr= new IngredientQuantityData();
            ingr.ingredient=ingredient;
            ingr.quantity = 1;

            ingredientQuantity.Add(ingr);
            Debug.Log("aggiunto " + ingr.ingredient.name);
        }

    }

    public void SendInfo()
    {
        OrderChecker.instance.setInfo(this.mosse, (int)time%60, ingredientQuantity);
        isTimer = false;
        time = 0;
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
            
            ingredientsList[vett[0].x, vett[0].y].CurrentScale = Vector3.zero;
            ingredientsList[vett[1].x, vett[1].y].CurrentScale = Vector3.zero;
            ingredientsList[vett[2].x, vett[2].y].CurrentScale = Vector3.zero;

            yield return new WaitForSeconds(seconds);


            ingredientsList[vett[0].x, vett[0].y].transform.position = pos1;
            ingredientsList[vett[1].x, vett[1].y].transform.position = pos2;
            ingredientsList[vett[2].x, vett[2].y].transform.position = pos3;

            ingredientsList[vett[0].x, vett[0].y].CurrentPosition = pos1;
            ingredientsList[vett[1].x, vett[1].y].CurrentPosition = pos2;
            ingredientsList[vett[2].x, vett[2].y].CurrentPosition = pos3;




            //// yield return new WaitForSeconds(seconds);

            GridCreation.Instance.IngredientGenerator(vett[0]);
            GridCreation.Instance.IngredientGenerator(vett[1]);
            GridCreation.Instance.IngredientGenerator(vett[2]);

            ingredientsList[vett[0].x, vett[0].y].CurrentScale = scala;
            ingredientsList[vett[1].x, vett[1].y].CurrentScale = scala;
            ingredientsList[vett[2].x, vett[2].y].CurrentScale = scala;

     
            yield return new WaitForSeconds(seconds);


            vett = CheckMatch3();

        }

        isChecking = false;
        StopCoroutine(AnimationMatch());
    }
}
