using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoobyMapManager : MonoBehaviour
{
    private GameObject mainMap;
    public GameObject[] LVLButtons;
    private GameObject lastActivated;
    public int disabvlelvlfrom;
    public Sprite[] stars = new Sprite[4];
    void Awake()
    {
        if (PlayerPrefs.HasKey("ActivatedLVLS"))
        {
            disabvlelvlfrom = PlayerPrefs.GetInt("ActivatedLVLS");
        }
        else
        {
            disabvlelvlfrom = 2;
        }

        for (int i = disabvlelvlfrom; i < LVLButtons.Length; i++)
        {
            if (GameObject.Find($"LVL {i}") != null)
            {
                LVLButtons[i] = GameObject.Find($"LVL{i}");
                GameObject.Find($"LVL {i}").SetActive(false);
            }
        }
    }
}
    