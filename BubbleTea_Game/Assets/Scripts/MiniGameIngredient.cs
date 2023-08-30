using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniGameIngredient : MonoBehaviour
{
    private Ingredient ingredient;
    [SerializeField][Range(0.1f, 1.0f)] private float scaleMult;
    [SerializeField] private Vector2Int gridPosition;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 currentPosition, currentScale;
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    //[SerializeField] private float offset;
    
    public Ingredient Ingredient { get => ingredient; }
    public Vector3 CurrentPosition { get => currentPosition; set=>currentPosition = value; }
    public Vector3 CurrentScale { get => currentScale; set => currentScale = value; }
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
        transform.localScale *= scaleMult;
        this.currentScale = transform.localScale;
    }

    private void Update()
    {
        //if (this.currentPosition != this.transform.position)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, this.currentPosition, Time.deltaTime * speed);
        //}
        if (this.currentScale != this.transform.localScale)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.currentScale, Time.deltaTime * speed);
        }

        if (Vector3.Distance(currentPosition, transform.position) > offset)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.currentPosition, Time.deltaTime * speed);
        }
        else
        {
            this.transform.position = currentPosition;
        }
        //if (Vector2.Distance(currentScale, transform.localScale) > offset)
        //{
        //    this.transform.localScale = Vector2.Lerp(this.transform.localScale, this.currentScale, Time.deltaTime * speed);
        //}
        //else
        //{
        //    this.transform.position = currentPosition;
        //}
    }
}


