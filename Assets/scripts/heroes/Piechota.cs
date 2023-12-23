using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piechota : Role
{
    [SerializeField]
    public override void Awake()
    {
        roleName="Piechota";
        damage=5;
        gridDistance=16;
        attacksNames[0]="cios z potylicy w kostke";
        attacksNames[1]="aura miecza";
    }

    public override int Attack1()
    {
        return damage;
    }

    public override int Attack2()
    {
        return damage+Random.Range(0,10);
    }
}
