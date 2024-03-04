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
        if(gameObjectOnTile!=null){
        unitController _unitOnTile = gameObjectOnTile.GetComponent<unitController>();
        if(_unitOnTile.getUnitDistance()==_unitOnTile.getBaseUnitDistance()){
            Vector2Int dist = _unitOnTile.getUnitDistance();
            dist = new Vector2Int(Mathf.CeilToInt(dist.x/2),Mathf.CeilToInt(dist.y/2));
            _unitOnTile.setUnitDistance(dist);
        }
        }
    }
}
