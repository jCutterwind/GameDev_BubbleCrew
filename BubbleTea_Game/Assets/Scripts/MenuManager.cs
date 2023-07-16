using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private MenuItem[] menu;
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private MenuItemDisplayer itemDisp;


    private void Start()
    {
        foreach(MenuItem item in menu)
        {
            MenuItemDisplayer disp = Instantiate(itemDisp);
            disp.setMenuItem(item);
            disp.transform.SetParent(menuPanel, false);
        }
    }
}
