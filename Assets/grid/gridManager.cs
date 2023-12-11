using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class gridManager : MonoBehaviour
{
    [SerializeField]
    private int width, height;
    [SerializeField]
    private Tile _tilePreset;
    [SerializeField]
    private Transform _camera;
    [SerializeField] float offsetX, offsetY;
    [SerializeField] float spacer;
    // Start is called before the first frame update
    List<Tile> gridMap = new List<Tile>();
    void Start()
    {
        offsetX = Screen.width / 10;
        offsetY = Screen.height / 6;
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
                gridMap.Add(spawnTile);
            }
            firstPosY = 0;
            firstPosX += spacer;
        }
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject hero in heroes)
        {
            hero.transform.position = gridMap[Random.Range(0,8)].transform.position;
            hero.transform.tag = "Untagged";
        }
        Debug.Log("ttt");
        //_camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f,-10);
    }
}
