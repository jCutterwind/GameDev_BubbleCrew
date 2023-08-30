using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CamPos
{
    LEFT, CENTER, RIGHT
}
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField] Button leftButton, rightButton;
    private Transform targetPos;
    private CamPos camPos;
    public CamPos CamPos => camPos;
    [SerializeField] Transform centerPos, leftPos, rightPos;

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
    void Start()
    {
        targetPos = centerPos;
        camPos = CamPos.CENTER;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position!=targetPos.position)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos.position, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    public void MoveRight()
    {
        if(camPos == CamPos.CENTER)
        {
            targetPos = leftPos;
            leftButton.interactable = true;
            rightButton.interactable = false;
            camPos = CamPos.RIGHT;
        } else if (camPos == CamPos.LEFT)
        {
            targetPos = centerPos;
            leftButton.interactable = true;
            rightButton.interactable = true;
            camPos = CamPos.CENTER;
        }

    }

    public void MoveLeft()
    {
        if (camPos == CamPos.CENTER)
        {
            targetPos = rightPos;
            leftButton.interactable = false;
            rightButton.interactable = true;
            camPos = CamPos.LEFT;
        }
        else if (camPos == CamPos.RIGHT)
        {
            targetPos = centerPos;
            leftButton.interactable = true;
            rightButton.interactable = true;
            camPos = CamPos.CENTER;
        }

    }




}
