using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private static int calculateDamage(Unit dealer,Unit victim){
        int dmg_from_dealer = dealer.getTotalDamage();
        int returnDmg = (int)(dmg_from_dealer/victim.unitBaseHealth);
        return returnDmg;
    }

    public static int getDealtDamage(Unit dealer,Unit victim){
        return calculateDamage(dealer,victim);
    }
}
