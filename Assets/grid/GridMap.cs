using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public static class GridMap
{

    //ogolna mapa gridu, static bo w sumie moze sie pozniej przydac w innych klasach
    public static Tile[,] gridMap;

    //ustawieeni mapy grida, moze sie przydac przy np innym ustawieniu grida
    public static void setGridMap(Tile[,] map){
        gridMap=map;
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
            List<Tile> reachableTiles = new List<Tile>();

    // Początkowy tile
    Tile startTile = gridMap[startingPoint.x, startingPoint.y];

    // Kolejka do przechowywania pól do sprawdzenia
    Queue<Tile> queue = new Queue<Tile>();
    queue.Enqueue(startTile);

    // Słownik przechowujący odległość od początkowego pola
    Dictionary<Tile, int> distances = new Dictionary<Tile, int>();
    distances[startTile] = 0;

    while (queue.Count > 0)
    {
        Tile currentTile = queue.Dequeue();
        int currentDistance = distances[currentTile];

        // Sprawdź sąsiadów aktualnego pola
        foreach (Tile neighbor in currentTile.getNeighbours())
        {
            if (neighbor == null || distances.ContainsKey(neighbor)) continue; // Pomijaj już odwiedzone pola

            int neighborDistance = currentDistance + 1;

            // Jeśli odległość do sąsiada w każdym wymiarze jest mniejsza lub równa maksymalnej odległości
            if (Mathf.Abs(neighbor.getPosition().x - startTile.getPosition().x) <= distance.x &&
                Mathf.Abs(neighbor.getPosition().y - startTile.getPosition().y) <= distance.y)
            {
                distances[neighbor] = neighborDistance;
                queue.Enqueue(neighbor);
                reachableTiles.Add(neighbor);
            }
        }
    }

    return reachableTiles;
    }

//Główny skrypt zwracajacy liste <tile> z drogą
//Args: tile startowy, tile docelowy, zasieg ruchu/wyszukiwania, szukanie po wykrytych Tile`
//Tak ukradłem z internetu i pomógł chatGPT
public static List<Tile> FindShortestPath(Tile startTile, Tile targetTile, Vector2Int searchRadius)
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

                    // Sprawdź, czy sąsiadujące pole nie jest przeszkodą
                    if (!(neighbor is obstacleTile))
                    {
                        float distance = Vector2Int.Distance(currentTile.getPosition(), neighbor.getPosition());
                        if (distance <= 1 && !neighbor.isBusy()) //zmiana o 1 bo szukanie w x,y
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

//Metoda do znalezienia sasiedniego pola celu
//Zgadza sie ukradlem z internetu a do tego mała pomoc ChatGTP
private static Tile FindNeighborOfTarget(Tile startTile, Tile targetTile, Vector2Int searchRadius, List<Tile> searchArea)
{
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

        // Jeśli sąsiad jest w obszarze
        if (Vector2Int.Distance(currentTile.getPosition(), targetTile.getPosition()) <= 1)
        {
            return currentTile;
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

                    // Sprawdzamy, czy sąsiadujące pole nie jest wodą lub przeszkodą, ale jeśli nie ma innej alternatywnej ścieżki, to uwzględniamy te pola
                    if (!(neighbor is waterTile || neighbor is obstacleTile) || (neighbor == targetTile && !parentMap.ContainsKey(neighbor)))
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

    // Jeśli nie znaleziono sąsiada, zwracamy null
    return null;
}

    //Pokaz sciezke do celu 
    //glownie do pokazania ruchu po tile danej jednostki
    public static void ShowPathToTile(GameObject source, GameObject target){
        List<Tile> movePath = getPathToTile(source,target);
        enableListTiles(movePath,Color.blue);
    }

    //Wyswietl sciezke do najbliszego pola obok celu
    public static void ShowPathNearGameObject(GameObject source, GameObject target){
        List<Tile> movePath = getPathToNeighbourObject(source,target);
        enableListTiles(movePath,Color.red);
    }

    //Zwroc liste Tile sciezko do danego Tile
    public static List<Tile> getPathToTile(GameObject source,GameObject target){
        Tile targetTile = target.GetComponent<Tile>();
        unitController sourceController = source.GetComponent<unitController>();
        List<Tile> movePath = FindShortestPath(sourceController.getAssignedTile(),targetTile,sourceController.getUnitDistance());
        return movePath;
    }


    public static List<Tile> getPathToNeighbourObject(GameObject source,GameObject target){
        unitController targetController = target.GetComponent<unitController>();
        unitController sourceController = source.GetComponent<unitController>();
        Tile targetTile = targetController.getAssignedTile();
        Tile neighbour = FindNeighborOfTarget(sourceController.getAssignedTile(),targetTile,sourceController.getBaseUnitDistance(),sourceController.getDetector().getMovementTiles());
        List<Tile> movePath = FindShortestPath(sourceController.getAssignedTile(),neighbour,sourceController.getBaseUnitDistance());
        return movePath;
    }

    public static int getDistanceBetweenTiles(Tile source, Tile target){
        return calculateDistanceBetweenPoints(source,target);
    }

    public static int getDistanceBetweenUnits(GameObject source,GameObject target){
        Tile sourceTile = source.GetComponent<unitController>().getAssignedTile();
        Tile targetTile = target.GetComponent<unitController>().getAssignedTile();
        return calculateDistanceBetweenPoints(sourceTile,targetTile);
    }

    private static int calculateDistanceBetweenPoints(Tile source,Tile target){
        Vector2Int[] distances = {source.getPosition(),target.getPosition()};
        int p1 = (distances[1].x-distances[0].x);
        int p2 = (distances[1].y-distances[0].y);
        int res = (int)Math.Sqrt(p1+p2);
        return res;
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

    //Z danej listy zwroc elementy ktore sa przypisane do danego Tile jezeli to GameObject
    public static List<GameObject> findGameObjectsOnTiles(Vector2Int startingPoint,Vector2Int distance,string type){
        List<GameObject> l = new List<GameObject>();
        List<Tile> objectsTiles = new List<Tile>();
        for(int x=Math.Max(0,startingPoint.x-distance.x);x<=Mathf.Min(gridMap.GetLength(0)-1,startingPoint.x+distance.x);x++){
            for(int y=Mathf.Max(0,startingPoint.y-distance.y);y<=Mathf.Min(gridMap.GetLength(1)-1,startingPoint.y+distance.y);y++){
                Tile currentTile = gridMap[x,y];
                objectsTiles.Add(currentTile);
            }
        }
        foreach(var t in objectsTiles){
            if(t.GetGameObjectOnTile()!=null)
                l.Add(t.GetGameObjectOnTile());
        }
        List<GameObject> gList = new List<GameObject>();
        foreach(var t in objectsTiles){
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
        tile.GetComponent<Image>().color=color;
    }

    //Lista tile zostaje aktywowana z kolorem
    public static void enableListTiles(List<Tile> tiles, Color color){
        foreach(var t in tiles){
            t.isActive=true;
            t.GetComponent<Image>().color=color;
        }
    }

    //Ustaw wszystkie Tile z podanej listy na disabled (nie podswietlone)
    public static void disableListTiles(List<Tile> tiles){
        foreach(var t in tiles){
            t.isActive=false;
        }
    }
}
