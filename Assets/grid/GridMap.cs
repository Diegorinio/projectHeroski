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

}
