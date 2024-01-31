using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeU : Unit
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        unitName="Close U";
        unitBaseDamage=7;
        unitBaseHealth=25;
        gridDistanceX=1;
        gridDistanceY=1;
        unitSprite = Resources.Load<Sprite>("Sprites/closeUnit");
    }
}
