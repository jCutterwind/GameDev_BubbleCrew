using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    public static SceneChangeManager instance;
    private FMODController fmodCont;
    [SerializeField] LoadingScreen loadScreen;
    private bool isLoading = false;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

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
            StartCoroutine(changeSceneCoroutine(1));
            FMODController.instance.setMenu(0);
        }
    }

    public void changeScene(int i)
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(changeSceneCoroutine(i));
        }
    }

    private IEnumerator changeSceneCoroutine(int i)
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
        isLoading = false;
        yield return null;

    }
}
