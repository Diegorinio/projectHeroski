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
    List<Unit> unitlist;
    public GameObject unitListElementTemplate;
    GameObject playerTemp;

    public void Start(){
        // unitlist = mainPlayerUnit.Instance.getUnitsList();
        // Debug.Log($"UnitsList {unitlist.Count}");
        // if(unitlist.Count>0){
        //     showUnits();
        // }
    }
    public void showUnits(){
        foreach(var u in unitlist){
            GameObject newImage = Instantiate(unitListElementTemplate,gameObject.transform.position,Quaternion.identity);
            newImage.transform.SetParent(playerTemp.transform);
            newImage.GetComponent<Image>().sprite = u.getUnitSprite();
            newImage.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void OnEnable(){
        playerTemp = new GameObject("temporarary container");
        playerTemp.AddComponent<RectTransform>();
        playerTemp.AddComponent<GridLayoutGroup>();
        playerTemp.transform.SetParent(gameObject.transform);
        GridLayoutGroup _grid = playerTemp.GetComponent<GridLayoutGroup>();
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = 7;
        _grid.spacing = new Vector2(30,60);
        playerTemp.GetComponent<RectTransform>().localScale = Vector3.one;
        // playerTemp.GetComponent
        unitlist = mainPlayerUnit.Instance.getUnitsList();
        showUnits();
    }

    void OnDisable(){
        Destroy(playerTemp);    
    }

    public void Update(){
        // showUnits();
    }
}
