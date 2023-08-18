using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    [SerializeField] private Sprite[] clientSprites;
    [SerializeField] private ClientDisplayer clientDisp;
    private Client currentClient;

    
    public void createRandomOrderChar(IngredientQuantityData[] ings)
    {
        currentClient = new RandomOrderClient(clientDisp, randomSprite(), ings);
        currentClient.setClient();
    }
    
    public void createNamedOrderChar(MenuItem item)
    {

    }
    
    public void createPersonalityChar()
    {

    }

    private Sprite randomSprite()
    {
        if (clientSprites != null)
        {
            return clientSprites[Random.Range(0, clientSprites.Length)];
        }
        else return null;
    }

}
