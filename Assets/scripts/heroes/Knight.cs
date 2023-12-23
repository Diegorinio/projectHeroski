using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Role
{
    public override void Awake()
    {
        base.Awake();
        roleName = "Knight";
        damage = 25;
        gridDistance = 6;
        attacksNames[0]="wir miecza";
        attacksNames[1]="Z calej epy!";
    }

    public override int Attack1()
    {
        return damage/2+(Random.Range(0,10));
    }

    public override int Attack2()
    {
        return damage*2-Random.Range(10,15);
    }
    
}
