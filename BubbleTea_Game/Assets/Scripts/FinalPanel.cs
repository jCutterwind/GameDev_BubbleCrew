using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class FinalPanel : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TextMeshProUGUI score;
    
    private Vector3 currentScale;
    [SerializeField] private float speed=3;
    void Start()
    {
        this.gameObject.SetActive(false);
        this.currentScale = transform.localScale;
        this.transform.localScale= Vector3.zero;
    }

    
    void Update()
    {

        if(this.isActiveAndEnabled)
        {
            if (this.transform.localScale != currentScale)
            {
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.currentScale, Time.deltaTime*speed);
            }
            
        }
        
    }

    public void GameOver()
    {
        this.gameObject.SetActive(true);
        cameraController.ReturnCenter();
        score.text = "Score: " + MaxScoreCounter.instance.Score;
        
        

    }

    public void closePanel()
    {
        this.gameObject.SetActive(false);
        
        this.transform.localScale= Vector3.zero;
    }
}
