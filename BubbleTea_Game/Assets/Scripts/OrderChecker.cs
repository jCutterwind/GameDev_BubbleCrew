using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    private int currentOrderNo;
    private int sentOrderNo;
    public int SentOrderNo { get=>sentOrderNo; set =>sentOrderNo=value; }
    public int CurrentOrderNo { get => currentOrderNo; set => currentOrderNo = value; }

    private int[] IntToIntArray(int num)
    {
        if (num == 0)
            return new int[1] { 0 };

        List<int> digits = new List<int>();

        for (; num != 0; num /= 10)
            digits.Add(num % 10);

        int[] array = digits.ToArray();
        System.Array.Reverse(array);

        return array;
    }

    /*
    private int[] FillArray(int[] current, int[] sent)
    {
        int sentLength = sent.Length;
        int currentLength = current.Length;

        if(sent.Length == current.Length)
        {
            return current;
        }
        else if (sent.Length<current.Length)
        {
            
        }
    }
    */

    private int checkIngredients(IngredientQuantityData[] clientOrder, IngredientQuantityData[] playerOrder)
    {
        int totScore = getTotScore(clientOrder);

        if(clientOrder.Length == playerOrder.Length)
        {
            //Un punto?
            return totScore;
        }
        else
        {
            List<IngredientQuantityData> orderList = new List<IngredientQuantityData>(clientOrder);

            foreach(IngredientQuantityData ing in playerOrder)
            {
                if(orderList.Contains(ing))
                {
                    IngredientQuantityData currentIng = orderList[orderList.IndexOf(ing)];
                    currentIng.quantity = Mathf.Abs(currentIng.quantity - ing.quantity);
                    if(currentIng.quantity==0)
                    {
                        //Altro punto 
                        orderList.Remove(currentIng);
                    }
                }
            }
            
            return totScore - getTotScore(orderList.ToArray()); 

        }

  
    }

    private int getTotScore(IngredientQuantityData[]ings)
    {
        int result = 0;

        foreach(IngredientQuantityData ing in ings)
        {
            result++;
            result += ing.quantity;
        }

        return result;
    }

}
