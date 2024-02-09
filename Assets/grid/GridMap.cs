using System;
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
    public static List<Tile> calculateMapTiles(Vector2Int startingPoint,Vector2Int distance){
        List<Tile> availableTiles = new List<Tile>();
        for(int x=Math.Max(0,startingPoint.x-distance.x);x<=Mathf.Min(gridMap.GetLength(0)-1,startingPoint.x+distance.x);x++){
            for(int y=Mathf.Max(0,startingPoint.y-distance.y);y<=Mathf.Min(gridMap.GetLength(1)-1,startingPoint.y+distance.y);y++){
                Tile currentTile = gridMap[x,y];
                availableTiles.Add(currentTile);
            }
        }
        gridMap[startingPoint.x,startingPoint.y].isActive=false;
        return availableTiles;
    }

    
    //Zwroc liste Wektorów 2Dint z danej listy
    public static Vector2Int[] getMapTilesVectors(List<Tile> tiles){
        Vector2Int[] resultVectors = new Vector2Int[tiles.Count];
        for(int x=0;x<tiles.Count-1;x++){
            resultVectors[x]=tiles[x].getPosition();
        }
        return resultVectors;
    }

public static List<Tile> FindShortestPath(Tile startTile, Tile targetTile, Vector2Int searchRadius, List<Tile> searchArea)
{
    // Lista do przechowywania odwiedzonych pól
    List<Tile> visited = new List<Tile>();

    // Kolejka dla pól do odwiedzenia
    Queue<Tile> queue = new Queue<Tile>();

    // Mapa przechowująca rodzica danego pola
    Dictionary<Tile, Tile> parentMap = new Dictionary<Tile, Tile>();

    // Koszt dotarcia do danego pola
    Dictionary<Tile, float> costSoFar = new Dictionary<Tile, float>();

    // Dodajemy startowe pole do kolejki i ustawiamy koszt na 0
    queue.Enqueue(startTile);
    costSoFar[startTile] = 0;

    while (queue.Count > 0)
    {
        // Pobieramy pole z kolejki
        Tile currentTile = queue.Dequeue();

        // Jeśli dotarliśmy do celu, przerywamy pętlę
        if (currentTile == targetTile)
            break;

        // Przechodzimy przez sąsiadów aktualnego pola
        for (int xOffset = -searchRadius.x; xOffset <= searchRadius.x; xOffset++)
        {
            for (int yOffset = -searchRadius.y; yOffset <= searchRadius.y; yOffset++)
            {
                int neighborX = currentTile.getPosition().x + xOffset;
                int neighborY = currentTile.getPosition().y + yOffset;

                // Czy sąsiad jest w obszarze
                if (neighborX >= 0 && neighborX < gridMap.GetLength(0) &&
                    neighborY >= 0 && neighborY < gridMap.GetLength(1))
                {
                    Tile neighbor = gridMap[neighborX, neighborY];

                    // Sprawdzamy, czy sąsiadujące pole nie jest wodą lub przeszkodą, ale jeśli nie ma innej alternatywnej ścieżki, to uwzględniamy te pola
                    if (!(neighbor is waterTile || neighbor is obstacleTile) || (neighbor == targetTile && !parentMap.ContainsKey(neighbor)))
                    {
                        float distance = Vector2Int.Distance(currentTile.getPosition(), neighbor.getPosition());
                        if (distance <= 1 && !neighbor.isBusy() && searchArea.Contains(neighbor)) //zmiana o 1 bo suzkanie w x,y
                        {
                            // Koszt dojazdu do sąsiada
                            float newCost = costSoFar[currentTile] + distance;
                            // Jeśli sąsiad nie był odwiedzony lub koszt jest mniejszy niż dotychczasowy
                            if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                            {
                                // Po aktualizacji kosztu dodaj do kolejki
                                costSoFar[neighbor] = newCost;
                                queue.Enqueue(neighbor);
                                parentMap[neighbor] = currentTile;
                            }
                        }
                    }
                }
            }
        }
    }

    // Generowanie pełnej ścieżki od startu do celu
    List<Tile> path = new List<Tile>();
    Tile current = targetTile; // Zacznij od pola docelowego
    while (current != null)
    {
        path.Add(current);
        // Przejście do następnego pola w kierunku startu
        current = parentMap.ContainsKey(current) ? parentMap[current] : null;
    }
    // Odwrócenie ścieżki, aby uzyskać kolejność od startu do celu
    path.Reverse();

    return path;
}

