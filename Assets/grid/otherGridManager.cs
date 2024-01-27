using System.Collections;
using System.Collections.Generic;
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
    private Tile _tilePreset;
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
        GridLayoutGroup grupa = gameObject.GetComponent<GridLayoutGroup>();
        grupa.constraintCount=1;
        grupa.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grupa.constraintCount = width;

        for(int x =0;x<width;x++){
            for(int y=0;y<height;y++){
                var newTile = Instantiate(_tilePreset,transform.position,Quaternion.identity);
                newTile.transform.SetParent(gameObject.transform);
                newTile.name = $"Tile{x}{y}";
                RectTransform rectTransform = newTile.GetComponent<RectTransform>();
                rectTransform.localScale= new Vector3(100,100,1);
                newTile.GetComponent<Tile>().setPosition(x,y);
                gridMapTiles[x,y]=newTile;
            }
        }
        GridMap.setGridMap(gridMapTiles);
        GameObject[] heroes = mainPlayerUnit.Instance.getUnitsAsGameObject();
        Debug.Log($"heroes size {heroes.Length}");
        // heroes[0]=spawnedUnit;
        foreach(GameObject hero in heroes)
        {
            hero.transform.parent=null;
            // hero.GetComponent<characterController>().characterMove(gridMap[Random.Range(0,9)].gameObject);
            int rnd = Random.Range(0,height-1);
            // gridMap[rnd].makeBusy();
            gridMapTiles[0,rnd].makeBusy();
            hero.GetComponent<unitController>().setTile(gridMapTiles[0,rnd]);
            Vector3 nPos = gridMapTiles[0,rnd].transform.position;
            hero.transform.position = new Vector3(nPos.x,nPos.y,-1);
            hero.SetActive(true);
        }
        GameObject[] enemies = mainEnemiesUnit.Instance.getUnitsAsGameObject();
        foreach(GameObject enemy in enemies){
            enemy.transform.parent=null;
            // int rnd=Random.Range(gridMap.Count-1,gridMap.Count-9);
            int rnd = Random.Range(0,height-1);
            // gridMap[rnd].makeBusy();
            gridMapTiles[width-1,rnd].makeBusy();
            enemy.GetComponent<unitController>().setTile(gridMapTiles[width-1,rnd]);
            Vector3 nPos = gridMapTiles[width-1,rnd].transform.position;
            enemy.transform.position = new Vector3(nPos.x,nPos.y,-1);
            enemy.SetActive(true);
        }

    }

    // void generateGrid()
    // {
    //     float firstPosX = 0;
    //     float firstPosY = 0;
    //     for(int x = 0; x < width; x++)
    //     {
    //         for (int y = 0; y < height; y++)
    //         {
    //             var screenPoint = new Vector3(x+offsetX,y+offsetY,0);
    //             var worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
    //             var spawnTile = Instantiate(_tilePreset, new Vector2(worldPos.x+firstPosX,worldPos.y+firstPosY), Quaternion.identity);
    //             spawnTile.name = $"Tile{x}{y}";
    //             spawnTile.transform.SetParent(gameObject.transform);
    //             spawnTile.GetComponent<Tile>().setPosition(x,y);
    //             firstPosY += spacer;
    //             // gridMap.Add(spawnTile);
    //             // gridMapTiles[x,y]=spawnTile;
                
    //         }
    //         firstPosY = 0;
    //         firstPosX += spacer;
    //     }
    //     GridMap.setGridMap(gridMapTiles);
    //     GameObject[] heroes = mainPlayerUnit.Instance.getUnitsAsGameObject();
    //     Debug.Log($"heroes size {heroes.Length}");
    //     // heroes[0]=spawnedUnit;
    //     foreach(GameObject hero in heroes)
    //     {
    //         hero.transform.parent=null;
    //         // hero.GetComponent<characterController>().characterMove(gridMap[Random.Range(0,9)].gameObject);
    //         int rnd = Random.Range(0,height-1);
    //         // gridMap[rnd].makeBusy();
    //         gridMapTiles[0,rnd].makeBusy();
    //         hero.GetComponent<unitController>().setTile(gridMapTiles[0,rnd]);
    //         Vector3 nPos = gridMapTiles[0,rnd].transform.position;
    //         hero.transform.position = new Vector3(nPos.x,nPos.y,-1);
    //         hero.SetActive(true);
    //     }
    //     GameObject[] enemies = mainEnemiesUnit.Instance.getUnitsAsGameObject();
    //     foreach(GameObject enemy in enemies){
    //         enemy.transform.parent=null;
    //         // int rnd=Random.Range(gridMap.Count-1,gridMap.Count-9);
    //         int rnd = Random.Range(0,height-1);
    //         // gridMap[rnd].makeBusy();
    //         gridMapTiles[width-1,rnd].makeBusy();
    //         enemy.GetComponent<unitController>().setTile(gridMapTiles[width-1,rnd]);
    //         Vector3 nPos = gridMapTiles[width-1,rnd].transform.position;
    //         enemy.transform.position = new Vector3(nPos.x,nPos.y,-1);
    //         enemy.SetActive(true);
    //     }
    //     testMap = GridMap.getGridMap();
    //     Debug.Log($"test map len: {testMap.Length}");
    // }
}
