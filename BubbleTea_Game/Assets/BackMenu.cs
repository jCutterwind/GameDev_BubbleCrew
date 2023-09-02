using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    [SerializeField] private string backMenu = "MainMenu";
    
    public void BackMenuButton()
    {

        SceneManager.LoadScene(backMenu);
    }
}
