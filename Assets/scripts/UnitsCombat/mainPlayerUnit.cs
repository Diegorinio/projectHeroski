using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class mainPlayerUnit : MonoBehaviour
{
public static mainPlayerUnit Instance{get;private set;}
    //jak narazie na staticach
    public List<Unit> playerTeam;

    // public List<Unit> enemyTeam;
    // Start is called before the first frame update
    void Awake(){
        if(Instance==null){
        Instance=this;
        DontDestroyOnLoad(gameObject);
        }
    }

    public void addToTeam(Unit _unit){
        playerTeam.Add(_unit);
    }
    public void addUnitsToTeam(Unit _unit){
        Type _unitType = _unit.GetType();
        Unit existingUnit = playerTeam.FirstOrDefault(unit=>unit.GetType()==_unitType);
        if(existingUnit!=null){
            existingUnit.addUnits(_unit.getUnitAmount());
        }
        else{
            playerTeam.Add(_unit);
        }
    }

    public Unit[] getUnits(){
        return playerTeam.ToArray();
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

    // private Unit get
}
