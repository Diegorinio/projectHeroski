using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class goToSceneClick : MonoBehaviour
{
    [SerializeField]
    private int sceneID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single);
        Debug.Log($"go to scene {sceneID}");
    }
}
