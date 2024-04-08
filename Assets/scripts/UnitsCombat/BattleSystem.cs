using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
//0-range 1-kon 2-piechota typy
    private static int calculateDamage(Unit dealer,Unit victim){
        // int widelki = new System.Random().Next(80, 101);
        int widelki = Random.Range(80,101);
        double dmg_from_dealer = (GetCritChange(dealer, victim) ? 1.5:1*(IsCounter(dealer,victim)? 1.5:1));
        Debug.Log($"Dmg from dealer {dmg_from_dealer}");
// dealer.getTotalDamage()))*((double)widelki/100)
        int returnDmg = (int)(dmg_from_dealer*(dealer.getTotalDamage()*((double)widelki/100)/victim.unitBaseHealth));
        return (int)returnDmg;
    }

    public static int getDealtDamage(Unit dealer,Unit victim){
        return calculateDamage(dealer,victim);
    }
    private static bool GetCritChange(Unit dealer,Unit victim)
    {
        var d20 = new System.Random().Next(21);
        
        if (IsCounter(dealer,victim)){
           if(d20<6) return true;
        }
        else { if (d20<3) return true; }
        return false;


    }
    private static bool IsCounter(Unit dealer,Unit victim)
    {
        if (dealer.getUnitType() == 0 && victim.getUnitType() == 2) { return true; }
        if(dealer.getUnitType()==2 &&victim.getUnitType()==1) { return true; }
        if(dealer.getUnitType()==1 &&victim.getUnitType()==0) { return true; }


        return false;
    }
}
