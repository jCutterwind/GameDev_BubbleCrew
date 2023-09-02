using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScoreCanvas1 : MonoBehaviour
{
     private IngredientQuantityData[] listaIng ;
     private TextMeshProUGUI[] text;
    [SerializeField] private Image img;
    [SerializeField] private GameObject[] circle; 
    [SerializeField] private TextMeshProUGUI prefabText;
    [SerializeField] private Ingredient ing;
    private float offsetx,offsety;
    // Start is called before the first frame update
    void Start()
    {
        listaIng = new IngredientQuantityData[5]; 
        for(int i=0; i<5; i++)
        {
            listaIng[i].ingredient = ing;
            listaIng[i].quantity = 0;
        }
        offsetx = img.rectTransform.sizeDelta.y/(listaIng.Length+1);
        offsety = img.rectTransform.sizeDelta.x /2;
        CreatePanel();
    }

    void Update()
    {
        
    }

    private void CreatePanel()
    {
        text = new TextMeshProUGUI[listaIng.Length];
       
        for(int i=0; i<listaIng.Length; i++ )
        {
            text[i] = prefabText;
            circle[i].GetComponent<SpriteRenderer>().sprite = listaIng[i].GetIngredient().icon;
            circle[i].transform.position = new Vector3(offsetx, offsety, circle[i].transform.position.z);        }

    }
    private void TextEdit()
    {
        int v ;
        for (v=0;v<5;v++)
        {
            text[v].text = "x" + listaIng[v].quantity;
            
        }
    }
}
