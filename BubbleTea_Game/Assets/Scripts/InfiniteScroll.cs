using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private Transform firstAnchor;
    [SerializeField] private Transform lastAnchor;
    [SerializeField] private float speed;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime * speed;
        this.transform.position = Vector3.Lerp(lastAnchor.position, firstAnchor.position, timer);
        //Debug.Log(Vector3.Distance(this.transform.position, firstAnchor.transform.position));
        if (timer >= 1)
        {
            timer = 0;
        }
    }
}
