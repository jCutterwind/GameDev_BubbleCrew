using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveIngrediente : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 posizione_da_cambiare= Vector3.zero;
    [SerializeField] private float speed = 7;
    
    void Update()
    {
        if (posizione_da_cambiare!= Vector3.zero)
        {
            this.transform.position= Vector3.Lerp(this.transform.position, posizione_da_cambiare, Time.deltaTime*speed);
            if( this.transform.position== posizione_da_cambiare)
            {
                posizione_da_cambiare = Vector3.zero;
            }
        }
    }
    private void OnMouseDown()
    {
        isSelected = true;
        Debug.Log(this.name + " è stato selezionato!");
    }
    private void OnMouseUp()
    {
       
    }


    

    public void setPosizioneDaCambiare(Vector3 posizione_nuova) { posizione_da_cambiare = posizione_nuova; }
    public void setIsSelected() { isSelected = false; }
    public bool getIsSelected() { return isSelected; }
}