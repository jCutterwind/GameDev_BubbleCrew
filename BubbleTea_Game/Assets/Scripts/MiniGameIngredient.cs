using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniGameIngredient : MonoBehaviour
{
    private Ingredient ingredient;
    [SerializeField] private Vector2Int gridPosition;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 currentPosition;
    [SerializeField] private float offset, speed;
    
    public Ingredient Ingredient { get => ingredient; }
    public Vector2 CurrentPosition { get => currentPosition; set=>currentPosition = value; }
    public Vector2Int GridPosition { get=>gridPosition; set => gridPosition = value; }

    public void setIngredient(Ingredient ingredient, Vector2Int gridPosition)
    {
        this.ingredient = ingredient;   
        this.gridPosition = gridPosition;
        if (this.ingredient != null)
        {
            this.spriteRenderer.sprite = ingredient.icon;
        }
    }

    public void setIngredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (this.ingredient != null)
        {
            this.spriteRenderer.sprite = ingredient.icon;
        }
    }

    private void Start()
    {
        this.currentPosition = transform.position;
    }

    private void Update()
    {
        if (this.currentPosition != this.transform.position)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, this.currentPosition, Time.deltaTime * speed);
        }
    }

}
