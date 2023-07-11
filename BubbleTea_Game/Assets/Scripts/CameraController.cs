using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    private bool isLeft = true;
    [SerializeField] Button leftButton, rightButton;
    private Transform targetPos;
    [SerializeField] Transform leftPos, rightPos;


    private void OnMouseUpAsButton()
    {
        
    }

    void Start()
    {
        targetPos = leftPos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos.position, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    public void SwitchTarget(float pos)
    {
        switch(pos)
        {
            case 0:
                targetPos = leftPos;
                leftButton.interactable = false;
                rightButton.interactable = true;
                break;
            case 1:
                targetPos = rightPos;
                rightButton.interactable = false;
                leftButton.interactable = true;
                break;
            default:
                break;
        }
    }
    



}
