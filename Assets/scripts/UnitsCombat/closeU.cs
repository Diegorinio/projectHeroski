using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeU : Unit
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        unitName="Knight";
        unitBaseDamage=7;
        unitBaseHealth=25;
        gridDistanceX=8;
        gridDistanceY=8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
