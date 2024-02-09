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
        gridMoveDistance = new Vector2Int(2,3);
        gridAttackDistance = new Vector2Int(2,4);
        unitSprite = Resources.Load<Sprite>("Sprites/spearman");
    }
}
