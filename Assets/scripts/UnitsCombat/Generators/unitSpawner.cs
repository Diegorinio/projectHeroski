using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitSpawner : MonoBehaviour
{
    public static GameObject unitTemplate=Resources.Load("Templates/UnitTemplates/unitTemplate") as GameObject;
    // Start is called before the first frame update
    void Awake()
    {
        unitTemplate=Resources.Load("Templates/UnitTemplates/unitTemplate") as GameObject;
    }

    public enum unitType{distance,cavalery,close}

    public static GameObject spawnUnitGameObject(unitType type,int amount){
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        switch(type){
            case unitType.distance:
            newUnit.AddComponent<distanceU>();
            break;
            case unitType.close:
            newUnit.AddComponent<closeU>();
            break;
            case unitType.cavalery:
            newUnit.AddComponent<cavaleryU>();
            break;
        }
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        return newUnit;
    }
    
    public static GameObject spawnUnitGameObject<T>(int amount) where T :Unit{
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        T _unit = newUnit.AddComponent<T>();
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        return newUnit;
    }

    public static GameObject spawnRandomUnitToGameObject(){
        unitType type = (unitType)UnityEngine.Random.Range(0,3);
        int amount = UnityEngine.Random.Range(100,200);
        GameObject newUnit = spawnUnitGameObject(type,amount);
        newUnit.GetComponent<Unit>().setUnitAmount(UnityEngine.Random.Range(100,200));
        return newUnit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
