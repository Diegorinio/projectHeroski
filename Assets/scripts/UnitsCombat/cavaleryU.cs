using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavaleryU:Unit
{
    
    public override void Awake()
    {
        base.Awake();
        unitName="Cavalery";
        unitBaseDamage=7;
        unitBaseHealth=16;
        gridDistanceX=8;
        gridDistanceY=14;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
