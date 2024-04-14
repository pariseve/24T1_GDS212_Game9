using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private float fadeDuration = 1f;


    private void Start()
    {
        // Ensure the fade panel is inactive at the start
        fadePanel.SetActive(false);
    }

    public void GoToScene()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        // Activate the fade panel
        fadePanel.SetActive(true);

        // Fade in effect
        float elapsedTime = 0f;
        Color fadeColor = fadePanel.GetComponent<UnityEngine.UI.Image>().color;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeColor.a = alpha;
            fadePanel.GetComponent<UnityEngine.UI.Image>().color = fadeColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade out effect
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeColor.a = alpha;
            fadePanel.GetComponent<UnityEngine.UI.Image>().color = fadeColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deactivate the fade panel
        fadePanel.SetActive(false);
    }

}