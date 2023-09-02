using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    public void SceneChange(int i)
    {
        Debug.Log("SCENE CHANGE");
        SceneChangeManager.instance.changeScene(i);

    }
}
