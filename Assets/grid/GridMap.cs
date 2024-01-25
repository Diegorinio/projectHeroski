using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridMap
{

    //ogolna mapa gridu, static bo w sumie moze sie pozniej przydac w innych klasach
    public static Tile[,] gridMap;

    //ustawieeni mapy grida, moze sie przydac przy np innym ustawieniu grida
    public static void setGridMap(Tile[,] map){
        gridMap=map;
    }
    //Zwroc dwu elementowa tablice reprezentujaca mape z coordami x,y
    public static Tile[,] getGridMap(){
        return gridMap;
    }

    // Zwroc dany Tile w danej pozycji mapy
    public static Tile getTile(int x,int y){
        Debug.Log($"Tilemap at {x},{y} is {gridMap[x,y].name} {gridMap[x,y].isActive}");
        return gridMap[x,y];
    }

    // Zwroc pozycje danego obiektu na mapie jako Vector2Int x,y
    public static Vector2Int getGameObjectMapPosition(GameObject obj){
        Vector2Int position = Vector2Int.zero;
        if(obj.GetComponent<unitController>()){
            Tile _tile = obj.GetComponent<unitController>().getAssignedTile();
            position = _tile.getPosition();
        }
        return position;
    }

    //Zwróć wszystkie kwadraty z mapy w zasięgu obiektu
    public static List<Tile> calculateMapTiles(Vector2Int startingPoint,int distance){
        List<Tile> availableTiles = new List<Tile>();
        for (int x = Mathf.Max(0, startingPoint.x - distance); x <= Mathf.Min(gridMap.GetLength(0) - 1, startingPoint.x + distance); x++)
        {
            for (int y = Mathf.Max(0, startingPoint.y - distance); y <= Mathf.Min(gridMap.GetLength(1) - 1, startingPoint.y + distance); y++)
            {
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

    //Z daje listy Tile zwroc liste wolnych Tile gdzie nie sa zajete
    public static List<Tile> findMovementTiles(List<Tile> tiles){
        List<Tile> mList = new List<Tile>();
        foreach(var tile in tiles){
            if(tile.GetGameObjectOnTile()==null){
                mList.Add(tile);
            }
        }
        return mList;
    }

    //Z danej listy Tile zwroc liste przypisanych obiektow do Tile jezeli takie istnieja
    public static List<GameObject> findGameObjectsOnTiles(List<Tile> tiles){
        List<GameObject> l = new List<GameObject>();
        foreach(var t in tiles){
            if(t.GetGameObjectOnTile()!=null)
                l.Add(t.GetGameObjectOnTile());
        }
        return l;
    }

    //Z danej listy zwroc elementy ktore sa przypisane do danego Tile jezeli to GameObject
    public static List<GameObject> findGameObjectsOnTiles(List<Tile> tiles,string type){
        List<GameObject> gList = new List<GameObject>();
        foreach(var t in tiles){
            if(t.GetGameObjectOnTile()){
                if(t.GetGameObjectOnTile().transform.tag==type){
                    gList.Add(t.GetGameObjectOnTile());
                }
            }
        }
        return gList;
    }

    //Ustaw wszystkie Tile z podanej listy na active (podswietlone)
    public static void enableListTiles(List<Tile> tiles){
        foreach(var t in tiles){
            t.isActive=true;
        }
    }

    //Ustaw wszystkie Tile z podanej listy na disabled (nie podswietlone)
    public static void disableListTiles(List<Tile> tiles){
        foreach(var t in tiles){
            t.isActive=false;
        }
    }
}
