using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//tile przeszkoda
public class obstacleTile : grassTile
{
    //Ilosc atakow do zniszczenia
    [SerializeField]
    private int obstacleHealth=2;
    //Czy Tile jest zniszczony
    private bool isDestroyed=false;
    protected override void setPreset()
    {
        setTilePreset("obstacleTile");
        isTaken=true;
    }
    //Nadpisz metode klikania do pierwsze zniszczenia a dopiero pozniej mozliwosc ruchu na ten Tile
    public override void OnMouseDown()
    {
        if(obstacleHealth>0){
            checkObstacleHealth();
        }
        else{
            isTaken=false;
            base.OnMouseDown();
        }
    }

    //Sprawdz stan przeszkody
    private void checkObstacleHealth(){
        int hp = obstacleHealth;
        if((hp-1)<=0 && !isDestroyed){
            isTaken=false;
            setTilePreset("normalTile",true);
            obstacleHealth--;
            isDestroyed=true;
        }
        else{
            obstacleHealth--;
        }

    }
}
