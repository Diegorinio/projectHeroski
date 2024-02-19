using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class goToSceneClick : MonoBehaviour
{

    //Glownie do koliderow bo jestem lewniwy
    [SerializeField]
    private string sceneID;
    //Zaladuj scene
    public async void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "Menu") await Task.Delay(300);
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
        Debug.Log($"go to scene {sceneID}");
    }
}
