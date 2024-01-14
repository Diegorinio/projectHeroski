using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherGridManager : MonoBehaviour
{
[SerializeField]
    private int width, height;
    [SerializeField]
    private int widthSplit,heigthSplit;//10,6
    [SerializeField]
    private Tile _tilePreset;
    [SerializeField]
    private Transform _camera;
    [SerializeField] float offsetX, offsetY;
    [SerializeField] float spacer;
    [SerializeField] Tile[,] testMap;
    // Start is called before the first frame update
    List<Tile> gridMap = new List<Tile>();
    public static Tile[,] gridMapTiles;
    public GameObject spawnedUnit;
    void Start()
    {
        gridMapTiles=new Tile[width,height];
        offsetX = Screen.width / widthSplit;
        offsetY = Screen.height / heigthSplit;
        generateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateGrid()
    {
        float firstPosX = 0;
        float firstPosY = 0;
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var screenPoint = new Vector3(x+offsetX,y+offsetY,0);
                var worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
                //var spawnTile = Instantiate(_tilePreset, new Vector2(x + firstPosX, y + firstPosY), Quaternion.identity);
                var spawnTile = Instantiate(_tilePreset, new Vector2(worldPos.x+firstPosX,worldPos.y+firstPosY), Quaternion.identity);
                spawnTile.name = $"Tile{x}{y}";
                spawnTile.transform.SetParent(gameObject.transform);
                firstPosY += spacer;
                // gridMap.Add(spawnTile);
                gridMapTiles[x,y]=spawnTile;
                
            }
            firstPosY = 0;
            firstPosX += spacer;
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
        testMap = GridMap.getGridMap();
        Debug.Log($"test map len: {testMap.Length}");
    }
}
