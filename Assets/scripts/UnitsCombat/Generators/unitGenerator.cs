using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Głównie do tawerny albo jakiego ewnetu czy cos gdzie mozesz dodaj jednostki
//Skrypt generujacy losowa jednostke jako sprite ktora mozna dodac przez klikniecie
public class unitGenerator : MonoBehaviour
{
    //GameObject ktory bedzie templataka
    [SerializeField]
    private GameObject rndUnit;

    //Na starcie generuje jednostke z unitSpawner jako GameOBject typu Player
    //Na komponenty UI dodaj tekst na nazwe jednostki i ilosc jednostki
    //Zmien Sprite na dany sprite klasy 
    void Start()
    {
        rndUnit = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Player);
        Unit _unit = rndUnit.GetComponent<Unit>();
        gameObject.transform.Find("unit_name").GetComponent<Text>().text=_unit.unitName;
        gameObject.transform.Find("unit_amount").GetComponent<Text>().text=_unit.getUnitAmount().ToString();
        gameObject.GetComponent<Image>().sprite = _unit.unitSprite;
    }

    // Po klienieciu myszka dodaj do mainPlayer jednostke
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
