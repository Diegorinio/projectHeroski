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
                else{
                    newTile.AddComponent<grassTile>();
                }
                newTile.transform.SetParent(gameObject.transform);
                newTile.name = $"Tile{x}{y}";
                RectTransform rectTransform = newTile.GetComponent<RectTransform>();
                rectTransform.localScale= new Vector3(100,100,1);
                newTile.GetComponent<Tile>().setPosition(x,y);
                gridMapTiles[x,y]=newTile.GetComponent<Tile>();
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
            // hero.SetActive(true);
        }
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
            enemy.SetActive(true);
        }

    }
}
