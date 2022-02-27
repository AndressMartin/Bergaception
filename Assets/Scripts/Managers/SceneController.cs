using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    public static UnityEvent nextScene;

    [Header("Initialization")]
    static int previousScene;
    static int currentScene;
    static bool isDone = false;

    public VoidEvent sceneLoadingCompleted;

    //TODO: Save FTUE
    private void Awake()
    {
    }

    private void Start()
    {

    }

    IEnumerator LoadAsynchronously(int sceneIndex, LoadSceneMode mode, Action<AsyncOperation> action)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, mode);
        if (action != null && operation.progress == 0)
        {
            operation.completed += action;
            operation.completed += delegate { SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex)); };
            operation.completed += delegate { sceneLoadingCompleted.Raise(); Debug.Log("Loading scene completed" + SceneManager.GetSceneByBuildIndex(sceneIndex)); };
            Debug.Log("Begin loading " + SceneManager.GetSceneByBuildIndex(sceneIndex));
            
        }
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
    }

    public void UnloadSceneAsync(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public void LoadSceneAdditive(int sceneIndex)
    {
        Debug.Log("Load scene" + sceneIndex);
        StartCoroutine(LoadAsynchronously(sceneIndex, LoadSceneMode.Additive, null)); 
    }

    public void LoadSceneAdditiveAndSetActive(int sceneIndex)
    {
        Debug.Log("Load scene additively " + sceneIndex);
        StartCoroutine(LoadAsynchronously(sceneIndex, LoadSceneMode.Additive, delegate { 
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        }));
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
