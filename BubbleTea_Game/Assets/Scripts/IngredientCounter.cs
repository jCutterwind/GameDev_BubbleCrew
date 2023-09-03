using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class IngredientCounter : MonoBehaviour
{
    [SerializeField] private Ingredient[] ingredients;
    public Ingredient[] Ingredients { set => ingredients = value; }
    private List<IngredientQuantityData> quantities;
    public List<IngredientQuantityData> Quantities { set => quantities = value; }
    [SerializeField] private IngredientDisplayer[] displayer;

   public void Aggiorna(IngredientQuantityData ingr)
    {
        for (int i = 0; i < quantities.Count; i++)
        {
            if (ingr.ingredient == quantities[i].ingredient)
            {
                quantities[i].quantity++;        
                break;
            }
        }

        updateDisplayer();
    }

    public void updateDisplayer()
    {
        for(int i=0; i < quantities.Count; i++)
        {
            displayer[i].setIng(quantities[i]);
            if (quantities[i].quantity == 0)
            {
                Color color = displayer[i].Image.color;
                color.a = 0.35f;
                displayer[i].Image.color = color;   
            }
            else
            {
                Color color = displayer[i].Image.color;
                color.a = 1;
                displayer[i].Image.color = color;
            }
        }
    }

    public void Restart()
    {
        quantities= new List<IngredientQuantityData>();
        for (int i = 0; i < displayer.Length; i++)
        {
            IngredientQuantityData ingr=new IngredientQuantityData();
            ingr.quantity=0;
            ingr.ingredient = ingredients[i];
            quantities.Add(ingr);
        }

        updateDisplayer();
    }
}