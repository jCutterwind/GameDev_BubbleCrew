using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuItemDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform panel;
    [SerializeField] private IngredientDisplayer ingDisp;
    [SerializeField] private float sizeMult;


    public void setMenuItem(MenuItem item)
    {
        this.text.text = item.name;
        foreach(IngredientQuantityData ing in item.ingredientQuantities)
        {
            IngredientDisplayer ing1 = Instantiate(ingDisp);
            ing1.setIng(ing);
            ing1.gameObject.transform.localScale *= sizeMult;
            ing1.transform.SetParent(panel, false);
        }
    }
}
