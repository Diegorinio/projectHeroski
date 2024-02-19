using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTier : MonoBehaviour
{
    private int tier;
    private UnitSO _unitSO;
    private string unitName;
    private Sprite unitSprite;
    private int unitBaseDamage;
    private int unitBaseHealth;
    private Vector2Int gridMoveDistance;
    private unitGUI _gui;
    public void assignUnitSO(UnitSO _unit){
        _unitSO= _unit;
        unitName = _unitSO.unitName;
        unitBaseDamage=_unitSO.unitBaseDamage;
        unitBaseHealth = _unitSO.unitBaseHealth;
        gridMoveDistance = new Vector2Int(_unitSO.gridDistanceX,_unitSO.gridDistanceY);
        unitSprite = _unitSO.unitSprite;
    }
    // private void Awake(){
    //     unitName = _unitSO.unitName;
    //     unitBaseDamage=_unitSO.unitBaseDamage;
    //     unitBaseHealth = _unitSO.unitBaseHealth;
    //     gridMoveDistance = new Vector2Int(_unitSO.gridDistanceX,_unitSO.gridDistanceY);
    //     unitSprite = _unitSO.unitSprite;
    // }

    private void Start(){
        _gui = gameObject.GetComponent<unitGUI>();
    }

}
