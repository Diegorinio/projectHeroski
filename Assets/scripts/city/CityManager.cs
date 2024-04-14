using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Threading;
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
    public int GOLDPERTIMER;
    public int IronPERTIMER;
    public int WoodPERTIMER;
    public int steelPERTIMER;
    public int xPERTIMER;
    public int ________________;
    //place holder for const script
    public int TimerToNextReward;
    //
    public resourcemanager resourcemanager;
    //surowce B, ze z budynk�w jeszcze nie w surowcemanager
    public int goldB;
    public int ironB;
    public int woodB;
    public int steelB;
    public int xB;
    //ratusz z�oto kopalnia iron
    protected GameObject GoldB_counter;
    protected GameObject IronB_counter;
    [SerializeField] protected GameObject WoodB_counter;
    [SerializeField] protected GameObject steelB_counter;
    [SerializeField] protected GameObject xB_counter;
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
    public sbyte lvlRatusza;
    public sbyte lvlkopalni;
    public sbyte lvlKoszar;
    //surowce jak nie grasz
    DateTime lastTimeIncity;

    private void Awake()
    {
        //COUNTERY DO BUDYNK�W
        GoldB_counter = GameObject.Find("GoldB_counter");
        //IronB_counter = GameObject.Find("IronB_counter");
        //
        BuildingButtons = GameObject.Find("CityButtonControl");
        CityBuildingPanelImg = GameObject.Find("CityBuildingPanelImg");
        animatorCmp = CityBuildingPanelImg.GetComponent<Animator>();
        //
        //wczytanie save
        goldB += PlayerPrefs.GetInt("GoldInBuilding");
        //ironB += PlayerPrefs.GetInt("IronInBuilding");
        //woodB += PlayerPrefs.GetInt("WoodInBuilding"); ;
        //steelB += PlayerPrefs.GetInt("SteelInBuilding"); ;
        //xB += PlayerPrefs.GetInt("XInBuilding");
        ChangeValuesInBuilding();
        //save lvl budynk�w
        if (!PlayerPrefs.HasKey("LvlRatusza")) lvlRatusza = 1;
        else lvlRatusza = Convert.ToSByte(PlayerPrefs.GetInt("LvlRatusza"));
        if(!PlayerPrefs.HasKey("LvlKopalni")) lvlkopalni =1;
        else
        {
            lvlkopalni = Convert.ToSByte(PlayerPrefs.GetInt("LvlKopalni"));
        }
        if (PlayerPrefs.HasKey("LvlKoszar"))lvlKoszar=Convert.ToSByte(PlayerPrefs.GetInt("LvlKoszar"));
        else lvlKoszar = 1;
        //jeśoi wiekszy niż 1 trzeba w osobbnej funkcji
        if (lvlRatusza >= 2) GameObject.Find("RatuszEntryButton").GetComponent<Image>().color = new Color32(155, 55, 190, 255);
        if (lvlkopalni >= 2) GameObject.Find("KopalniaEntryButton").GetComponent<Image>().color = new Color32(241, 255, 0, 255);
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
            goldB += GOLDPERTIMER*lvlRatusza;
            //ironB += IronPERTIMER*lvlkopalni;
            //woodB+=WoodPERTIMER*lvlkopalni;
            //if(lvlkopalni>=2) { steelB += steelPERTIMER * lvlkopalni; }
            //if(lvlkopalni>=3){ xB += xPERTIMER * lvlkopalni; }

            ChangeValuesInBuilding();
            timeRemaining = 5;
            savebuildingres();
        }
    }

    public void goToMainMap(){
        if(mainPlayerUnit.Instance.getUnits().Length>0 && mainPlayerUnit.Instance.getSelectedHero()!=null){
            SceneManager.LoadScene("mainMap");
        }
        else{
            gameMessagebox.createMessageBox("Units","Before going to adventure recruit units in BARRACKS and hire hero from TAVERN");
        }
    }
    private void OnEnable()
    {
        //wczytanie ile mineło czasu XD / przez ile na 1 staka
        lastTimeIncity = DateTime.Parse(PlayerPrefs.GetString("Last Time login Town"));
        Debug.Log($"Ostatni pobyt{lastTimeIncity}");
        TimeSpan howManySecPassed= DateTime.Now - lastTimeIncity;
        if ((int)howManySecPassed.TotalSeconds >= TimerToNextReward)
        {
            goldB += (GOLDPERTIMER * PlayerPrefs.GetInt("LvlRatusza")) *( (int)howManySecPassed.TotalSeconds % TimerToNextReward);
            //ironB += (IronPERTIMER * PlayerPrefs.GetInt("LvlKopalni")) *( (int)howManySecPassed.TotalSeconds % TimerToNextReward);
            //woodB+= (WoodPERTIMER*PlayerPrefs.GetInt("lvlKopalni"))* ((int)howManySecPassed.TotalSeconds % TimerToNextReward);
            //if (PlayerPrefs.GetInt("lvlKopalni") >= 2) {
            //    steelB += (steelPERTIMER * PlayerPrefs.GetInt("lvlKopalni")) * ((int)howManySecPassed.TotalSeconds % TimerToNextReward); 
            //    if(PlayerPrefs.GetInt("lvlKopalni") >= 3) 
            //            xB+= ( xPERTIMER * PlayerPrefs.GetInt("lvlKopalni")) * ((int)howManySecPassed.TotalSeconds % TimerToNextReward);
            //}

            ChangeValuesInBuilding();
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetString("Last Time login Town",DateTime.Now.ToString());
    }
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
                ChangeValuesInBuilding();
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
        if (GameObject.Find("StatShow")!=null) { GameObject.Find("StatShow").SetActive(false); print("XDDDDDDDD"); return; }
        animatorCmp.ResetTrigger("OpenPanel");
        animatorCmp.SetTrigger("ClosePanel");
        P1.SetActive(false);
        P2.SetActive(false);
        P3.SetActive(false);
        P4.SetActive(false);
        P5.SetActive(false);
    }
    public void savebuildingres()
    {
        PlayerPrefs.SetInt("GoldInBuilding", goldB);
        //PlayerPrefs.SetInt("IronInBuilding", ironB);
        //PlayerPrefs.SetInt("WoodInBuilding", woodB);
        //PlayerPrefs.SetInt("SteelInBuilding", steelB);
        //PlayerPrefs.SetInt("XInBuilding", xB);
    }
    void ChangeValuesInBuilding()
    {
        if (GoldB_counter!=null) GoldB_counter.GetComponent<TextMeshProUGUI>().SetText(goldB.ToString());
        //if (IronB_counter==null) return;
        //IronB_counter.GetComponent<TextMeshProUGUI>().SetText(ironB.ToString());
        //WoodB_counter.GetComponent<TextMeshProUGUI>().SetText("How much Wood: " + woodB.ToString());
        //steelB_counter.GetComponent<TextMeshProUGUI>().SetText("How much steel: " + steelB.ToString());
        //xB_counter.GetComponent<TextMeshProUGUI>().SetText("How much X: " + xB.ToString());

    }
    public void GoldColleted()
    {
        resourcemanager.gold += goldB;
        goldB = 0;
        ChangeValuesInBuilding();
        resourcemanager.CheckifChange();
    }
    //public void IronColleted()
    //{
    //    resourcemanager.iron += ironB;
    //    ironB = 0;
    //    ChangeValuesInBuilding();
    //    resourcemanager.CheckifChange();
    //}
    //public void collectedRes(string res)
    //{
    //    switch (res)
    //    {
    //        case "Wood":
    //            resourcemanager.wood += woodB;
    //            woodB = 0;
    //            ChangeValuesInBuilding();
    //            resourcemanager.CheckifChange();
    //            break;
    //        case "Steel":
    //            resourcemanager.steel += steelB;
    //            steelB = 0;
    //            ChangeValuesInBuilding();
    //            resourcemanager.CheckifChange();
    //            break;
    //        case "X":
    //            resourcemanager.X += xB;
    //            xB = 0;
    //            ChangeValuesInBuilding();
    //            resourcemanager.CheckifChange();
    //            break;
    //    }

    //}
    //public void UpgradeBuilding(string building)
    //{
    //    if (building == null) return;

    //    if (building == "Ratusz" && lvlRatusza < 5 && resourcemanager.gold > 2000 * lvlRatusza 
    //        && resourcemanager.iron > 3000 * lvlRatusza)

    //    {resourcemanager.gold -= 2000 * lvlRatusza; resourcemanager.iron -= 3000 * lvlRatusza;
    //        lvlRatusza += 1;
    //        PlayerPrefs.SetInt("LvlRatusza", lvlRatusza); resourcemanager.CheckifChange();
    //        GameObject.Find("RatuszEntryButton").GetComponent<BuildingScript>().UpgradeStarts();
    //    }

    //    if (building == "kopalnia" && lvlkopalni < 5 && resourcemanager.gold > 3000 * lvlkopalni 
    //        && resourcemanager.iron > 2000 * lvlkopalni) 

    //    { resourcemanager.gold -= 2000 * lvlkopalni; resourcemanager.iron -= 3000 * lvlkopalni; 
    //        lvlkopalni += 1;
    //        GameObject.Find("KopalniaEntryButton").GetComponent<BuildingScript>().UpgradeStarts();
    //        PlayerPrefs.SetInt("LvlKopalni", lvlkopalni); resourcemanager.CheckifChange();
    //    }
    //    //dla koszar
    //    if (building == "koszary" && lvlKoszar <5) //awrunki do zrobienia koszar
        
    //    { lvlKoszar += 1; PlayerPrefs.SetInt("LvlKoszar", lvlKoszar); GameObject.Find("KoszaryEntryButton").GetComponent<BuildingScript>().UpgradeStarts(); }
    //    if (lvlRatusza >= 2) GameObject.Find("RatuszEntryButton").GetComponent<Image>().color = new Color32(155, 55, 190, 255);
    //    if (lvlkopalni >= 2) GameObject.Find("KopalniaEntryButton").GetComponent<Image>().color = new Color32(155, 100, 75, 50);
    //    BackToCity();
    //}
}
