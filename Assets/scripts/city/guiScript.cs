using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private List<GameObject> imagesList = new List<GameObject>();
    private GameObject createUnitElement(Unit _unit){
        GameObject newUnitElement = Instantiate(unitListElementTemplate,gameObject.transform.position,Quaternion.identity);
        TextMeshProUGUI unitAmountText = newUnitElement.transform.Find("amount_nr").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI unitTierText = newUnitElement.transform.Find("tier_nr").GetComponent<TextMeshProUGUI>();
        newUnitElement.GetComponent<Image>().sprite = _unit.getUnitImage().sprite;
        unitAmountText.text = _unit.getUnitAmount().ToString();
        unitTierText.text = _unit.getUnitTier().ToString();
        return newUnitElement;
    }
    // public void showUnits(){
    //     foreach(var u in unitlist){
    //         GameObject newImage = Instantiate(unitListElementTemplate,gameObject.transform.position,Quaternion.identity);
    //         newImage.transform.SetParent(playerTemp.transform);
    //         newImage.GetComponent<Image>().sprite = u.unitSprite;
    //         newImage.GetComponent<RectTransform>().localScale = Vector3.one;
    //         imagesList.Add(newImage);
    //     }
    // }

    void OnEnable(){
        unitlist = mainPlayerUnit.Instance.getUnitsList();
        clearUnitsElements();
        foreach(var u in unitlist){
            imagesList.Add(createUnitElement(u));
        }
        attachImagesToParent();
    }

    void attachImagesToParent(){
        foreach(var i in imagesList){
            i.transform.parent = gameObject.transform;
            i.transform.localScale = Vector3.one;
        }
    }
    // void MakeStart(){
    //     clearUnitsElements();
    //     playerTemp = new GameObject("temporarary container");
    //     playerTemp.AddComponent<RectTransform>();
    //     playerTemp.AddComponent<GridLayoutGroup>();
    //     playerTemp.transform.SetParent(gameObject.transform);
    //     GridLayoutGroup _grid = playerTemp.GetComponent<GridLayoutGroup>();
    //     _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    //     _grid.constraintCount = 7;
    //     _grid.spacing = new Vector2(30,60);
    //     playerTemp.GetComponent<RectTransform>().localScale = Vector3.one;
    //     // playerTemp.GetComponent
    //     unitlist = mainPlayerUnit.Instance.getUnitsList();
    //     showUnits();
    // }

    private void clearUnitsElements(){
        if(imagesList.Count>0){
            for(int i=0;i<imagesList.Count;i++){
                Destroy(imagesList[i]);
            }
        }
        imagesList = new List<GameObject>();
    }
}
