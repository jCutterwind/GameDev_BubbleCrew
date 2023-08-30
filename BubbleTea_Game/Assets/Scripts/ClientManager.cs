using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance;
    [SerializeField] private Sprite[] clientSprites;
    [SerializeField] private ClientDisplayer clientDisp;
    [SerializeField] private Greetings greetings;

    private Client currentClient;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentClient = null;
    }

    public void createRandomOrderChar(IngredientQuantityData[] ings)
    {
        clientCheck();
        currentClient = new RandomOrderClient(clientDisp, randomSprite(), ings);
        //Debug.Log("Is currClient null? = " + isCurrentClientNull);
        currentClient.setClient();
    }
    
    public void createNamedOrderChar(MenuItem item)
    {
        clientCheck();
        currentClient = new MenuItemClient(clientDisp, randomSprite(), item, greetings);
        currentClient.setClient();
        //Debug.Log("Is currClient null? = " + isCurrentClientNull);
        //newClientAnim();
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

   private void clientCheck()
    {
        //bool clientCheck = currentClient == null;
        //Debug.Log("CurrClient == null? " + clientCheck);
        clientDisp.Clear();        
    }

    private void checkClient()
    {
        Debug.Log("Is there a client? " + currentClient.name);
    }


}
