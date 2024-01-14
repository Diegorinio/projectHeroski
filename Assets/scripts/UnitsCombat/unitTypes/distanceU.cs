using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceU:Unit
{
    public override void Awake()
    {
        base.Awake();
        unitName="Distance U";
        unitBaseDamage=5;
        unitBaseHealth=20;
        gridDistanceX=14;
        gridDistanceY=14;
        unitSprite = Resources.Load<Sprite>("Sprites/distanceUnit");
    }
}
