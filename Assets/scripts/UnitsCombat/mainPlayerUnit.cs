using System.Collections;
using System.Collections.Generic;
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
    public void addUnitsToTeam(Unit _unit, int amount){
        if(playerTeam.Contains(_unit)){
            foreach(var unit in playerTeam){
                if(unit==_unit){
                    unit.addUnits(amount);
                }
            }
        }
        else{
            playerTeam.Add(_unit);
        }
    }

    public Unit[] getUnits(){
        return playerTeam.ToArray();
    }

    // private Unit get
}
