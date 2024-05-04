using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class barracks : MonoBehaviour
{
    private resourcemanager manager;
    [SerializeField]
    private resourcemanager resource;

    public int goldPerUnit;
    [SerializeField]
    Slider amount_slider;
    [SerializeField]
    InputField amount_input;
    public Button buyBtn;

    public GameObject collectBtn;

    private unitSpawner.tier unitTier;
    private unitSpawner.unitType unitType;
    //recruiment
    private string unitName;
    private int lastRecruitSoldiers;
    private bool isRecrutable;
    private bool isFirstTime;

    public TMP_Text _complitionTime;
    
    protected DateTime UnitToReadyTime;
    protected DateTime rightNowTime;

    //Do cheat buttona, !pamietac wywalic na release
    public Button cheatBtn;

    void Start()
    {
        unitSetUpBarracks unitSettings = gameObject.GetComponent<unitSetUpBarracks>();
        unitTier = unitSettings.getUnitTier();
        unitType = unitSettings.getUnitType();
        //cheat button
        cheatBtn.onClick.AddListener(cheat);
        //
        amount_slider.onValueChanged.AddListener(changeSlider);
        amount_input.onValueChanged.AddListener(changeInput);
        amount_input.text="1";
        buyBtn.onClick.AddListener(buyUnit);
        // buyBtn.onClick.AddListener(buyUnitTier);
        collectBtn.GetComponent<Button>().onClick.AddListener(recruitUnitTier);
        if(isRecrutable)isRecrutable = true;
        if((PlayerPrefs.GetInt($"isFirstTime {unitName}") != 0)==false) isFirstTime = false;
        else isFirstTime = true;
    }
    private void Awake()
    {
        unitName = this.gameObject.name;

    }
    private void FixedUpdate()
    {
        if (UnitToReadyTime >= DateTime.Now && !isRecrutable)
        {
            amount_slider.interactable = false;
            amount_input.interactable = false;
            buyBtn.interactable = false;
            TimeSpan ts = UnitToReadyTime - DateTime.Now;
            _complitionTime.SetText($"Time left: {(int)ts.TotalSeconds}");

        }
        else
        {
            if (isRecrutable || !isFirstTime) return;
        
            buyBtn.interactable = true;
            if(!isRecrutable&& isFirstTime) { collectBtn.SetActive(true); 
            }
                
        }
    }

    private void OnEnable()
    {
        //maks slider value
        amount_slider.maxValue = resource.gold / goldPerUnit;



        manager=GameObject.Find("resourceManager").GetComponent<resourcemanager>();
        UnitToReadyTime = DateTime.Parse(PlayerPrefs.GetString($"{unitName}: recruitment time"));
        lastRecruitSoldiers = PlayerPrefs.GetInt($"{unitName}: amount");
        isRecrutable= (PlayerPrefs.GetInt($"isRecrutable {unitName}") != 0);
        if (!isRecrutable)
        {
            collectBtn.SetActive(false);
            buyBtn.gameObject.SetActive(true);
        }
        else
        {
            buyBtn.gameObject.SetActive(true);
            collectBtn.SetActive(false);
        }

    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt($"isRecrutable {unitName}", (isRecrutable ? 1 : 0));
        PlayerPrefs.SetString($"{unitName}: recruitment time",UnitToReadyTime.ToString());
        PlayerPrefs.SetInt($"{unitName}: amount",lastRecruitSoldiers);
        PlayerPrefs.SetInt($"isFirstTime {unitName}", (isFirstTime ? 1 : 0));
    }

    private void buyUnit(){
        if (amount_slider.value <= 0) return;
        if (manager.gold < (int)amount_slider.value*goldPerUnit) {
            //wyœwietl nie masz z³ota biedaku
            return;
        }
        DateTime UnitBoughtTime = DateTime.Now;
        if((int)amount_slider.value>0){
        UnitToReadyTime = UnitBoughtTime.AddSeconds(5);
        int goldToBuy= (int)amount_slider.value*goldPerUnit;
        manager.gold-=goldToBuy;
        manager.CheckifChange();
                //UnitBoughtTime.AddSeconds((int)amount_slider.value*2);
        lastRecruitSoldiers = (int)amount_slider.value;
        isRecrutable = false;
        isFirstTime = true;
        buyBtn.gameObject.SetActive(false);
        }
    }

    private void recruitUnitTier(){
        if((int)amount_slider.value>0){
        Debug.Log("Kupienie jednostek CHCHCHCHHCHCUI!!!1");
        GameObject rndUnit = unitSpawner.spawnUnitGameObject(unitTier,unitType,unitSpawner.controllers.Player,(int)amount_slider.value);
        Unit _unit = rndUnit.GetComponent<Unit>();
        rndUnit.transform.SetParent(mainPlayerUnit.Instance.transform);
        rndUnit.transform.localPosition = Vector3.zero;
        if (!mainPlayerUnit.Instance.isUnitExists(_unit))
        {
            rndUnit.transform.localPosition = Vector3.zero;
            mainPlayerUnit.Instance.addUnitsToTeam(_unit);
        }
        else
        {
            mainPlayerUnit.Instance.addUnitsToTeam(_unit);
            Destroy(rndUnit);
        }

        isRecrutable=true;
        amount_slider.interactable = true;
        amount_input.interactable = true;
        buyBtn.gameObject.SetActive(true);
        collectBtn.gameObject.SetActive(false);
        }
    }

    // private void RecruitUnit()
    // {
    //     GameObject rndUnit = unitSpawner.spawnUnitGameObject(unitType, unitSpawner.controllers.Player, lastRecruitSoldiers);
    //     Unit _unit = rndUnit.GetComponent<Unit>();
    //     rndUnit.transform.SetParent(mainPlayerUnit.Instance.transform);
    //     rndUnit.transform.localPosition = Vector3.zero;
    //     if (!mainPlayerUnit.Instance.isUnitExists(_unit))
    //     {
    //         rndUnit.transform.localPosition = Vector3.zero;
    //         mainPlayerUnit.Instance.addUnitsToTeam(_unit);
    //     }
    //     else
    //     {
    //         mainPlayerUnit.Instance.addUnitsToTeam(_unit);
    //         Destroy(rndUnit);
    //     }
    //     isRecrutable=true;
    //     amount_slider.interactable = true;
    //     amount_input.interactable = true;
    //     buyBtn.gameObject.SetActive(true);
    //     collectBtn.gameObject.SetActive(false);
    // }
    private void changeSlider(float v ){
        int r = (int)v;
        amount_input.text = r.ToString();
    }

    private void changeInput(string s){
        int r = int.Parse(s);
        amount_slider.value = (float)r;
    }
    public void cheat()
    {
        UnitToReadyTime=DateTime.Now;
    }
}
