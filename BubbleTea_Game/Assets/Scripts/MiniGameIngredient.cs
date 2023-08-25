using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameIngredient : MonoBehaviour
{
    private Ingredient ingredient;
    [SerializeField] private Vector2 gridPosition;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Vector2 GridPosition { get=>gridPosition; set => gridPosition = value; }

    public void setIngredient(Ingredient ingredient, Vector2 gridPosition)
    {
        this.ingredient = ingredient;   
        this.gridPosition = gridPosition;
        if (this.ingredient != null)
        {
            this.spriteRenderer.sprite = ingredient.icon;
        }
    }

}
