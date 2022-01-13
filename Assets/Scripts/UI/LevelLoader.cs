using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    static LevelLoader lvlloaderInstance;
    public GameObject loadingScreen;
    public Image loadingProgressBar;
    public TextMeshProUGUI progressText;
    public CanvasGroup loadingCanvas;
    public GameObject deadbols_screen, deadbols_bg, completed_screen, completed_bg;

    private void Awake()
    {
        if(LevelLoader.lvlloaderInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        lvlloaderInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        LeanTween.alphaCanvas(loadingCanvas, 1, 1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingProgressBar.fillAmount = progress;
            progressText.text = "Game Loading " + progress * 100f + "%";
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("done loading");
        LeanTween.alphaCanvas(loadingCanvas, 0, 1);
        loadingScreen.SetActive(false);
    }

    public void showLevelCompleteScreen()
    {
        LeanTween.scale(completed_screen, new Vector3(1, 1, 1), 0.1f);
        LeanTween.alphaCanvas(completed_bg.GetComponent<CanvasGroup>(), 1, 0.1f).setEase(LeanTweenType.linear);
        completed_bg.SetActive(true);
    }

    public void showDeadbolsScreen()
    {
        LeanTween.scale(deadbols_screen, new Vector3(1, 1, 1), 0.1f);
        LeanTween.alphaCanvas(deadbols_bg.GetComponent<CanvasGroup>(), 1, 0.1f).setEase(LeanTweenType.linear);
        deadbols_bg.SetActive(true);
    }
}
