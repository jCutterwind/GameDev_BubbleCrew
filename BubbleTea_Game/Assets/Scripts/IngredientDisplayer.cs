using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientDisplayer : MonoBehaviour
{
    [SerializeField] private Image image;
    
    public Image Image { get=> image;}
    [SerializeField] private TMP_Text textMeshPro;

   public void setIng(Sprite image, string textMeshPro)
    {
        
        this.image.sprite = image;
        this.textMeshPro.text = textMeshPro;
    }

    public void setIng(IngredientQuantityData ing)
    {
        this.image.sprite = ing.ingredient.icon;
        this.textMeshPro.text = "x" + ing.quantity;
    }

    public void setQuantity(int n)
    {
        Debug.Log("aggiornato");
        this.textMeshPro.text = "x" + n;
    }

    public void Clear()
    {
        Destroy(this);
    }
}
