using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Skrypt generujacy losowa jednostke jako sprite ktora mozna dodac przez klikniecie
public class unitGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject rndUnit;
    [SerializeField]
    public GameObject testUnit;
    void Start()
    {
        testUnit = unitSpawner.unitTemplate;
        rndUnit = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Player);
        Unit _unit = rndUnit.GetComponent<Unit>();
        gameObject.transform.Find("unit_name").GetComponent<Text>().text=_unit.unitName;
        gameObject.transform.Find("unit_amount").GetComponent<Text>().text=_unit.getUnitAmount().ToString();
        gameObject.GetComponent<Image>().sprite = _unit.unitSprite;
    }

    public void OnMouseDown(){
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
        Destroy(gameObject);
    }
}
