using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Skrypt odpowiedzialny za wyswietlenie jednostek na panelu city
// TODO: ZMIENIć nazwe klasy
// Zmiana lokalizacji panelu
public class guiScript : MonoBehaviour
{
    Text unitsList;
    List<Unit> unitlist;

    public void Start(){
        unitsList = gameObject.transform.Find("units_list").GetComponent<Text>();
        unitsList.text= "XD";
        // showUnits();
        unitlist = mainPlayerUnit.Instance.getUnitsList();
    }
    public void showUnits(){
        string tekst ="";
        foreach(var u in unitlist){
            tekst+=$"{u.unitName},{u.getUnitAmount()}\n";
            // print(u);
        }
        unitsList.text=tekst;
    }

    public void Update(){
        showUnits();
    }
}
