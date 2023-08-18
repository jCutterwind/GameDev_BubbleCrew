using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class ClientDisplayer : MonoBehaviour
{

    public SpriteRenderer spriteRend;
    [SerializeField] private IngredientDisplayer ingDisp;
    [SerializeField] private RectTransform panel;
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    public void displayIngs(IngredientQuantityData[] ings)
    {
        if (ings != null)
        {
            foreach (IngredientQuantityData ing in ings)
            {
                IngredientDisplayer ingDisp1 = Instantiate(ingDisp);
                ingDisp1.setIng(ing);
                ingDisp1.transform.SetParent(panel, false);

            }
        }
    }

    public void displayText(MenuItem item)
    {
        string order = "Salve, vorrei un " + item.name + ", per favore";
        TMP_Text orderName = Instantiate(text);
        orderName.text = order;
        orderName.transform.SetParent(panel, false);
    }

    public void displaySprite(Sprite sprite)
    {
        this.spriteRend.sprite = sprite;
    }

}
