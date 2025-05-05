using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTitleUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text levelTitleText;
    public float fadeDuration = 1.5f;
    public float displayDuration = 2.5f;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        levelTitleText.text = GetFormattedTitle(sceneName);
        StartCoroutine(ShowLevelTitle());
    }

    string GetFormattedTitle(string rawName)
    {
        switch (rawName)
        {
            case "Level1": return "Level 1: Tutorial";
            case "Level 2": return "Level 2: Prison Basement";
            case "Level 3": return "Level 3: Sewers";
            case "Level 4": return "Level 4: Alchemists' Lab";
            default: return rawName;
        }
    }

    IEnumerator ShowLevelTitle()
    {
        yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeCanvas(1f, 0f, fadeDuration));
        gameObject.SetActive(false);
    }

    IEnumerator FadeCanvas(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
