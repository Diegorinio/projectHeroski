using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//Komponent do generaowania mapy i przypisania do statycznej klasy GridMap
public class otherGridManager : MonoBehaviour
{

    //Szerokosc i wysokosc grida
    [SerializeField]
    private int width, height;
    [SerializeField]
    //Preset Tile, jak narazie przez edytor, pozniej zmiana na ladowanie z resources
    private GameObject _tilePreset;
    //Ogolna mapa grida generowana i przekazywana do GridMap
    private Tile[,] gridMapTiles;
    private GameObject[,] gridMapGameObjects;

    void Awake(){
    }
    void Start()
    {
        gridMapTiles=new Tile[width,height];
        gridMapGameObjects = new GameObject[width,height];
        generateGrid();
        gameObject.GetComponent<AdjustLayoutCellSize>().UpdateCellSize();
        setUpTilesToGrid();
    }

    void generateGrid(){
        Debug.Log("Generowanie grida!");
        GridLayoutGroup grupa = gameObject.GetComponent<GridLayoutGroup>();
        grupa.constraintCount=1;
        grupa.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grupa.constraintCount = width;
        for(int x =0;x<width;x++){
            for(int y=0;y<height;y++){
                GameObject newTile = Instantiate(_tilePreset,gameObject.transform.position,Quaternion.identity);

                gridMapGameObjects[x,y]=newTile;
                newTile.transform.SetParent(gameObject.transform);
            }
        }
        SetMapTiles(gridMapGameObjects);
        //Przekaz wygenerowana mape do GridMap
        GridMap.setGridMap(gridMapTiles);
        setNeighbours();
        generatePlayer();
        generateEnemies();
    }

    private void SetMapTiles(GameObject[,] mapTiles){
        for(int x=0;x<width;x++){
            for(int y=0;y<height;y++){
                int rnd = Random.Range(0,100);
                if(rnd>=95){
                    mapTiles[x,y].AddComponent<obstacleTile>();
                    if(x>0&&mapTiles[x-1,y].GetComponent<obstacleTile>()==null&&mapTiles[x-1,y].GetComponent<Tile>()==null){
                        mapTiles[x-1,y].AddComponent<obstacleTile>();
                    }
                }
                else if(rnd>=60&&rnd<69){
                    mapTiles[x,y].AddComponent<waterTile>();
                    int rndWaterSize=Random.Range(1,3);
                    for(int i=0;i<rndWaterSize;i++){
                        for(int j=0;j<rndWaterSize;j++){
                            if(x+i<width&&y+j<height){
                                if(mapTiles[x+i,y+j].GetComponent<waterTile>()==null&&mapTiles[x+i,y+j].GetComponent<Tile>()==null){
                                    mapTiles[x+i,y+j].AddComponent<waterTile>();
                                }
                            }
                        }
                    }
                }
                else{
                    mapTiles[x,y].AddComponent<grassTile>();
                }

                mapTiles[x,y].name = $"{mapTiles[x,y].GetComponent<Tile>().GetType()}{x}{y}";
                Tile newTileComponent = mapTiles[x,y].GetComponent<Tile>();
                newTileComponent.setPosition(x,y);
                newTileComponent.isActive=false;
                gridMapTiles[x,y]=newTileComponent;
            }
        }
    }

