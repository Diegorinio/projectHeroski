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

    public static Tile getTile(int x,int y){
        Debug.Log($"Tilemap at {x},{y} is {gridMap[x,y].name} {gridMap[x,y].isActive}");
        return gridMap[x,y];
    }

    public static Vector2Int getGameObjectMapPosition(GameObject obj){
        Vector2Int position = Vector2Int.zero;
        if(obj.GetComponent<unitController>()){
            Tile _tile = obj.GetComponent<unitController>().getAssignedTile();
            position = _tile.getPosition();
        }
        return position;
    }

    public static List<Tile> calculateMapTiles(Vector2Int startingPoint,int distance){
        List<Tile> availableTiles = new List<Tile>();
        for (int x = Mathf.Max(0, startingPoint.x - distance); x <= Mathf.Min(gridMap.GetLength(0) - 1, startingPoint.x + distance); x++)
        {
            for (int y = Mathf.Max(0, startingPoint.y - distance); y <= Mathf.Min(gridMap.GetLength(1) - 1, startingPoint.y + distance); y++)
            {
                // Zmiana znaku tylko jeśli odległość (w tym narożniki) jest mniejsza lub równa podanemu dystansowi
                if (Mathf.Abs(startingPoint.x - x) + Mathf.Abs(startingPoint.y - y) <= distance)
                {
                    // gridMap[x,y].isActive=true;
                    availableTiles.Add(gridMap[x,y]);
                }
            }
        }
        gridMap[startingPoint.x,startingPoint.y].isActive=false;
        return availableTiles;
    }
}
