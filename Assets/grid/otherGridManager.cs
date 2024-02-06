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

    void Awake(){
    }
    void Start()
    {
        gridMapTiles=new Tile[width,height];
        Debug.Log($"Ekran width {Screen.width} height:{Screen.height}");
        generateGrid();

    }

    void generateGrid(){
        Debug.Log("Generowanie grida!");
        GridLayoutGroup grupa = gameObject.GetComponent<GridLayoutGroup>();
        grupa.constraintCount=1;
        grupa.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grupa.constraintCount = width;

        for(int x =0;x<width;x++){
            for(int y=0;y<height;y++){
                var newTile = Instantiate(_tilePreset,transform.position,Quaternion.identity);
                //Czy Tile jest eventem np woda czy coś
                int rnd = Random.Range(0,100);
                if(rnd>=80){
                    newTile.AddComponent<obstacleTile>();
                }
                else if(rnd>50&&rnd<69){
                    newTile.AddComponent<waterTile>();
                }
                else{
                    newTile.AddComponent<grassTile>();
                }
                newTile.transform.SetParent(gameObject.transform);
                newTile.name = $"Tile{x}{y}";
                RectTransform rectTransform = newTile.GetComponent<RectTransform>();
                rectTransform.localScale= new Vector3(100,100,1);
                Tile newTileComponent = newTile.GetComponent<Tile>();
                newTileComponent.setPosition(x,y);
                newTileComponent.isActive=false;
                gridMapTiles[x,y]=newTileComponent;
            }
        }

        // ustaw sasiadow
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
        GridMap.setGridMap(gridMapTiles);
        // List<Tile> xd = GridMap.FindShortestPath(gridMapTiles[4,0],gridMapTiles[2,6]);
        // foreach(var x in xd){
            // x.isActive=true;
        // }
        generatePlayer();
        generateEnemies();
    }

    private bool isInList(List<Tile> have,Tile toAdd){
        return have.Contains(toAdd);
    }

    private void generatePlayer(){
        GameObject[] heroes = mainPlayerUnit.Instance.getUnitsAsGameObject();
        Debug.Log($"heroes size {heroes.Length}");
        // heroes[0]=spawnedUnit;
        foreach(GameObject hero in heroes)
        {
            hero.transform.parent=null;
            // hero.GetComponent<characterController>().characterMove(gridMap[Random.Range(0,9)].gameObject);
            int rnd = Random.Range(0,width-1);
            // gridMap[rnd].makeBusy();
            // gridMapTiles[rnd,0].makeBusy();
            Tile spawnTile = gridMapTiles[rnd,0];
            hero.GetComponent<unitController>().setTile(spawnTile);
            spawnTile.makeBusy();
            Vector3 nPos = gridMapTiles[rnd,0].transform.position;
            Debug.Log($"Tile Transform {nPos.x},{nPos.y}");
            hero.transform.position= new Vector3(nPos.x,nPos.y,-1);
            hero.SetActive(true);
            // hero.GetComponent<unitController>().characterMoveToTile(spawnTile.gameObject);
            hero.GetComponent<unitController>().characterMove(spawnTile.gameObject,true);
            // hero.SetActive(true);
        }
    }
    
    private void generateEnemies(){
         GameObject[] enemies = mainEnemiesUnit.Instance.getUnitsAsGameObject();
        foreach(GameObject enemy in enemies){
            enemy.transform.parent=null;
            // int rnd=Random.Range(gridMap.Count-1,gridMap.Count-9);
            int rnd = Random.Range(0,width-1);
            Tile spawnTile = gridMapTiles[rnd,height-1];
            // gridMap[rnd].makeBusy();
            // gridMapTiles[rnd,height-1].makeBusy();
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
