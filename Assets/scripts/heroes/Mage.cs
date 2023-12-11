using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Role
{
    public override void Awake()
    {
        roleName = "Mag";
        damage = 80;
        gridDistance = 10;
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
