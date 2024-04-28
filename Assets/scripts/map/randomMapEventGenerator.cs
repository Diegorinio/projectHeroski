using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Skrypt generujacay losowee jednostki przeciwnika do mapy 
public class randomMapEventGenerator : MonoBehaviour
{
    //Lista przeciwnikow
    private List<GameObject> Enemies = new List<GameObject>();
    private GameObject enemyHero;

    //Ilosc przeciwnikow
    int amountOfEventUnits=0;

    //Na starcie ustaw ilosc typow jednostek przeciwnika od 1 do 5
    void Start(){
        amountOfEventUnits = Random.Range(1,5);
        gameObject.AddComponent<LoadingScene>();
    }

    //Ustaw jednostki
    // Wygeneruj przez UnitSpawner jednostki losowego typu i dodaj do instacji MainEnemiesUnit
    void setEventUnits(){
        for(int x=0;x<amountOfEventUnits;x++){
        // GameObject newEnemy = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Enemy);
        GameObject newEnemy = unitSpawner.spawnRandomUnitGameObject(unitSpawner.tier.T1,unitSpawner.controllers.Enemy,Random.Range(100,300));
        newEnemy.transform.SetParent(mainEnemiesUnit.Instance.gameObject.transform);
        newEnemy.transform.localPosition=Vector3.zero;
        Enemies.Add(newEnemy);
        enemyHero = heroSpawner.spawnRandomHeroGameObject(heroSpawner.HeroController.Enemy);

        }
    }

    //Wywoal motede setEvenUnits i przypisz liste przeciwnkow do instacji mainEnemyUnit
    public void goToFight(string biom){
        if(mainPlayerUnit.Instance.getUnits().Length>0){
        setEventUnits();
        Debug.Log($"Random event generator!");
        foreach(var e in Enemies){
            Unit _unit = e.GetComponent<Unit>();
            mainEnemiesUnit.Instance.addUnitsToTeam(_unit);
        }
        mainEnemiesUnit.Instance.assignHeroToTeam(enemyHero.GetComponent<Hero>());
        // SceneManager.LoadSceneAsync(biom);
        LoadingScene.LoadScene(3);

        }
        else{
            
        }
    }

    // void PrepareUnits(){
    //     setEventUnits();
    //     Debug.Log($"Random event generator!");
    //     foreach(var e in Enemies){
    //         Unit _unit = e.GetComponent<Unit>();
    //         mainEnemiesUnit.Instance.addUnitsToTeam(_unit);
    //     }
    //     mainEnemiesUnit.Instance.assignHeroToTeam(enemyHero.GetComponent<Hero>());
    // }
}
