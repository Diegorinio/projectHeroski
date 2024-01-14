using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavaleryU:Unit
{
    
    public override void Awake()
    {
        // base.Awake();
        unitName="Cavalery";
        unitBaseDamage=7;
        unitBaseHealth=16;
        gridDistanceX=8;
        gridDistanceY=14;
        unitSprite = Resources.Load<Sprite>("Sprites/cavaleryUnit");
    }
}
