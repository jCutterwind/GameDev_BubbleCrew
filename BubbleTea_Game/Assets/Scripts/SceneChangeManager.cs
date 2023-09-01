using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    private FMODController fmodCont;
    [SerializeField] LoadingScreen loadScreen;
    private bool isLoading = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        loadScreen.gameObject.SetActive(false);
    }
    private void Start()
    {
        FMODController.instance.setMenu(1);

    }

    public void startGame()
    {
        if(!isLoading)
        {
            isLoading = true;
            StartCoroutine(changeScene(1));
            FMODController.instance.setMenu(0);
        }
    }

    private IEnumerator changeScene(int i)
    {
        loadScreen.gameObject.SetActive(true);
        loadScreen.zoomInAnim();
        while (loadScreen.isAnim)
        {
            yield return null;
        }
        AsyncOperation async = SceneManager.LoadSceneAsync(i);
        while (!async.isDone)
        {
            Debug.Log("LOADING");
            yield return null;
        }

        loadScreen.zoomOutAnim();
        while (loadScreen.isAnim)
        {
            yield return null;
        }
        loadScreen.gameObject.SetActive(false);
        yield return null;

    }
}
