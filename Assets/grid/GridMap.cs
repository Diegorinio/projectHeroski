using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridMap
{
    public static Tile[,] gridMap;

    public static void setGridMap(Tile[,] map){
        gridMap=map;
    }
    public static Tile[,] getGridMap(){
        return gridMap;
    }

    public static int[] getGameObjectMapPosition(GameObject obj){
        int[] position = new int[2];
        if(obj.GetComponent<unitController>()){
            Tile _tile = obj.GetComponent<unitController>().getAssignedTile();
            position = _tile.getPosition();
        }
        return position;
    }

    public static void calculateMapTiles(int[] startingPoint,int distance){
        int x = startingPoint[0];
        int y= startingPoint[1];
        int left,right;
        int up,down;
        if((x-distance)<=0){
            left=x;
        }
        else{
            left=x-distance;
        }
        if((x+distance)>=gridMap.GetLength(0)){
            right=x;
        }
        else{
            right=x+distance;
        }
        if((y-distance)<=0){
            up=y;
        }
        else{
            up=y-distance;
        }
        if((y+distance)>=gridMap.GetLength(1)){
            down=y;
        }
        else{
            down=y+distance;
        }
        for(int l=left;l>=y;l--){
            gridMap[l,y].isActive=true;
        }
    }
}
