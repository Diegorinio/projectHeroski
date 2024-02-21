using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Singleton
//Pozwala przenosic jednostki gracza do dowolnej sceny bez niszczenia
//TODO: to wlasciwie to samo robi mainEnemyUnit wiec mozna kiedys poprawic
public class mainPlayerUnit : MonoBehaviour
{
    //instacja klasy
    public static mainPlayerUnit Instance{get;private set;}
    [SerializeField]
    //Lista przetrzymujaca jednostki gracza w instancji
    // private List<Unit> playerTeam;
    // [SerializeField]
    private Dictionary<int,List<Unit>> playerUnits = new Dictionary<int, List<Unit>>();
    private Hero selectedHero;

    //Jezeli nie ma instancji to utworz
    void Awake(){
        if(Instance==null){
        Instance=this;
        // playerTeam = new List<Unit>();
        playerUnits = new Dictionary<int, List<Unit>>();
        DontDestroyOnLoad(gameObject);
        }
    }


    // Dodaj jednostke do  listy jednostek
    public void addUnitsToTeam(Unit _unit){
        if(isUnitExists(_unit)){
            Unit existingUnit = getExistingUnit(_unit);
            existingUnit.addUnits(_unit.getUnitAmount());
        }
        else{
            int tier=_unit.getUnitTier();
            // playerUnits[tier]=new List<Unit> {_unit};
            addKeyToDictionary(tier,_unit);
        }
    }

    public void assignHeroToTeam(Hero hero){
        if(selectedHero==null){
            selectedHero=hero;
        }
        else{
            
        }
    }

    private void addKeyToDictionary(int _tier,Unit _unit){

        // playerUnits[_tier].Add(_unit);
        if(playerUnits.ContainsKey(_tier)){
            playerUnits[_tier].Add(_unit);
        }
        else{
        playerUnits.Add(_tier,new List<Unit>());
        playerUnits[_tier].Add(_unit);
        }
    }
    
    //Zworc jednostke ktora istnieje
    public Unit getExistingUnit(Unit _unit){
        int _tier = _unit.getUnitTier();
        if(playerUnits.ContainsKey(_tier)){
            Unit existingUnit = playerUnits[_tier].FirstOrDefault(unit=>unit.GetType()==_unit.GetType());
            return existingUnit;
        }
        else{
            return null;
        }
    }

    //Sprawdz czy jednostka juz istnieje w instancji
    public bool isUnitExists(Unit _unit){
        
        if(getExistingUnit(_unit)!=null)
            return true;
        else
            return false;
    }

    //Zwroc jednostki jako tablice
    public Unit[] getUnits(){
        return getUnitsList().ToArray();
    }

    //Zwroc jednostki jako tablice gameObject
    public GameObject[] getUnitsAsGameObject(){
        List<GameObject> list = new List<GameObject>();
        // foreach(var u in playerTeam){
        //     list.Add(u.gameObject);
        // }
        foreach(var unitList in playerUnits.Values){
            foreach(var unit in unitList){
                list.Add(unit.gameObject);
            }
        }
        return list.ToArray();
    }

    //Zwroc Jednostki jako lista
    public List<Unit> getUnitsList(){
        List<Unit> allUnits = new List<Unit>();
        foreach(var unit in playerUnits.Values){
            allUnits.AddRange(unit);
        }
        return allUnits;
    }

    //Usun jednostke z druzyny instancji
    public void removeFromUnits(Unit _unit){
        // playerTeam.Remove(_unit);
        int tier = _unit.getUnitTier();
        if(playerUnits.ContainsKey(tier)){
            playerUnits[tier].Remove(_unit);
        }
    }
}
