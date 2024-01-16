using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class randomMapEventGenerator : characterGenerator
{
    [SerializeField]
    private List<GameObject> enemiesList=new List<GameObject>();
    [SerializeField]
    private List<string> enemiesListNames=new List<string>();
    [SerializeField]
    // private GameObject eventPanel;
    Text eventNameText;
    Text eventEnemiesText;
    string[] eventNames = {"test","Kutang klan", "XD", "Potężna wichura"};
    string[] eventEnemies={"Chleb","Mieso","Wszystko","Gomez","Enemy1","test"};
    Button fightBtn;

    // void Start(){
    //     int amount = Random.Range(1,4);
    //     for(int x=0;x<amount;x++){
    //         enemiesListNames.Add(eventNames[Random.Range(0,eventNames.Length-1)]);
    //     }
    // }
    // public void generateEvent(){
    //     foreach(var name in enemiesListNames){
    //         GameObject newEnemy = generateRandomCharacter(characterType.Enemy,name);
    //         Enemy _enemy = newEnemy.GetComponent<Enemy>();
    //         _enemy.setHeroHealth(Random.Range(101,160));
    //         enemiesList.Add(newEnemy);

    //         newEnemy.transform.SetParent(mainPlayer.Instance.gameObject.transform);
    //         newEnemy.transform.localPosition=Vector3.zero;
    //     }
    // }


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
        setEventUnits();
        foreach(var e in Enemies){
            Unit _unit = e.GetComponent<Unit>();
            mainEnemiesUnit.Instance.addUnitsToTeam(_unit);
        }
        SceneManager.LoadScene(biom);
    }
}
