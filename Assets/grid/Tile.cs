using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{

    [SerializeField]
    private int posX,posY;
    public bool isActive = false;
    [SerializeField]
    bool isTaken = false;
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render=gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            render.color = Color.green;
        }
        else if(!isActive||isTaken)
        {
            render.color = Color.grey;
        }
    }

    public int[] getPosition(){
        int[] pos = {posX,posY};
        return pos;
    }
    public void setPosition(int x,int y){
        posX=x;
        posY=y;
    }
    private void OnMouseDown()
    {
        Debug.Log($"Pressed {name}");
        if (turnbaseScript.isSelected && isActive && !isTaken)
        {
            GameObject player = turnbaseScript.selectedGameObject;
            player.GetComponent<unitController>().characterMove(gameObject);
            isActive = false;
        }
        else
        {
            Debug.Log("NIE");
        }
        int[] pos = {4,4};
        GridMap.calculateMapTiles(pos,1);
    }

    public bool isBusy()
    {
        return isTaken;
    }
    public void makeBusy()
    {
        isTaken=true;
    }
    public void unMakeBusy(){
        isTaken=false;
    }
}
