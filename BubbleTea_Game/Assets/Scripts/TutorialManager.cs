using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Canvas[] pannelli;
    private int count = 0;
    private Vector3 currentScale;
    [SerializeField] private float speed=3;
    [SerializeField] private Canvas backgroundCanvas;
    void Start()
    {
        
        foreach (Canvas canvas in pannelli)
        {
            canvas.gameObject.SetActive(false);
        }

        this.currentScale = this.transform.localScale;
        this.pannelli[0].gameObject.SetActive(true);
        cameraController.disableButtons();

    }

    public void Avanti()
    {
        this.pannelli[count].gameObject.SetActive(false);
        
        switch (count)
        {
            case 0:
                cameraController.ReturnCenter();
                cameraController.disableButtons();
                break;
            case 2:
                cameraController.MoveLeft();
                cameraController.disableButtons();
                break;
            case 3:
                cameraController.MoveRight();
                cameraController.MoveRight();
                cameraController.disableButtons();
                break;
            case 5:
                cameraController.ReturnCenter();
                cameraController.disableButtons();
                break;
            case 6:
                break;
        }
        
        this.transform.localScale = Vector3.zero;
        count++;
        this.pannelli[count].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (this.transform.localScale != currentScale)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.currentScale, Time.deltaTime*speed);
        }
    }
    public void EndTutorial()
    {
        cameraController.enableButtons();
        this.currentScale = Vector3.zero;
        this.pannelli[count].gameObject.SetActive(false);
        backgroundCanvas.gameObject.SetActive(false);
        GridManager.instance.setTutorial(false);
    }
    
}
