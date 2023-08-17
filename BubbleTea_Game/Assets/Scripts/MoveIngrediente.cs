using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveIngrediente : MonoBehaviour
{
    public int colonna;
    public int riga;
    public int TargetX;
    public int TargetY;
    private creazioneSfondo board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle = 0;
    void Start ()
    {
        board = FindObjectOfType<creazioneSfondo>();
        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;

    }
    void Update()
    {
        TargetX = colonna;
        TargetY = riga;
    }
    private void OnMouseDown()
    {
        firstTouchPosition = Input.mousePosition;
        //Debug.Log(firstTouchPosition);
    }
    private void OnMouseUp()
    {
        finalTouchPosition = Input.mousePosition;
        CalculateAngle();
    }
    void CalculateAngle() {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        MovePieces();
    }
    void MovePieces()
    {
        if(swipeAngle > -45 && swipeAngle <= 45)
        {
            //swipe a destra
            mat= board.mat[colonna + 1,riga];
            mat.GetComponent<mat>().colonna -=1;
            colonna += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135)
        {
            //swipe up
            mat = board.mat[colonna , riga+1];
            mat.GetComponent<mat>().riga -= 1;
            riga += 1;
        }
        else if (swipeAngle > 135 && swipeAngle <= -135)
        {
            //swipe a sinistra
            mat = board.mat[colonna - 1, riga];
            mat.GetComponent<mat>().colonna += 1;
            colonna -= 1;
        }
        else if (swipeAngle > -45 && swipeAngle <= -135)
        {
            //swipe down
            mat = board.mat[colonna , riga-1];
            mat.GetComponent<mat>().riga += 1;
            riga -= 1;
        }
    }
}