using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pichota : Role
{
    [SerializeField]
    public override void Awake()
    {
        roleName="Piechota";
        damage=0;
        gridDistance=16;
        attacks[0]="cios z potylicy w kostke";
        attacks[1]="aura miecza";
    }

    public override int Attack1()
    {
        return damage;
    }

    public override int Attack2()
    {
        return damage+1;
    }
}
