using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Role
{
    public override void Awake()
    {
        base.Awake();
        roleName = "Knight";
        damage = 50;
        gridDistance = 6;
        attacks[0]="wir miecza";
        attacks[1]="Z calej epy!";
    }

    public override int Attack1()
    {
        return damage / 2;
    }

    public override int Attack2()
    {
        return damage - (damage - Random.Range(10, 50));
    }
    
}
