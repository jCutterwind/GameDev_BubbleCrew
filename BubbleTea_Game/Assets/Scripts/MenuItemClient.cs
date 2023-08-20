using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemClient : Client
{

    [SerializeField] private Greetings greetings;
    private MenuItem item;
    
    public MenuItemClient(ClientDisplayer clientDisplayer, Sprite sprite, MenuItem item, Greetings greetings) : base(clientDisplayer, sprite)
    {
        this.item = item;
        this.greetings = greetings;
    }

    public override void setClient()
    {
        clientDisplayer.displaySprite(sprite);
        clientDisplayer.displayText(getText());
    }

    private string getText()
    {
        string result = null;
        if (this.item != null)
        {
            result = string.Format(this.greetings.dialogues[Random.Range(0, greetings.dialogues.Length)].text, this.item.itemName);
        }
        return result;
    }
}
