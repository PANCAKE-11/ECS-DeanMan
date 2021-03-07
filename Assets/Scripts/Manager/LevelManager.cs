using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private bool isFading=false;
    [SerializeField] private CanvasGroup faderCanvasGroup = null;

    private void Start()
    {
        StartCoroutine(Fade(0));
    }
    private IEnumerator Fade(float finalAlpha ,float fadeDuration=1)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.unscaledDeltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
       
        // Call before scene unload fade out event
        EventHandler.CallBeforeSceneUnloadFadeOutEvent();

        // Start fading to black and wait for it to finish before continuing.
        yield return StartCoroutine(Fade(1f));

      

        //  Call before scene unload event.
        EventHandler.CallBeforeSceneUnloadEvent();

        // Unload the current active scene.

        // Start loading the given scene and wait for it to finish.
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        // Call after scene load event
        EventHandler.CallAfterSceneLoadEvent();


        // Call after scene load fade in event
        EventHandler.CallAfterSceneLoadFadeInEvent();
        if(sceneName!="MainMenu")
        { 
            SaveLoadManager.Instance.Load();
        }   
        yield return StartCoroutine(Fade(0f));

         UIManager.Instance.ClearPanels();


    }
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        // Allow the given scene to load over several frames and add it to the already loaded scenes (just the Persistent scene at this point).
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);


        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
    public void StartSwitchScene(string sceneName)
    {
        if(!isFading)
        StartCoroutine(FadeAndSwitchScenes( sceneName));
    }

}
