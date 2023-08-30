using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richiamo : MonoBehaviour
{
    [SerializeField] private FinalPanel pannello;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.K))
        {
            pannello.GameOver();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            pannello.closePanel();
        }

    }
}
