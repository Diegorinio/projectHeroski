using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(characterController))]

//Klasa odpowiedzialna za wykrywanie Tile ruchu i wykrywanie przeciwnika
//enemyDetector dziedziczy
public class Detector : MonoBehaviour
{
    //Lista Tile po ktorych jednostka moze atakowac
    [SerializeField]
    protected List<Tile> movementTilesList;
    
    //Lista GameObject wykrytych przeciwnikow
    [SerializeField]
    protected List<GameObject> enemyUnitList;
    [SerializeField]
    //Przupisany kontroler jenostki, w tym wypadku Player
    protected unitController assignedController;

    //Znajdz Tile w zasiegu ruchu, pozniej znajdz Tile po ktorych moze sie poruszyc do movementTilesList i wykryj ewentualnych przeciwnikow
    // Wykrywa jednostki z tagiem Enemy
    public virtual void setTiles(){
        Tile _assignedTile = assignedController.getAssignedTile();
        List<Tile> rangeTiles = GridMap.calculateMapTiles(_assignedTile.getPosition(),assignedController.getUnitDistance());
        movementTilesList = GridMap.findMovementTiles(rangeTiles);
        enemyUnitList = GridMap.findGameObjectsOnTiles(rangeTiles,"Enemy");
        assignedController.addToTargets(enemyUnitList);
    }

    //Startuje wykrywanie
    public virtual void StartDetector(){
        assignedController = gameObject.GetComponent<unitController>();
        setTiles();
        GridMap.enableListTiles(movementTilesList);
        Debug.Log($"{assignedController.name} activated detector");
    }

    public List<Tile> getMovementTiles(){
        return movementTilesList;
    }

    //Usuwa efekty wykrywania, czysci liste wykrytych przeciwnikow i Tile ruchu
    public void StopDetector(){
        if(movementTilesList.Count>0){
            GridMap.disableListTiles(movementTilesList);
            movementTilesList.Clear();
            enemyUnitList.Clear();
            assignedController.clearTargets();
        }
    }
}
