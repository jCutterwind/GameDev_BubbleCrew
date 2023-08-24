using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private FullMenu fullMenu;
    private MenuItem[] currentMenu;

    //MenuDisplay
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private MenuItemDisplayer itemDisp;
    [SerializeField][Range(0.3f, 2.8f)]private float sizeMult = 1;

    public void newMenu(diff difficultySetting)
    {
        MenuItem[] result = new MenuItem[6];
        for (int i = 0; i < 6; i++)
        {
            MenuItem item = new MenuItem();

            do
            {
                item = fullMenu.menu[Random.Range(0, fullMenu.menu.Length)];
                //Debug.Log("Trying to add " + item.ToString());
            } while (item.getDiff() > difficultySetting || returnDiffThresh(item.getDiff()) < Random.value || result.ToList<MenuItem>().Contains(item));

            result[i] = item;
            //Debug.Log("Added ITEM to currMenu = " + item.ToString());
        }

        currentMenu = result;
        displayMenu(currentMenu);
    }

    private float returnDiffThresh(Ingredient ing)
    {
        switch (ing.difficulty)
        {
            case diff.EASY:
                return 1;
            case diff.MEDIUM:
                return GameManager.MediumThresh;
            case diff.HARD:
                return GameManager.HardThresh;
            default:
                return 1;
        }
    }

    private float returnDiffThresh(diff diff)
    {
        switch (diff)
        {
            case diff.EASY:
                return 1;
            case diff.MEDIUM:
                return GameManager.MediumThresh;
            case diff.HARD:
                return GameManager.HardThresh;
            default:
                return 1;
        }
    }

    public void displayMenu(MenuItem[] menu)
    {
        foreach(MenuItem item in menu)
        {
            MenuItemDisplayer disp = Instantiate(itemDisp);
            disp.setMenuItem(item);
            disp.gameObject.transform.localScale *= sizeMult;
            disp.transform.SetParent(menuPanel, false);
       
        }
    }

    public MenuItem getRandomMenuItem()
    {
        MenuItem item = null;

        if(this.currentMenu != null)
        {
            item = currentMenu[Random.Range(0, currentMenu.Length)];
            //da implementare meglio. Come si fa con la difficoltà?
        }

        return item;
    }


}
