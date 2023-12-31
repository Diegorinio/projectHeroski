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
    private bool isHero=false;
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
        GameObject enemyFound = turnbaseScript.selectedGameObject.GetComponent<characterController>().targetEnemy;
        if(enemyFound!=null){
            Role selectedRole = turnbaseScript.selectedGameObject.GetComponent<Role>();
            Enemy enemy = enemyFound.GetComponent<Enemy>();
            attackBtns[0].onClick.AddListener(()=>setAttack(selectedRole,enemy,0));
            attackBtns[1].onClick.AddListener(()=>setAttack(selectedRole,enemy,1));
        }
    }
    public void setAttack(Role role,Enemy enemy,int id)
    {
        role.dealDamageTo(enemy, id);
        turnbaseScript.selectedGameObject.GetComponent<characterController>().disableClickable();
    }
    public void unsetAttacks(){
        foreach(Button btn in attackBtns)
        {
            btn.GetComponentInChildren<Text>().text = "";
            btn.onClick.RemoveAllListeners();
        }
    }
}
