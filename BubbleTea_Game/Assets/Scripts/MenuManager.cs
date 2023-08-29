using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private FullMenu fullMenu;
    private MenuItem[] currentMenu;
    private MenuItemDisplayer[] currentDisp;
    [SerializeField] private MenuChangeAnimation textAnim;

    //MenuDisplay
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private MenuItemDisplayer itemDisp;
    [SerializeField][Range(0.3f, 2.8f)]private float sizeMult = 1;

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
    public void newMenu()
    {
        clearMenu();
        textAnim.startZoomAnim();
        diff difficultySetting = GameManager.instance.DifficultySetting;
        MenuItem[] result = new MenuItem[6];
        for (int i = 0; i < 6; i++)
        {
            MenuItem item = new MenuItem();

            do
            {
                item = fullMenu.menu[Random.Range(0, fullMenu.menu.Length)];
                //Debug.Log("Trying to add " + item.ToString());
            //} /while (item.getDiff() > difficultySetting || returnDiffThresh(item.getDiff()) < Random.value || result.ToList<MenuItem>().Contains(item));
            } while (item.getDiff() > difficultySetting || result.ToList<MenuItem>().Contains(item));
            result[i] = item;
            //Debug.Log("Added ITEM to currMenu = " + item.ToString());
        }

        currentMenu = result;
        displayMenu(currentMenu);
    }

    /*
    private float returnDiffThresh(Ingredient ing)
    {
        switch (ing.difficulty)
        {
            case diff.EASY:
                return 1;
            case diff.MEDIUM:
                return GameManager.instance.MediumThresh;
            case diff.HARD:
                return GameManager.instance.HardThresh;
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
                return GameManager.instance.MediumThresh;
            case diff.HARD:
                return GameManager.instance.HardThresh;
            default:
                return 1;
        }
    }
    */

    public void displayMenu(MenuItem[] menu)
    {
        int length = menu.Length;
        currentDisp = new MenuItemDisplayer[length];

        for(int i =0;i<menu.Length;i++)
        {
            MenuItemDisplayer disp = Instantiate(itemDisp);
            disp.setMenuItem(menu[i]);
            disp.gameObject.transform.localScale *= sizeMult;
            disp.transform.SetParent(menuPanel, false);
            currentDisp[i] = disp;
        }
    }

    public MenuItem getRandomMenuItem()
    {
        MenuItem item = null;

        if(currentMenu != null)
        {
            do
            {
                item = currentMenu[Random.Range(0, currentMenu.Length)];
            }
            while (Random.value < (float) ((int) (item.getDiff()+1)/((int)GameManager.instance.DifficultySetting + 2)));
            //da implementare meglio. Come si fa con la difficoltà?
        }

        return item;
    }

    private void clearMenu()
    {
        if(currentMenu != null && currentDisp!= null)
        {
            for (int i = 0; i < currentMenu.Length; i++)
            {
                currentMenu[i] = null;
                Object.Destroy(currentDisp[i].gameObject);
                currentDisp[i] = null;
            }
        }
    }
}
