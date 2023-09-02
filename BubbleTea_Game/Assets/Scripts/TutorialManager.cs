using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private CameraController camera;
    [SerializeField] private Canvas[] pannelli;
    private int count = 0;
    private Vector3 currentScale;
    [SerializeField] private float speed=3;
    void Start()
    {
        foreach (Canvas canvas in pannelli)
        {
            canvas.gameObject.SetActive(false);
        }

        this.currentScale = this.transform.localScale;

    }

    public void Avanti()
    {
        this.pannelli[count].gameObject.SetActive(false);
        
        switch (count)
        {
            case 0:
                //camera.ReturnCenter();
                break;
            case 3:
                //camera.MoveLeft();
                break;
            case 4:
                //camera.MoveRight();
                break;
            case 5:
                //camera.ReturnCenter();
                break;
        }

        this.transform.localScale = Vector3.zero;
        count++;
        this.pannelli[count].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            this.pannelli[0].gameObject.SetActive(true);
        }

        if (this.transform.localScale != currentScale)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.currentScale, Time.deltaTime*speed);
        }
    }

    
}
