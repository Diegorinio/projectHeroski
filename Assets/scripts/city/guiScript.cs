using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Skrypt odpowiedzialny za wyswietlenie jednostek na panelu city
// TODO: ZMIENIć nazwe klasy
// Zmiana lokalizacji panelu
public class guiScript : MonoBehaviour
{
    public GameObject ImagePrefab;
    public List<GameObject> displayedUnitsList = new List<GameObject>();
    public void OnEnable(){
        List<Unit> unitList = mainPlayerUnit.Instance.getUnitListByTier(1);
        foreach(var unit in unitList){
            GameObject tempImageContainer = Instantiate(ImagePrefab,ImagePrefab.transform.position,Quaternion.identity);
            displayedUnitsList.Add(tempImageContainer);
        }

    }
}
