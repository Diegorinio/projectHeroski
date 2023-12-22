using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiScript : MonoBehaviour
{
    public Text heroName;
    public Button[] attackBtns;
    public Text bossName;
    // Start is called before the first frame update
    void Start()
    {
    }

    public  void initializeGui()
    {
        if(turnbaseScript.IsHeroTurn()){
            heroName.text = turnbaseScript.selectedGameObject.GetComponent<Hero>().getHeroName();
        }
        unsetAttacks();
        setAttacks();
    }

    public void setAttacks()
    {
        // int id = 0;
        // unsetAttacks();
        attackBtns[0].GetComponentInChildren<Text>().text= turnbaseScript.selectedGameObject.GetComponent<Role>().attacks[0];
        attackBtns[1].GetComponentInChildren<Text>().text= turnbaseScript.selectedGameObject.GetComponent<Role>().attacks[1];
        List<GameObject> enemyFound = turnbaseScript.selectedGameObject.GetComponent<characterController>().targets;
        attackEvent atkEvent =turnbaseScript.selectedGameObject.GetComponent<attackEvent>();
        if(enemyFound.Count>0){
            Role selectedRole = turnbaseScript.selectedGameObject.GetComponent<Role>();
            // Enemy enemy = enemyFound.GetComponent<Enemy>();
            attackBtns[0].onClick.AddListener(()=>atkEvent.setDamageValue(selectedRole.getAttack(0)));
            attackBtns[1].onClick.AddListener(()=>atkEvent.setDamageValue(selectedRole.getAttack(1)));
        }
    }
    // public void setAttack(Role role,Enemy enemy,int id)
    // {
    //     role.dealDamageTo(enemy, id);
    //     turnbaseScript.selectedGameObject.GetComponent<characterController>().disableClickable();
    // }
    public void unsetAttacks(){
        foreach(Button btn in attackBtns)
        {
            btn.GetComponentInChildren<Text>().text = "";
            btn.onClick.RemoveAllListeners();
        }
    }
}
