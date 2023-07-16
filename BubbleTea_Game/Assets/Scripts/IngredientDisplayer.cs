using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientDisplayer : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text textMeshPro;

   public void setIng(Sprite image, string textMeshPro)
    {
        this.image.sprite = image;
        this.textMeshPro.text = textMeshPro;
    }

    public void setIng(Ingredient ing)
    {
        this.image.sprite = ing.icon;
        this.textMeshPro.text = "x" + ing.quantity;
    }
}
