using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    public static void clearPrefs(){
        PlayerPrefs.DeleteAll();
    }

    public static void savePrefs(){
        PlayerPrefs.Save();
    }

    public static void addGold(int amount){
        int current = PlayerPrefs.GetInt("GoldInBuilding");
        int afterAddition = current+amount;
        PlayerPrefs.SetInt("GoldInBuilding",afterAddition);
    }
    
}