    //Ustawia skale Tile do rozmiarow GridLayoutGroup
    private void setUpTilesToGrid(){
        Vector2 cell = gameObject.GetComponent<AdjustLayoutCellSize>().getGridCells();
        foreach(var tile in gridMapTiles){
            tile.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    //Ustaw sasiadow 
    private void setNeighbours(){
        for(int x=0;x<gridMapTiles.GetLength(0);x++){
            for(int y=0;y<gridMapTiles.GetLength(1);y++){
                Tile newTileComponent = gridMapTiles[x,y].GetComponent<Tile>();
                //Dodawanie sasiadow
                if(x>0){//lewo
                    if(!isInList(newTileComponent.getNeighbours(),gridMapTiles[x-1,y])&& !gridMapTiles[x-1,y].isBusy()){
                    newTileComponent.addNeighbour(gridMapTiles[x-1,y]);
                    gridMapTiles[x-1,y].addNeighbour(newTileComponent);
                    Debug.Log($"Tile {newTileComponent.name} lewy sasiad {gridMapTiles[x-1,y].name}");
                    }
                }
                if(x<width-1){//prawo
                if(!isInList(newTileComponent.getNeighbours(),gridMapTiles[x+1,y])&& !gridMapTiles[x+1,y].isBusy()){
                    newTileComponent.addNeighbour(gridMapTiles[x+1,y]);
                    gridMapTiles[x+1,y].addNeighbour(newTileComponent);
                    Debug.Log($"Tile {newTileComponent.name} prawy sasiad {gridMapTiles[x+1,y].name}");
                    }
                }
                if(y>0){//dol
                if(!isInList(newTileComponent.getNeighbours(),gridMapTiles[x,y-1])&& !gridMapTiles[x,y-1].isBusy()){
                    newTileComponent.addNeighbour(gridMapTiles[x,y-1]);
                    gridMapTiles[x,y-1].addNeighbour(newTileComponent);
                    Debug.Log($"Tile {newTileComponent.name} dolny sasiad {gridMapTiles[x,y-1].name}");
                    }
                }
                if(y<height-1){//gora
                if(!isInList(newTileComponent.getNeighbours(),gridMapTiles[x,y+1])&& !gridMapTiles[x,y+1].isBusy()){
                    newTileComponent.addNeighbour(gridMapTiles[x,y+1]);
                    gridMapTiles[x,y+1].addNeighbour(newTileComponent);
                    Debug.Log($"Tile {newTileComponent.name} gorny sasiad {gridMapTiles[x,y+1].name}");
                    }
                }
            }
        }
    }
    // Przy wyszukiwaniu sasiadow sprawdz czy juz nie jest w liscie (dla bezpieczenstwa)
    private bool isInList(List<Tile> have,Tile toAdd){
        return have.Contains(toAdd);
    }

    //Przypisz jednostki gracza do losowych Tile startowych
    private void generatePlayer(){
        GameObject[] heroes = mainPlayerUnit.Instance.getUnitsAsGameObject();
        Debug.Log($"heroes size {heroes.Length}");
        foreach(GameObject hero in heroes)
        {
            hero.transform.parent=null;
            int rnd = Random.Range(0,width-1);
            Tile spawnTile = gridMapTiles[rnd,0];
            hero.GetComponent<unitController>().setTile(spawnTile);
            spawnTile.makeBusy();
            Vector3 nPos = gridMapTiles[rnd,0].transform.position;
            Debug.Log($"Tile Transform {nPos.x},{nPos.y}");
            hero.transform.position= new Vector3(nPos.x,nPos.y,-1);
            hero.SetActive(true);
            hero.GetComponent<unitController>().characterMove(spawnTile.gameObject,true);
        }
    }

    //Przypisz jednostki przeciwnika do losowych Tile startowych
    private void generateEnemies(){
         GameObject[] enemies = mainEnemiesUnit.Instance.getUnitsAsGameObject();
        foreach(GameObject enemy in enemies){
            enemy.transform.parent=null;
            int rnd = Random.Range(0,width-1);
            Tile spawnTile = gridMapTiles[rnd,height-1];
            spawnTile.makeBusy();
            enemy.GetComponent<unitController>().setTile(spawnTile);
            Vector3 nPos = gridMapTiles[rnd,height-1].transform.position;
            enemy.transform.position = new Vector3(nPos.x,nPos.y,-1);
            // enemy.GetComponent<unitController>().characterMoveToTile(spawnTile.gameObject);
            enemy.GetComponent<unitController>().characterMove(spawnTile.gameObject,true);
            enemy.SetActive(true);
        }
    }
}
