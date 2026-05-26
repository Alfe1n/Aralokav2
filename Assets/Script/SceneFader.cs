using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;

    public float fadeDuration = 1f;

    void Start()
    {
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;
    }

    public IEnumerator FadeIn()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float alpha =
                Mathf.Lerp(1f, 0f,
                time / fadeDuration);

            Color c = fadeImage.color;
            c.a = alpha;

            fadeImage.color = c;

            yield return null;
        }

        Color finalColor =
            fadeImage.color;

        finalColor.a = 0f;

        fadeImage.color = finalColor;
    }

    public IEnumerator FadeOut()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float alpha =
                Mathf.Lerp(0f, 1f,
                time / fadeDuration);

            Color c = fadeImage.color;
            c.a = alpha;

            fadeImage.color = c;

            yield return null;
        }

        Color finalColor =
            fadeImage.color;

        finalColor.a = 1f;

        fadeImage.color = finalColor;
    }
}