using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
// [RequireComponent(typeof(characterGUI))]
public class Enemy:Hero
{
    public override void Awake()
    {
        _gui=gameObject.GetComponent<enemyGUI>();
    }

    public void dealDamageTo(GameObject target){
        attackEvent atkEvent = gameObject.GetComponent<attackEvent>();
        if(target.GetComponent<Hero>()&&atkEvent.isSet){
            Hero h = target.GetComponent<Hero>();
            h.getHit(atkEvent.damage);
            atkEvent.isSet=false;
            turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
            script.nextTurn();
        }
    }

    public void OnMouseDown(){
        if(turnbaseScript.IsHeroTurn()){
            turnbaseScript.selectedGameObject.GetComponent<characterController>().hitToSelectedTarget(gameObject);
        }
    }
    
}
