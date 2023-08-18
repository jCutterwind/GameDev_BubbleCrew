using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public abstract class Client
{
    protected ClientDisplayer clientDisplayer;
    protected Sprite sprite;

    public Client(ClientDisplayer clientDisplayer, Sprite sprite)
    {
        this.clientDisplayer = clientDisplayer;
        this.sprite = sprite;
    }

    public abstract void setClient();
}
