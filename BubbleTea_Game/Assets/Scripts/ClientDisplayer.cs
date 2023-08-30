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
    private List<IngredientDisplayer> currentIngDisp = new List<IngredientDisplayer>();
    private TMP_Text currentText;

    [SerializeField][Range(0.1f,5f)] private float animSpeed;
    private Vector2 startPos;
    private Vector2 endPos;
    



    // Start is called before the first frame update
    public void displayIngs(IngredientQuantityData[] ings)
    {
        if (ings != null)

            foreach(IngredientQuantityData ing in ings)
            {
                IngredientDisplayer ingDisp1 = Instantiate(ingDisp);
                currentIngDisp.Add(ingDisp1);
                ingDisp1.setIng(ing);
                ingDisp1.transform.SetParent(panel, false);
            }
        
    }

    public void displayText(string str)
    {
        currentText = Instantiate(text);
        currentText.text = str;
        currentText.transform.SetParent(panel, false);
    }

    public void displaySprite(Sprite sprite)
    {
        this.spriteRend.sprite = sprite;
    }

    public void Clear()
    {
        if (currentIngDisp!=null)
        {
            foreach(IngredientDisplayer ingDisp in currentIngDisp)
            {
                Destroy(ingDisp.gameObject);
                
            }
            currentIngDisp.Clear();

        }
        if(currentText!=null)
        {
            Object.Destroy(currentText.gameObject);
        }
    }

    private void Update()
    {
    }

    private void MoveClient()
    {
        this.transform.position = Vector2.Lerp(startPos, endPos, Time.deltaTime * animSpeed);
    }

    public void newClient()
    {
        endPos = transform.position;
        startPos = endPos - new Vector2(Screen.width, 0);
    }

    public void endClient()
    {
        startPos = transform.position;
        endPos = startPos + new Vector2(Screen.width, 0);
    }
}
