using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour
{
    public bool isbuilding;
   [Serialize] public Button Timer;
    public TMP_Text TimerText;
    protected DateTime TimeToComplete;
    public int multiplier;
    public sbyte whatLvlHaveBuilding;
    private void Start()
    {
        //jesli jest to nie znika i odlicza czas
        Timer.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey($"isBulding {this.name}")){ 
            isbuilding=(PlayerPrefs.GetInt($"isBulding {this.name}") !=0);
        }
        else isbuilding=false;
        switch (this.name)
        {
            case "RatuszEntryButton":
                whatLvlHaveBuilding = GameObject.Find("CityManager").GetComponent<CityManager>().lvlRatusza; break;
            case "KopalniaEntryButton":
                whatLvlHaveBuilding = GameObject.Find("CityManager").GetComponent<CityManager>().lvlkopalni; break;
           case "KoszaryEntryButton":
              whatLvlHaveBuilding = GameObject.Find("CityManager").GetComponent<CityManager>().lvlKoszar; break;
        }

    }
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey($"isBulding {this.name}"))
        {
            isbuilding =( PlayerPrefs.GetInt($"isBulding {this.name}") != 0);
        }
        
        if(PlayerPrefs.HasKey($"time to complete {this.name} Building"))
        TimeToComplete=DateTime.Parse( PlayerPrefs.GetString($"time to complete {this.name} Building"));
    }
    private void OnDisable()
    {
        PlayerPrefs.SetString($"time to complete {this.name} Building", TimeToComplete.ToString());
        PlayerPrefs.SetInt($"isBulding {this.name}", (isbuilding ? 1 : 0));


    }
    void FixedUpdate()
    {
        // print(isbuilding);
        if (!isbuilding) return;
        if (DateTime.Now <= TimeToComplete)
        {
            Timer.gameObject.SetActive (true);
            Timer.gameObject.GetComponent<Button>().interactable = false;
            this.gameObject.GetComponent<Button>().interactable = false;
            TimeSpan TimeLeft=TimeToComplete-DateTime.Now;
            TimerText.SetText("Time to Complete: "+((int)TimeLeft.TotalSeconds).ToString());

        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            isbuilding = false;
            PlayerPrefs.SetInt($"isBulding {this.name}", (isbuilding ? 1 : 0));
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
