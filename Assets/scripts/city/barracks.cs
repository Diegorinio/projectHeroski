using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barracks : MonoBehaviour
{
    [SerializeField]
    Slider amount_slider;
    [SerializeField]
    InputField amount_input;
    public Button buyBtn;
    public enum unitsTier{tier1,tier2,tier3}
    public unitsTier Tier;
    public unitSpawner.unitType unitType;
    // Start is called before the first frame update
    void Start()
    {
        // amount_input.onValueChanged.AddListener(delegate {changeSlider();});
        amount_slider.onValueChanged.AddListener(changeSlider);
        amount_input.onValueChanged.AddListener(changeInput);
        buyBtn.onClick.AddListener(buyUnit);
    }

    // Update is called once per frame
    void Update()
    {
        // amount_slider.value = amount_input.text
    }

    private void buyUnit(){
        Debug.Log("CHUUJJJ!J!J!J!J1");
        GameObject rndUnit = unitSpawner.spawnUnitGameObject(unitType,unitSpawner.controllers.Player,(int)amount_slider.value);
        Unit _unit = rndUnit.GetComponent<Unit>();
        rndUnit.transform.SetParent(mainPlayerUnit.Instance.transform);
        rndUnit.transform.localPosition = Vector3.zero;
        if(!mainPlayerUnit.Instance.isUnitExists(_unit)){
            rndUnit.transform.localPosition = Vector3.zero;
            mainPlayerUnit.Instance.addUnitsToTeam(_unit);
        }
        else{
            mainPlayerUnit.Instance.addUnitsToTeam(_unit);
            Destroy(rndUnit);
        }
    }
    private void changeSlider(float v ){
        int r = (int)v;
        amount_input.text = r.ToString();
    }

    private void changeInput(string s){
        int r = int.Parse(s);
        amount_slider.value = (float)r;
    }
}
