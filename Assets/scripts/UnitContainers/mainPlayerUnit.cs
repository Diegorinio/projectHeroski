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
    private List<Unit> playerTeam;

    //Jezeli nie ma instancji to utworz
    void Awake(){
        if(Instance==null){
        Instance=this;
        playerTeam = new List<Unit>();
        DontDestroyOnLoad(gameObject);
        }
    }

    //Dodaj jednostke
    public void addToTeam(Unit _unit){
        playerTeam.Add(_unit);
    }
    // Dodaj liste jednostek
    public void addUnitsToTeam(Unit _unit){
        if(isUnitExists(_unit)){
            Unit existingUnit = getExistingUnit(_unit);
            existingUnit.addUnits(_unit.getUnitAmount());
        }
        else{
            playerTeam.Add(_unit);
        }
    }
    //Zworc jednostke ktora istnieje
    public Unit getExistingUnit(Unit _unit){
        Unit exisingUnit = playerTeam.FirstOrDefault(unit=>unit.GetType()==_unit.GetType());
        return exisingUnit;
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
        return playerTeam.ToArray();
    }

    //Zwroc jednostki jako tablice gameObject
    public GameObject[] getUnitsAsGameObject(){
        List<GameObject> list = new List<GameObject>();
        foreach(var u in playerTeam){
            list.Add(u.gameObject);
        }
        return list.ToArray();
    }

    //Zwroc Jednostki jako lista
    public List<Unit> getUnitsList(){
        return playerTeam;
    }

    //Usun jednostke z druzyny instancji
    public void removeFromUnits(Unit _unit){
        playerTeam.Remove(_unit);
    }
}
