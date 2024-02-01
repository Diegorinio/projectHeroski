using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTile:Tile
{
    protected override void setPreset()
    {
        setTilePreset("waterTile");
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void TileBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
