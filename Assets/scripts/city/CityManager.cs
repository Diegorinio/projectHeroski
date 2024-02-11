using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
//mini dukomentacja
/*
 ControlerCitypanel s�u�y do wyswietlenia paneli czyli tam b�da opcje co dany budynek robi
 ma back bttn do wracania
 Tokow� tawerne umie�ci�em pod koszary i zmienie to na ko�cu
City manager b�dzie zajmowa� sie skryptami tych paneli i wysy�a� sur�wce do recource managera
kt�re b�dzie trzyma� w sobie
 */
public class CityManager : MonoBehaviour
{
    public resourcemanager resourcemanager;
    //surowce B, ze z budynk�w jeszcze nie w surowcemanager
    public int goldB;
    public int ironB;
    //ratusz z�oto kopalnia iron
    protected GameObject GoldB_counter;
    protected GameObject IronB_counter;
    //obiekty itp
    protected GameObject BuildingButtons;
    protected Animator animatorCmp;
    protected GameObject CityBuildingPanelImg;

    //interfejsy do panelu kp to kopalnia panel itp
    protected GameObject P1;//kopalnia arena tawerna ratusz itp
    protected GameObject P2;
    protected GameObject P3;
    protected GameObject P4;
    protected GameObject P5;
    //protected GameObject[] CityPanels =new GameObject[5]; w przysz�osci wsadzi si� do array
    //lvl budynk�w
    sbyte lvlRatusza;
    sbyte lvlkopalni;
    //stan rekrutacji

    private void Start()
    {   lvlRatusza = 1;
        lvlkopalni = 1;
        //COUNTERY DO BUDYNK�W
        GoldB_counter = GameObject.Find("GoldB_counter");
        IronB_counter = GameObject.Find("IronB_counter");
        //
        BuildingButtons = GameObject.Find("CityButtonControl");
        CityBuildingPanelImg = GameObject.Find("CityBuildingPanelImg");
        animatorCmp = CityBuildingPanelImg.GetComponent<Animator>();
        //
        //wczytanie save
        goldB = PlayerPrefs.GetInt("GoldInBuilding");
        ironB = PlayerPrefs.GetInt("IronInBuilding");
        ChangeGoldInBuilding();
        ChangeIronInBuilding();
        //save lvl budynk�w
        if( Convert.ToSByte(PlayerPrefs.GetInt("LvlRatusza"))==0) lvlRatusza = 1;
        else lvlRatusza = Convert.ToSByte(PlayerPrefs.GetInt("LvlRatusza"));
        if(Convert.ToSByte(PlayerPrefs.GetInt("LvlKopalni"))==0) lvlkopalni =1;
        else
        {
            lvlkopalni = Convert.ToSByte(PlayerPrefs.GetInt("LvlKopalni"));
        }
        if (lvlRatusza > 2) GameObject.Find("RatuszEntryButton").GetComponent<Image>().color = new Color32(155, 55, 190, 255);
        if (lvlkopalni > 2) GameObject.Find("KopalniaEntryButton").GetComponent<Image>().color = new Color32(155, 100, 75, 255);
        //
        P1 = GameObject.Find("RatuszPanel");
        P2 = GameObject.Find("KopalniaPanel");
        P3=GameObject.Find("ArenaPanel");
        P4=GameObject.Find("TavernPanel");
        P5=GameObject.Find("Koszary");
        //zrobi� dla koszar czyli tomkowej tawerny
        P1.SetActive(false);
        P2.SetActive(false);
        P3.SetActive(false);
        P4.SetActive(false);
        P5.SetActive(false);
    }
    //60 sec
    public float timeRemaining = 5;
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            goldB += 500*lvlRatusza;
            ironB += 300*lvlkopalni;
            ChangeGoldInBuilding();
            ChangeIronInBuilding();
            timeRemaining = 5;
            PlayerPrefs.SetInt("GoldInBuilding",goldB);
            PlayerPrefs.SetInt("IronInBuilding", ironB);
        }
    }

    public void goToMainMap(){
        if(mainPlayerUnit.Instance.getUnits().Length>0){
            SceneManager.LoadScene("mainMap");
        }
        else{
            gameMessagebox.createMessageBox("Units","Przed wyruszeniem w droge zbierz druzyne");
        }
    }

    // private void chuj(){
        // Debug.Log("Test buttona");
    // }


    public void ActivateBuilding(string buttonBuildingName)
    {
        animatorCmp.SetTrigger("OpenPanel");

        switch (buttonBuildingName)
        {
            case "ratusz":
                P1.SetActive(true);
                //znajduje i zmienia walrtosc w budynku by nie czeka� na zegar by da� hajs
                GoldB_counter.GetComponent<TextMeshProUGUI>().SetText(goldB.ToString());
                break;
            case "kopalnia":
                P2.SetActive(true);
                //IronB_counter = GameObject.Find("IronB_counter");
                IronB_counter.GetComponent<TextMeshProUGUI>().SetText(ironB.ToString());
                break;
            case "arena":
                P3.SetActive(true);
                break;
            case "tawerna":
                P4.SetActive(true);
                break;
            case "barracks":
                P5.SetActive(true);
                break;
        }


    }
    public void BackToCity()
    {
        animatorCmp.ResetTrigger("OpenPanel");
        animatorCmp.SetTrigger("ClosePanel");
        P1.SetActive(false);
        P2.SetActive(false);
        P3.SetActive(false);
        P4.SetActive(false);
        P5.SetActive(false);
    }
    public void ChangeGoldInBuilding()
    {
        if(!GoldB_counter.activeInHierarchy) return;
        GoldB_counter.GetComponent<TextMeshProUGUI>().SetText(goldB.ToString());
    }
    public void ChangeIronInBuilding()
    {
        if (!IronB_counter.activeInHierarchy) return;
        IronB_counter.GetComponent<TextMeshProUGUI>().SetText(ironB.ToString());
    }
    public void GoldColleted()
    {
        resourcemanager.gold += goldB;
        goldB = 0;
        ChangeGoldInBuilding();
        resourcemanager.CheckifChange();
    }
    public void IronColleted()
    {
        resourcemanager.iron += ironB;
        ironB = 0;
        ChangeIronInBuilding();
        resourcemanager.CheckifChange();
    }
    public void UpgradeBuilding(string building)
    {
        if (building == null) return;
        if (building == "Ratusz" && lvlRatusza < 5 && resourcemanager.gold > 2000 * lvlRatusza && resourcemanager.iron > 3000 * lvlRatusza) {resourcemanager.gold -= 2000 * lvlRatusza; resourcemanager.iron -= 3000 * lvlRatusza;
            lvlRatusza += 1;
            PlayerPrefs.SetInt("LvlRatusza", lvlRatusza); resourcemanager.CheckifChange(); }
        if (building == "kopalnia" && lvlkopalni < 5 && resourcemanager.gold > 3000 * lvlkopalni && resourcemanager.iron > 2000 * lvlkopalni) { resourcemanager.gold -= 2000 * lvlkopalni; resourcemanager.iron -= 3000 * lvlRatusza; lvlkopalni += 1;
            lvlkopalni += 1;
            PlayerPrefs.SetInt("LvlKopalni", lvlkopalni); resourcemanager.CheckifChange(); }
        if (lvlRatusza > 2) GameObject.Find("RatuszEntryButton").GetComponent<Image>().color = new Color32(155, 55, 190, 255);
        if (lvlkopalni > 2) GameObject.Find("RatuszEntryButton").GetComponent<Image>().color = new Color32(155, 100, 75, 50);
    }
}
