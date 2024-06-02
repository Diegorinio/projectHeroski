using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    private static string unitPrefix = "UNIT:";
    private static string heroPrefix = "HEROID:";
    public static void clearPrefs(){
        PlayerPrefs.DeleteAll();
    }

    public static void savePrefs(){
        PlayerPrefs.Save();
    }

    public static int getGold(){
        return PlayerPrefs.GetInt("GoldInMenu");
    }

    public static void addGold(int amount){
        int current = PlayerPrefs.GetInt("GoldInMenu");
        int afterAddition = current+amount;
        PlayerPrefs.SetInt("GoldInMenu",afterAddition);
        if(findGoldObject()){
        GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(afterAddition.ToString());
        }
    }

    public static void removeGold(int amount){
        int current = getGold();
        int after = current-amount;
        PlayerPrefs.SetInt("GoldInMenu",after);
        if(findGoldObject()){
        GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(after.ToString());
        }
    }

    private static GameObject findGoldObject(){
        GameObject goldObj = GameObject.Find("Gold_counter");
        return goldObj;
    }

    public static bool isGoldEnough(int amount){
        int current = getGold();
        if(current>=amount){
            return true;
        }
        else{
            return false;
        }
    }

    public static void saveUnit(Unit unit){
        int unitID = unit.getUnitType();
        int unitAmount =  unit.getUnitAmount();
        string unitString = $"{unitPrefix}{unitID}";
        PlayerPrefs.SetInt(unitString,unitAmount);
    }

    public static void resetUnitPref(Unit unit){
        int unitID = unit.getUnitType();
        string unitString = $"{unitPrefix}{unitID}";
        PlayerPrefs.SetInt(unitString,0);
    }


    public static void saveGeneral(Hero hero){
        int heroID = hero.getHeroID();
        PlayerPrefs.SetInt($"{heroPrefix}",heroID);
    }

    public static GameObject loadSavedGeneral(){
        int assignedGeneral = PlayerPrefs.GetInt($"{heroPrefix}");
        Debug.Log($"Loaded general: {assignedGeneral}");
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(assignedGeneral,heroSpawner.HeroController.Player);
        if(spawnedHero !=null){
            mainPlayerUnit.Instance.assignHeroToTeam(spawnedHero.GetComponent<Hero>());
        spawnedHero.gameObject.transform.parent = mainPlayerUnit.Instance.transform;
        spawnedHero.gameObject.transform.localPosition = Vector3.zero;
        }
        return spawnedHero;
    }

    public static void saveUnitstoPrefs(){
        if(mainPlayerUnit.Instance.getUnits().Length>0){
        Unit[] playerUnits = mainPlayerUnit.Instance.getUnits();
        for(int i=0;i<playerUnits.Length;i++){
            saveUnit(playerUnits[i]);
        }
        }
    }

    public static void setPlayerUnitsFromPrefs(){
        Debug.Log($"Load from prefabs");
        Unit[] loadedUnits = loadSavedUnits();
        Debug.Log(loadedUnits.Length);
        if(loadedUnits.Length>0){
            for(int i=0;i<loadedUnits.Length;i++){
                mainPlayerUnit.Instance.addUnitsToTeam(loadedUnits[i]);
                loadedUnits[i].gameObject.transform.parent = mainPlayerUnit.Instance.transform;
                loadedUnits[i].gameObject.transform.localPosition = Vector3.zero;
            }
        }

    }

    //0 -cav 1-close 2-distance
    private static GameObject getLoadedUnit(int id,int amount){
        Debug.Log($"getLoadedUnit:"+amount);
            switch(id){
                case 0:
                return unitSpawner.spawnUnitGameObject(unitSpawner.tier.T1,unitSpawner.unitType.Cavalery,unitSpawner.controllers.Player,amount);
                case 1:
                return unitSpawner.spawnUnitGameObject(unitSpawner.tier.T1,unitSpawner.unitType.Close,unitSpawner.controllers.Player,amount);
                case 2:
                return unitSpawner.spawnUnitGameObject(unitSpawner.tier.T1,unitSpawner.unitType.Distance,unitSpawner.controllers.Player,amount);
                default:
                return unitSpawner.spawnUnitGameObject(unitSpawner.tier.T1,unitSpawner.unitType.Close,unitSpawner.controllers.Player,amount);
            }
    }

    private static Unit[] loadSavedUnits(){
        List<GameObject> units = new List<GameObject>();
        int cavalery= PlayerPrefs.GetInt($"{unitPrefix}0");
        int close = PlayerPrefs.GetInt($"{unitPrefix}1");
        int distance = PlayerPrefs.GetInt($"{unitPrefix}2");
        int[] arr = {cavalery,close,distance};
        for(int i=0;i<arr.Length;i++){
            Debug.Log($"Ilosc jednostek: {arr[i]}");
            if(arr[i]>0){
                units.Add(getLoadedUnit(i,arr[i]));
            }
        }
        Unit[] _unitArr = new Unit[units.Count];
        for(int i=0;i<_unitArr.Length;i++){
            _unitArr[i]=units[i].GetComponent<Unit>();
        }
        return _unitArr;
    }
    
}
