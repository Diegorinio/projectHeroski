using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartupLoading : MonoBehaviour
{
    string str = "isTutorialComplete";
    // Start is called before the first frame update
    void Awake(){
        Debug.Log($"Status tutorial: {PlayerPrefs.GetInt("isTutorialComplete")}");
        if(!PlayerPrefs.HasKey(str) || PlayerPrefs.GetInt(str)==0){
            PlayerPrefs.SetInt(str,0);
            SceneManager.LoadScene("tutorial");
        }
        else{
            SceneManager.LoadScene("city");
        }
    }
}
