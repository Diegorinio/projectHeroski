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
        gridDistance=12;
    }

    // public void Start(){
    //     Debug.Log($"Pierdolona piechota: {damage}, {getAttack(0)}, {getAttack(1)}");
    // }

    public override int Attack1()
    {
        return damage;
    }

    public override int Attack2()
    {
        return damage+1;
    }
}
