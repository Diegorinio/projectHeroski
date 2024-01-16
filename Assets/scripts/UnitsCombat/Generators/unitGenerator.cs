using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitGenerator : MonoBehaviour
{
    private GameObject rndUnit;
    void Start()
    {
        rndUnit = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Player);
        Unit _unit = rndUnit.GetComponent<Unit>();
        gameObject.transform.Find("unit_name").GetComponent<Text>().text=_unit.unitName;
        gameObject.transform.Find("unit_amount").GetComponent<Text>().text=_unit.getUnitAmount().ToString();
        gameObject.GetComponent<Image>().sprite = _unit.unitSprite;
    }

    public void OnMouseDown(){
        Unit _unit = rndUnit.GetComponent<Unit>();
        rndUnit.transform.SetParent(mainPlayerUnit.Instance.transform);
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
