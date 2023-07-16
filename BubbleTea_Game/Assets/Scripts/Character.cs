using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private IngredientDisplayer ingDisp;
    [SerializeField] private RectTransform panel;
  

    public void setCharacter(Sprite sprite, List<Ingredient> ingredients)
    {
        this.spriteRend.sprite = sprite;
        this.ingredients = ingredients;
        displayIngs();       
    }

    private void displayIngs()
    {
        if(ingredients!=null)
        {
            foreach (Ingredient ing in ingredients)
            {
                IngredientDisplayer ingDisp1 = Instantiate(ingDisp);
                ingDisp1.setIng(ing);
                ingDisp1.transform.SetParent(panel, false);

            }
        }
    }

    public void Start()
    {

    }
}
