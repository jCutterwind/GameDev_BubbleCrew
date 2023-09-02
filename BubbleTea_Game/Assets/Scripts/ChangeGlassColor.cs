using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGlassColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRend;

    public void changeColor()
    {
        spriteRend.color = Color.HSVToRGB(Random.value, 0.25f, 0.90f);
    }
}
