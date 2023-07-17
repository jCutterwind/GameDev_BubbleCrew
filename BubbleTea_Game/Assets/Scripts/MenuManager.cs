using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private MenuItem[] menu;
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private MenuItemDisplayer itemDisp;

    [SerializeField][Range(0.3f, 2.8f)]private float sizeMult = 1;


    public void setMenu(MenuItem[] menu)
    {
        foreach(MenuItem item in menu)
        {
            MenuItemDisplayer disp = Instantiate(itemDisp);
            disp.setMenuItem(item);
            disp.gameObject.transform.localScale *= sizeMult;
            disp.transform.SetParent(menuPanel, false);
       
        }
    }
}
