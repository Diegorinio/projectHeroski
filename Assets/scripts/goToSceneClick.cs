using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class goToSceneClick : MonoBehaviour
{

    //Glownie do koliderow bo jestem lewniwy
    [SerializeField]
    private string sceneID;
    //Zaladuj scene
    public void OnMouseDown()
    {
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
        Debug.Log($"go to scene {sceneID}");
    }
}
