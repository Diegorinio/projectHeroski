using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

//Detektor przeciwnika, to samo co Detector dla Gracza ale dziedziczy i zmienia kilka metod
// Przez to ze musi pozniej puscic wykryte Tile i GameObject do AI
public class enemyDetector : Detector{
    [SerializeField]

    //Komponent odpowiedzialny za AI
    private enemyAI aI;

    //To samo co w rodzicu ale wykrywa z tagiem jednostek Player
    public override void setTiles(){
        Tile _assignedTile = assignedController.getAssignedTile();
        List<Tile> rangeTiles = GridMap.calculateMapTiles(_assignedTile.getPosition(),assignedController.getAssignedUnit().gridDistanceX);
        movementTilesList = GridMap.findMovementTiles(rangeTiles);
        enemyUnitList = GridMap.findGameObjectsOnTiles(rangeTiles,"Player");
        assignedController.addToTargets(enemyUnitList);
    }

    //Startuje wykrywanie, wykryte Tile i GameObject przekazuje do AI a nastepnie wykonuje losowa akcje z AI
    public override void StartDetector(){
        base.StartDetector();
        aI=gameObject.GetComponent<enemyAI>();
        aI.changeCollidersMovement(movementTilesList);
        aI.changeCollidersCharacters(enemyUnitList);
        aI.randomAction();
    }

    //Jezeli aktualna tura to gracz to po nacisnieciu na przeciwnika zadaj obrazenia
    public void OnMouseDown(){
        if(turnbaseScript.IsHeroTurn()){
            turnbaseScript.selectedGameObject.GetComponent<unitController>().hitToSelectedTarget(gameObject);
        }
    }
}
