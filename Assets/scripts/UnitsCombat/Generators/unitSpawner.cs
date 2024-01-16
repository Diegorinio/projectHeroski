using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class unitSpawner : MonoBehaviour
{

    //Statyczna jako ze dostepna zawsze gdy potrzeba a w sumie potrzeba tylko jedna instacje

    //Zaladowanie GameObjectu Unit jako template
    public static GameObject unitTemplate;
    // Start is called before the first frame update
    void Awake()
    {
        unitTemplate=Resources.Load("Templates/UnitTemplates/unitTemplate") as GameObject;
    }

    //Typy jednostek i jaki kontroler
    public enum unitType{distance,cavalery,close}
    public enum controllers{Player,Enemy}

    //Zespawnij dany typ i dana ilosc
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
        newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<Unit>().unitSprite;
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        newUnit.name=$"{type} {amount}";
        return newUnit;
    }

    // public static GameObject spawnUnitGameObject(unitType type,int amount,controllers controller){
    //     GameObject _newUnit = spawnUnitGameObject(type, amount);
    //     switch (controller)
    //     {
    //         case controllers.Player:
    //         assignPlayerAttributes(_newUnit);
    //         break;
    //         case controllers.Enemy:
    //         assignEnemyAttributes(_newUnit);
    //         break;
    //     }
    //     _newUnit.name=$"{controller} {type}";
    //     return _newUnit;
    // }

    // Dodaj komponenty potrzebne jednostce gracza
    private static void assignPlayerAttributes(GameObject obj){
        obj.transform.Find("tileDetector").AddComponent<Detector>();
    }

    // Dodaj komponenety potrzebne jednostce przeciwnika
    private static void assignEnemyAttributes(GameObject obj){
        obj.AddComponent<enemyAI>();
        obj.AddComponent<Enemy>();
        obj.transform.Find("tileDetector").AddComponent<enemyDetector>();
        obj.transform.tag="Enemy";
    }


    //Utworz losowy typ jednostki
    public static GameObject spawnRandomUnitToGameObject(){
        unitType type = (unitType)UnityEngine.Random.Range(0,3);
        int amount = UnityEngine.Random.Range(100,200);
        GameObject newUnit = spawnUnitGameObject(type,amount);
        newUnit.GetComponent<Unit>().setUnitAmount(UnityEngine.Random.Range(100,200));
        return newUnit;
    }

    //Utworz lososwy typ jednostki z wyborem czy to jednostka przeciwna czy gracza
    public static GameObject spawnRandomUnitToGameObject(controllers controller){
        GameObject _newUnit = spawnRandomUnitToGameObject();
        switch(controller){
            case controllers.Player:
            assignPlayerAttributes(_newUnit);
            break;
            case controllers.Enemy:
            assignEnemyAttributes(_newUnit);
            break;
        }
        _newUnit.name +=$" {controller}";
        _newUnit.SetActive(false);
        return _newUnit;
    }
}