//Zwraca wszystkie mozliwe sciezki
public static Tile[,] FindAllPaths(Tile startTile, Tile targetTile, Vector2Int searchRadius, List<Tile> searchArea)
{
    // Kolejka dla pól do odwiedzenia
    Queue<Tile> queue = new Queue<Tile>();

    // Mapa przechowująca rodzica danego pola
    Dictionary<Tile, Tile> parentMap = new Dictionary<Tile, Tile>();

    // Koszt dotarcia do danego pola
    Dictionary<Tile, float> costSoFar = new Dictionary<Tile, float>();

    // Lista wszystkich ścieżek
    List<List<Tile>> allPaths = new List<List<Tile>>();

    // Dodajemy startowe pole do kolejki i ustawiamy koszt na 0
    queue.Enqueue(startTile);
    costSoFar[startTile] = 0;

    // Wyszukiwanie wszystkich możliwych ścieżek
    while (queue.Count > 0)
    {
        // Pobieramy pole z kolejki
        Tile currentTile = queue.Dequeue();

        // Jeśli dotarliśmy do celu, zapisujemy ścieżkę
        if (currentTile == targetTile)
        {
            List<Tile> path = new List<Tile>();
            Tile pathTile = currentTile;
            while (pathTile != null)
            {
                path.Insert(0, pathTile);
                pathTile = parentMap.ContainsKey(pathTile) ? parentMap[pathTile] : null;
            }
            allPaths.Add(path);
            continue; // Przechodzimy do kolejnego sąsiada
        }

        // Przechodzimy przez sąsiadów aktualnego pola
        for (int xOffset = -searchRadius.x; xOffset <= searchRadius.x; xOffset++)
        {
            for (int yOffset = -searchRadius.y; yOffset <= searchRadius.y; yOffset++)
            {
                int neighborX = currentTile.getPosition().x + xOffset;
                int neighborY = currentTile.getPosition().y + yOffset;

                // Czy sąsiad jest w obszarze
                if (neighborX >= 0 && neighborX < gridMap.GetLength(0) &&
                    neighborY >= 0 && neighborY < gridMap.GetLength(1))
                {
                    Tile neighbor = gridMap[neighborX, neighborY];

                    // Sprawdzamy, czy sąsiadujące pole nie jest wodą lub jest to pole docelowe
                    if (!(neighbor is waterTile) || neighbor == targetTile)
                    {
                        float distance = Vector2Int.Distance(currentTile.getPosition(), neighbor.getPosition());
                        if (distance <= 1 && !neighbor.isBusy() && searchArea.Contains(neighbor)) //zmiana o 1 bo szukanie w x,y
                        {
                            // Koszt dojazdu do sąsiada
                            float newCost = costSoFar[currentTile] + distance;
                            // Jeśli sąsiad nie był odwiedzony lub koszt jest mniejszy niż dotychczasowy
                            if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                            {
                                // Po aktualizacji kosztu dodaj do kolejki
                                costSoFar[neighbor] = newCost;
                                queue.Enqueue(neighbor);
                                parentMap[neighbor] = currentTile;
                            }
                        }
                    }
                }
            }
        }
    }

    // Tworzymy tablicę 2D do przechowywania ścieżek
    Tile[,] allPathsArray = new Tile[allPaths.Count, searchRadius.x * 2 + 1];

    // Przenosimy wszystkie ścieżki do tablicy 2D
    for (int i = 0; i < allPaths.Count; i++)
    {
        for (int j = 0; j < allPaths[i].Count; j++)
        {
            allPathsArray[i, j] = allPaths[i][j];
        }
    }

    return allPathsArray;
}




    //Sprawdz sciezke i zwroc przez wyswietlenie na mapie
    public static void showPath(Tile start, Tile target, Vector2Int radius, List<Tile> area){
        List<Tile> movePath = FindShortestPath(start,target,radius,area);
        enableListTiles(movePath,Color.blue);
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

    public static void enableTile(Tile tile,Color color){
        tile.GetComponent<SpriteRenderer>().color=color;
    }

    public static void enableListTiles(List<Tile> tiles, Color color){
        foreach(var t in tiles){
            t.isActive=true;
            t.GetComponent<SpriteRenderer>().color=color;
        }
    }

    //Ustaw wszystkie Tile z podanej listy na disabled (nie podswietlone)
    public static void disableListTiles(List<Tile> tiles){
        foreach(var t in tiles){
            t.isActive=false;
        }
    }
}
