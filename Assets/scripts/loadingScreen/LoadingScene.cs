using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private static LoadingScene instance;

    public static void LoadScene(int sceneIndex)
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("LoadingScene");
            instance = obj.AddComponent<LoadingScene>();
            DontDestroyOnLoad(obj);
        }

        instance.StartCoroutine(instance.AsyncLoadScene(sceneIndex));
    }

    private IEnumerator AsyncLoadScene(int index)
    {
        SceneManager.LoadScene("loading_screen");
        yield return new WaitForSeconds(0.01f);

        AsyncOperation loading = SceneManager.LoadSceneAsync(index);
        while (!loading.isDone)
        {
            Debug.Log($"Loading progress: {loading.progress}");
            yield return null;
        }
    }
}