using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.8f;
    public bool fadeInOnStart = true;
    public bool fadeOutOnExit = true;
    public GameObject you;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3f);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        you.SetActive(true);
    }
    }