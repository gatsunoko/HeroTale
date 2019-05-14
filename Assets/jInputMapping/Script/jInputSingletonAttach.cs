using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class jInputSingletonAttach : jInputSingleton<jInputSingletonAttach>
{
    public void Awake()
    {
        if (this != Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        #if (UNITY_5_4_OR_NEWER)
            SceneManager.sceneLoaded += OnSceneLoaded;
        #endif

        DontDestroyOnLoad(this.gameObject);
    }

    #if (UNITY_5_4_OR_NEWER)
    void OnSceneLoaded(Scene scene, LoadSceneMode m)
    {
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
    }

#else
    void OnLevelWasLoaded()
    {
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
    }
    #endif
}
