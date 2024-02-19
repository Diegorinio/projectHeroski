using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class barracks : MonoBehaviour
{
    [SerializeField]
    Slider amount_slider;
    [SerializeField]
    InputField amount_input;
    public Button buyBtn;

    public GameObject collectBtn;

    public enum unitsTier{tier1,tier2,tier3}
    public unitsTier Tier;
    public unitSpawner.unitType unitType;
    //recruiment
    private string unitName;
    private int lastRecruitSoldiers;
    private bool isRecrutable;
    private bool isFirstTime;

    public TMP_Text _complitionTime;
    
    protected DateTime UnitToReadyTime;
    protected DateTime rightNowTime;

    void Start()
    {
        amount_slider.onValueChanged.AddListener(changeSlider);
        amount_input.onValueChanged.AddListener(changeInput);
        buyBtn.onClick.AddListener(buyUnit);
        // buyBtn.onClick.AddListener(buyUnitTier);
        collectBtn.GetComponent<Button>().onClick.AddListener(buyUnitTier);
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
        UnitToReadyTime = DateTime.Parse(PlayerPrefs.GetString($"{unitName}: recruitment time"));
        lastRecruitSoldiers = PlayerPrefs.GetInt($"{unitName}: amount");
        isRecrutable= (PlayerPrefs.GetInt($"isRecrutable {unitName}") != 0);
        if (!isRecrutable)
        collectBtn.SetActive(false);
        else buyBtn.gameObject.SetActive(true);

    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt($"isRecrutable {unitName}", (isRecrutable ? 1 : 0));
        PlayerPrefs.SetString($"{unitName}: recruitment time",UnitToReadyTime.ToString());
        PlayerPrefs.SetInt($"{unitName}: amount",lastRecruitSoldiers);
        PlayerPrefs.SetInt($"isFirstTime {unitName}", (isFirstTime ? 1 : 0));
    }

    private void buyUnit(){

        DateTime UnitBoughtTime = DateTime.Now;

        UnitToReadyTime = UnitBoughtTime.AddSeconds((int)amount_slider.value*2);
        lastRecruitSoldiers = (int)amount_slider.value;
        isRecrutable = false;
        isFirstTime = true;
        buyBtn.gameObject.SetActive(false);
        
    }

    private void buyUnitTier(){
        Debug.Log("Kupienie jednostek CHCHCHCHHCHCUI!!!1");
        GameObject rndUnit = unitSpawner.spawnUnitGameObject(unitSpawner.tier.T1,unitType,unitSpawner.controllers.Player,(int)amount_slider.value);
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

    private void RecruitUnit()
    {
        GameObject rndUnit = unitSpawner.spawnUnitGameObject(unitType, unitSpawner.controllers.Player, lastRecruitSoldiers);
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
