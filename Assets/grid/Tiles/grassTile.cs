using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassTile : Tile
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void TileBehaviour()
    {
        if(gameObjectOnTile!=null)
            gameObjectOnTile.GetComponent<unitController>().setNormalDistance();
    }
}
