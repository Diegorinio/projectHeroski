using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// [RequireComponent(typeof(characterGUI))]
public class Enemy:MonoBehaviour
{
    public void OnMouseDown(){
        if(turnbaseScript.IsHeroTurn()){
            turnbaseScript.selectedGameObject.GetComponent<unitController>().hitToSelectedTarget(gameObject);
        }
    }
    
}
