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
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("PositionOfMap"))
        {
            GameObject.Find("Map").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, PlayerPrefs.GetFloat("PositionOfMap"));
            print(PlayerPrefs.GetFloat("PositionOfMap"));
        }
    }
    private void Update()
    {

        if (GameObject.Find("Map").GetComponent<RectTransform>().anchoredPosition.y > 1335)
        {
            print("SADFSDFSDF");
            GameObject.Find("Map").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1334);

        }
    }
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
                GameObject.Find($"LVL {i}").SetActive(false);
            }
        }
    }
}
    