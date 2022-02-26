using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SceneIndexes
{
    MANAGER = 0,
    TITLE_SCREEN = 1,
    LOADING_SCREEN_FTUE = 2,
    LOADING_SCREEN = 3,
    LEVEL_SELECTION = 4
}

public enum LevelIndexes
{
    level1 = 0,
    level2 = 1,
    level3 = 2,
    level4 = 3
}

public class SceneController : Singleton<SceneController>
{
    public static UnityEvent nextScene;

    [Header("Initialization")]
    //public MusicManage musicManager;
    //public PointManage pointManager;

    //public LoadingScreen loadingScreen;

    //static bool instantiateMusic = false;
    //public static bool paused = false;
    static int previousScene;
    static int currentScene;
    //static bool ftue = true;
    static bool isDone = false;

    public VoidEvent sceneLoadingCompleted;

    //TODO: Save FTUE
    private void Awake()
    {
        //if (nextScene == null)
        //    nextScene = new UnityEvent();
        //nextScene.AddListener(LoadNextScene);
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
            Debug.Log(progress);

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
        StartCoroutine(LoadAsynchronously(sceneIndex, LoadSceneMode.Additive, delegate { SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex)); }));
    }
}
