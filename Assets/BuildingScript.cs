using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour
{
    protected bool isbuilding;
   [Serialize] public Button Timer;
    public TMP_Text TimerText;
    protected DateTime TimeToComplete;
    public int multiplier;
    public sbyte whatLvlHaveBuilding;
    private void Start()
    {
        //jesli jest to nie znika i odlicza czas
        Timer.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey($"{this.name} Timer")){ 
            isbuilding=PlayerPrefs.GetInt($"{this.name} Timer")!=0;
        }
        else isbuilding=false;
        switch (this.name)
        {
            case "RatuszEntryButton":
                whatLvlHaveBuilding = GameObject.Find("CityManager").GetComponent<CityManager>().lvlRatusza; break;
        }

    }
    private void OnEnable()
    {
        //2 wczytanie jeabæ bo dzia³¹ wsumie i jak zmienisz scene to i tak enable odpali
    }
    private void OnDisable()
    {
        //zapisuje ile zosta³o do koñca i zapisuje
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isbuilding) return;
        //if jest to liczy
        //else zapisuje isbuilding na false i deaktywuje wszystko
        if (DateTime.Now <= TimeToComplete)
        {

        }
        else
        {
            
            isbuilding = false;
            PlayerPrefs.SetInt($"isBulding {this.gameObject.name}", (isbuilding ? 1 : 0));
            Timer.gameObject.SetActive(false);
        }
        
    }
    public void UpgradeStarts()
    {
        if (isbuilding == true) return;
        TimeToComplete = DateTime.Now.AddSeconds(whatLvlHaveBuilding*multiplier);
        isbuilding = true;
        Timer.gameObject.SetActive(true);

    }
}
