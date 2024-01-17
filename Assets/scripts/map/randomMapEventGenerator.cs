using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class randomMapEventGenerator : characterGenerator
{

    private List<GameObject> Enemies = new List<GameObject>();

    int amountOfEventUnits=0;
    void Start(){
        amountOfEventUnits = Random.Range(1,5);
    }

    void setEventUnits(){
        for(int x=0;x<amountOfEventUnits;x++){
        GameObject newEnemy = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Enemy);
        newEnemy.transform.SetParent(mainEnemiesUnit.Instance.gameObject.transform);
        newEnemy.transform.localPosition=Vector3.zero;
        Enemies.Add(newEnemy);
        }
    }

    public void goToFight(string biom){
        if(mainPlayerUnit.Instance.getUnits().Length>0){
        setEventUnits();
        foreach(var e in Enemies){
            Unit _unit = e.GetComponent<Unit>();
            mainEnemiesUnit.Instance.addUnitsToTeam(_unit);
        }
        SceneManager.LoadScene(biom);
        }
        else{
            
        }
    }
}
