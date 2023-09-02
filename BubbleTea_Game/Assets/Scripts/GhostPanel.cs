using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image[] images;
    private Vector3 endPos;
    private Vector3 startPos;
    [SerializeField] private float offset;
    [SerializeField] private float speed;

    private void Start()
    {
        startPos = this.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + offset, startPos.z);
        gameObject.SetActive(false);
    }

    public void GoGhost()
    {
        StartCoroutine(ghostAnim());
    }
    private IEnumerator ghostAnim()
    {
        float timer = 0;
        while (timer < 1)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, timer);
            foreach(Image image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f * (1 - timer));
            }
            timer += Time.deltaTime * speed;
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
