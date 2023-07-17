using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Character : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    [SerializeField] private List<IngredientQuantityData> ingredients;
    [SerializeField] private IngredientDisplayer ingDisp;
    [SerializeField] private RectTransform panel;
    [SerializeField] private TMP_Text text;

    public void setCharacter(Sprite sprite, List<IngredientQuantityData> ingredients)
    {
        this.spriteRend.sprite = sprite;
        this.ingredients = ingredients;
        displayIngs();       
    }

    public void setCharacter(Sprite sprite, MenuItem item)
    {
        this.spriteRend.sprite = sprite;
        this.ingredients = item.ingredientQuantities.ToList<IngredientQuantityData>();
        displayText(item);
    }

    private void displayIngs()
    {
        if(ingredients!=null)
        {
            foreach (IngredientQuantityData ing in ingredients)
            {
                IngredientDisplayer ingDisp1 = Instantiate(ingDisp);
                ingDisp1.setIng(ing);
                ingDisp1.transform.SetParent(panel, false);

            }
        }
    }

    private void displayText(MenuItem item)
    {
        string order = "Salve, vorrei un " + item.name + ", per favore";
        TMP_Text orderName = Instantiate(text);
        orderName.text = order;
        orderName.transform.SetParent(panel, false);
    }

    //Useless Now

    private List<Ingredient> GetIngredients(MenuItem item)
    {
        List<Ingredient> result = new List<Ingredient>();
        foreach(IngredientQuantityData ingQuant in item.ingredientQuantities)
        {
            result.Add(ingQuant.GetIngredient());
        }
        return result;
    }

    public void Start()
    {

    }
}
