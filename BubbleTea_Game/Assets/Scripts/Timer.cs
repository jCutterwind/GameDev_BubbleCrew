using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    Image timerBar;
    public float tempo = 100f;
    float tempoRimasto;
    public GameObject GameOverText;
    public GameObject BackMenuButton;
    void Start()
    {
        GameOverText.SetActive(false);
        BackMenuButton.SetActive(false);
        timerBar = GetComponent<Image>();
        tempoRimasto = tempo;
    }
    private void Update()
    {
        tempoRimasto -= 1 * Time.deltaTime;
        timerBar.fillAmount = tempoRimasto/tempo;
         if (tempoRimasto<=0)
        {
            GameOverText.SetActive(true);
            BackMenuButton.SetActive(true);
            tempoRimasto = 0;
        }
    }
    
}
