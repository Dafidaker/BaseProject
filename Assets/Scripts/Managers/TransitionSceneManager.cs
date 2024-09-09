using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SceneUtil = Utility.Scene.SceneUtil;

public class TransitionSceneManager : Singleton<TransitionSceneManager>
{
    [SerializeField] private CanvasGroup canvasGroup;
    private float _fadeDuration = 1f;
    private bool _isFading;
    private bool _isInitialized;
    
    public void FadeToScene(string sceneName,float fadeDuration = 1.0f)
    {
        ActivateDontDestroyOnLoad();
        
        _fadeDuration = fadeDuration;

        if (!_isInitialized)  Initialize(); 
        
        if (sceneName != null && !_isFading && SceneUtil.IsSceneLoadableFromPath(sceneName))
        {
            StartCoroutine(FadeOutIn(sceneName));
        }
    }
    
    public void FadeToNextScene(float fadeDuration = 1.0f)
    {
        _fadeDuration = fadeDuration;
        FadeToScene(SceneUtil.GetNextSceneName(),_fadeDuration);
    }

    private void Initialize()
    {
        _isInitialized = true;
        
        // Create the Canvas GameObject
        GameObject canvasGO = new GameObject("CustomCanvas");
        
        // Add a Canvas component
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Add a CanvasScaler component for proper UI scaling
        CanvasScaler canvasScaler = canvasGO.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        // Add a GraphicRaycaster component to detect UI interactions
        canvasGO.AddComponent<GraphicRaycaster>();

        // Make the Canvas a child of the current GameObject
        canvasGO.transform.SetParent(this.transform, false);

        // Create the Panel GameObject
        GameObject panelGO = new GameObject("CustomPanel");

        CanvasGroup tempCanvasGroup = panelGO.AddComponent<CanvasGroup>();
        tempCanvasGroup.alpha = 0;
        tempCanvasGroup.interactable = false;

        canvasGroup = tempCanvasGroup;

        // Add RectTransform and Image components to the Panel
        RectTransform rectTransform = panelGO.AddComponent<RectTransform>();
        Image image = panelGO.AddComponent<Image>();
        image.color = Color.black; // Set color for the panel

        // Set the size of the panel (e.g., 400x300)
        rectTransform.sizeDelta = new Vector2(1930, 1090);

        // Make the Panel a child of the Canvas
        panelGO.transform.SetParent(canvasGO.transform, false);
    }
    
    private IEnumerator FadeOutIn(string sceneName)
    {
        _isFading = true;
        yield return StartCoroutine(FadeOut());
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
        _isFading = false;
    }
    private IEnumerator FadeOut()
    {
        float duration = _fadeDuration / 2;
        var timer = 0f;
        canvasGroup.alpha = 0;
        
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = timer / duration;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
    private IEnumerator FadeIn()
    {
        float duration = _fadeDuration / 2;
        var timer = 0f;
        canvasGroup.alpha = 1f;
        
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1f - timer / duration;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}

