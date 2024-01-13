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
        rndUnit = unitSpawner.spawnRandomUnitToGameObject();
        Unit _unit = rndUnit.GetComponent<Unit>();
        gameObject.transform.Find("unit_name").GetComponent<Text>().text=_unit.unitName;
        gameObject.transform.Find("unit_amount").GetComponent<Text>().text=_unit.getUnitAmount().ToString();
        gameObject.GetComponent<Image>().sprite = _unit.unitSprite;
    }

    public void OnMouseDown(){
        Unit _unit = rndUnit.GetComponent<Unit>();
        rndUnit.transform.SetParent(mainPlayer.Instance.transform);
        mainPlayerUnit.Instance.addUnitsToTeam(_unit);
        Destroy(gameObject);
    }
}
