using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class goToSceneClick : MonoBehaviour
{
    [SerializeField]
    private string sceneID;
    public void OnMouseDown()
    {
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
        Debug.Log($"go to scene {sceneID}");
    }
}
