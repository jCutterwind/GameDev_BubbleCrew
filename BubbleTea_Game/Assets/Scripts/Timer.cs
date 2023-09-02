using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    [SerializeField] private Image timerBar;
    public float tempo = 100f;
    private float tempoRimasto;

    private bool isTimer = false;
    void Start()
    {
        //GameOverText.SetActive(false);
        //BackMenuButton.SetActive(false);
        timerBar = GetComponent<Image>();
        tempoRimasto = tempo;
    }
    private void Update()
    {
        if(isTimer)
        {
            tempoRimasto -= 1 * Time.deltaTime;
            timerBar.fillAmount = tempoRimasto / tempo;
            if (tempoRimasto <= 0)
            {
                //GameOverText.SetActive(true);
                //BackMenuButton.SetActive(true);
                tempoRimasto = 0;
            }
        }
    }

    public void addTime(float time)
    {
        tempoRimasto += time;
    }

    public void StartTimer()
    {
        isTimer = true;
    }
    
}
