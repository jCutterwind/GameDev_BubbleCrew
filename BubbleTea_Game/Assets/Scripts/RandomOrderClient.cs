using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOrderClient : Client
{
    private IngredientQuantityData[] ingredients;
    public RandomOrderClient(ClientDisplayer clientDisplayer, Sprite sprite, IngredientQuantityData[] ingredients) : base(clientDisplayer, sprite)
    {
        this.ingredients = ingredients;
    }

    public override void setClient()
    {
        clientDisplayer.displaySprite(this.sprite);
        clientDisplayer.displayIngs(this.ingredients);
    }
}
