using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    public void FadeToScene(string sceneName)
    {
        Debug.Log("Fader called for scene: " + sceneName);
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        canvasGroup.blocksRaycasts = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = t / fadeDuration;
            Debug.Log("Fading alpha = " + canvasGroup.alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
