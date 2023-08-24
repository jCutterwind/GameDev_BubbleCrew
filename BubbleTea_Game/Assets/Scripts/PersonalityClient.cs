using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityClient : Client
{
    public PersonalityClient(ClientDisplayer clientDisplayer, Sprite sprite) : base(clientDisplayer, sprite)
    {
    }

    public override void setClient()
    {
        clientDisplayer.displaySprite(this.sprite);
    }
}
