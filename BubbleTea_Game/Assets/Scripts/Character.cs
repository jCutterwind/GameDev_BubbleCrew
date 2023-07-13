using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    [SerializeField] private List<Ingredient> ingredients;

    public void setCharacter(Sprite sprite, List<Ingredient> ingredients)
    {
        this.spriteRend.sprite = sprite;
        this.ingredients = ingredients;
    }

    public void Start()
    {

    }
}
