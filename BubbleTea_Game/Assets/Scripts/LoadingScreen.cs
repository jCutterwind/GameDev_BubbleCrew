using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private float animSpeed;
    private Vector3 startScale;
    public static LoadingScreen instance;
    public bool isAnim = false;
    [SerializeField] private GameObject scalingObject;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        } else if (instance!=this)
        {
            Destroy(gameObject);
        }
        startScale = scalingObject.transform.localScale;
        scalingObject.transform.localScale = Vector3.zero;
        DontDestroyOnLoad(gameObject);
    }
    private IEnumerator zoomIn()
    {
        isAnim = true;
        float timer = 0;
        while (timer <= 1)
        {
            Debug.Log("ZOOM IN " + timer + ", startScale = " + startScale + ", localScale = " + transform.localScale);
            timer += Time.deltaTime * animSpeed;
            scalingObject.transform.localScale = Vector3.Lerp(scalingObject.transform.localScale, startScale, timer);
            yield return null;
        }
        //for(float timer =0;timer<1;timer+=Time.deltaTime*animSpeed)
        //{

        //}
        isAnim = false;
        yield return null;
    }

    private IEnumerator zoomOut()
    {
        isAnim = true;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * animSpeed;
            scalingObject.transform.localScale = Vector3.Lerp(scalingObject.transform.localScale, Vector3.zero, timer);
            yield return null;
        }
        isAnim = false;
        yield return null;
    }

    public void zoomInAnim()
    {
        StartCoroutine(zoomIn());
    }

    public void zoomOutAnim()
    {
        StartCoroutine(zoomOut());
    }
}
