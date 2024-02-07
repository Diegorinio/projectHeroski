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
            Debug.Log($"AŁA KURWA RZECZYWISCIE WODA");
        unitController _unitOnTile = gameObjectOnTile.GetComponent<unitController>();
        if(_unitOnTile.getUnitDistance()==_unitOnTile.getBaseUnitDistance()){
            Vector2Int dist = _unitOnTile.getUnitDistance();
            _unitOnTile.setUnitDistance(dist.x/2,dist.y/2);
        }
        // _unitOnTile.disableClickable();
        }
    }
}
