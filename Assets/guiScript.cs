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

    public  void chuj()
    {
        heroName.text = playerCharacter.selectedGameObject.transform.name;
        unsetAttacks();
        setAttacks();
    }

    public void setAttacks()
    {
        // int id = 0;
        // unsetAttacks();
        attackBtns[0].GetComponentInChildren<Text>().text=playerCharacter.selectedGameObject.GetComponent<Role>().attacks[0];
        attackBtns[1].GetComponentInChildren<Text>().text=playerCharacter.selectedGameObject.GetComponent<Role>().attacks[1];
        GameObject enemyFound = playerCharacter.selectedGameObject.GetComponent<playerCharacter>().targetEnemy;
        if(enemyFound!=null){
            Role selectedRole = playerCharacter.selectedGameObject.GetComponent<Role>();
            Enemy enemy = enemyFound.GetComponent<Enemy>();
            attackBtns[0].onClick.AddListener(()=>selectedRole.dealDamageTo(enemy,0));
            attackBtns[1].onClick.AddListener(()=>selectedRole.dealDamageTo(enemy,1));
        }
        // foreach(Button btn in attackBtns)
        // {
        //     btn.GetComponentInChildren<Text>().text = playerCharacter.selectedGameObject.GetComponent<Role>().attacks[id];
        //     GameObject enemyFound = playerCharacter.selectedGameObject.GetComponent<playerCharacter>().targetEnemy;
        //     Debug.Log(enemyFound);
        //     if (enemyFound)
        //     {
        //         Role selectedRole = playerCharacter.selectedGameObject.GetComponent<Role>();
        //         Enemy enemy = enemyFound.GetComponent<Enemy>();
        //         // Debug.Log($"Mozliwy damage: {damage}");
        //          Debug.Log($"ajdi before: {id}");
        //         btn.onClick.AddListener(()=>selectedRole.dealDamageTo(enemy,id));
        //          Debug.Log($"ajdi after: {id}");
        //         btn.GetComponentInChildren<Text>().text = selectedRole.getAttack(id).ToString();
        //         // Debug.Log($"ajdi: {id}");
        //     }
        //     Debug.Log($"ajdi: {id}");
        //     id++;
        // }
    }
    public void unsetAttacks(){
        foreach(Button btn in attackBtns)
        {
            btn.GetComponentInChildren<Text>().text = "";
            GameObject enemyFound = playerCharacter.selectedGameObject.GetComponent<playerCharacter>().targetEnemy;
            btn.onClick.RemoveAllListeners();
        }
    }
}
