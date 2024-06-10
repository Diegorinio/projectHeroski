using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Instacja main EnemisUnit
//Pozwala przenosic jednostki przeciwnka z mapy do sceny

//Doslownie to samo co mainPlayerUnit
public class mainEnemiesUnit : MonoBehaviour
{
public static mainEnemiesUnit Instance{get;private set;}
[SerializeField]
    private List<Unit> playerTeam;
    private Hero selectedEnemyHero;
    public GameObject selectedEnemyHeroG;
    void Awake(){
        if(Instance==null){
        Instance=this;
        playerTeam =new List<Unit>();
        DontDestroyOnLoad(gameObject);
        }
    }
    public void addUnitsToTeam(Unit _unit){
        if(isUnitExists(_unit)){
            Unit existingUnit = getExistingUnit(_unit);
            existingUnit.addUnits(_unit.getUnitAmount());
        }
        else{
            playerTeam.Add(_unit);
        }
    }
    public Hero getSelectedHero(){
        return selectedEnemyHero;
    }

    public void assignHeroToTeam(Hero hero){
        selectedEnemyHero=hero;
        selectedEnemyHeroG = hero.gameObject;
        selectedEnemyHeroG.transform.SetParent(gameObject.transform);
    }

    public Unit getExistingUnit(Unit _unit){
        Unit exisingUnit = playerTeam.FirstOrDefault(unit=>unit.GetType()==_unit.GetType());
        return exisingUnit;
    }
    public bool isUnitExists(Unit _unit){
        if(getExistingUnit(_unit)!=null)
            return true;
        else
            return false;
    }
    public GameObject[] getUnitsAsGameObject(){
        List<GameObject> list = new List<GameObject>();
        foreach(var u in playerTeam){
            list.Add(u.gameObject);
        }
        return list.ToArray();
    }
    public List<Unit> getUnitsList(){
        return playerTeam;
    }

    public void clearEnemyTeamList(){
        if(playerTeam.Count>0){
        foreach(var el in playerTeam){
            Destroy(el.gameObject);
        }
        }
        playerTeam.Clear();
        playerTeam = new List<Unit>();
        Destroy(selectedEnemyHero);
        Destroy(selectedEnemyHeroG);
    }

    public void removeFromUnits(Unit _unit){
        playerTeam.Remove(_unit);
        Debug.Log($" MainEnemiesUnit Unit removed ${_unit.name}");
    }
}
