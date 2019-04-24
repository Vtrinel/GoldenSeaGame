using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition sceneTransition;
    public string nextScene = "Scene_Tutorial";

    [Header("Fade Animation")]
    public Animator animFade;
    public Image fadeImage;

    private void Awake()
    {
        sceneTransition = this;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,1);
    }

    private void Start()
    {
        animFade.SetTrigger("fadeOut");
    }

    [Button]
    public void LoadNextScene()
    {
        LoadScene(nextScene);
    }

    public void LoadScene(string _sceneName)
    {
        animFade.SetTrigger("fadeIn");
        StartCoroutine(LoadAsyncScene(_sceneName));
    }

    public void LoadScene(int _sceneIndex)
    {
        animFade.SetTrigger("fadeIn");
        StartCoroutine(LoadAsyncScene(_sceneIndex));
    }

    IEnumerator LoadAsyncScene(string _sceneName)
    {
        yield return new WaitUntil(IsOpaque);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadAsyncScene(int _sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    bool IsOpaque()
    {
        if(fadeImage.color.a < 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
