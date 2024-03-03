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
        List<Tile> rangeTiles = GridMap.calculateMapTiles(_assignedTile.getPosition(),assignedController.getUnitDistance());
        Debug.Log($"Enemy list tiles {rangeTiles.Count}");
        movementTilesList = GridMap.findMovementTiles(rangeTiles);
        Debug.Log($"set tiles enemy {movementTilesList.Count}");
        enemyUnitList = GridMap.findGameObjectsOnTiles(_assignedTile.getPosition(),assignedController.getUnitDistance(),"Player");
        assignedController.addToTargets(enemyUnitList);
    }

    //Startuje wykrywanie, wykryte Tile i GameObject przekazuje do AI a nastepnie wykonuje losowa akcje z AI
    public override void StartDetector(){
        assignedController = gameObject.GetComponent<unitController>();
        setTiles();
        GridMap.enableListTiles(movementTilesList);
        // base.StartDetector();
        aI=gameObject.GetComponent<enemyAI>();
        setTiles();
        aI.changeCollidersMovement(movementTilesList);
        aI.changeCollidersCharacters(enemyUnitList);
        aI.randomAction();
        Debug.Log($"Enemy end starting detector detector");
    }

    //Jezeli aktualna tura to gracz to po nacisnieciu na przeciwnika zadaj obrazenia
    public void OnMouseDown(){
        Debug.Log($"Enemy pressed");
        if(turnbaseScript.IsHeroTurn() && !battleManager.isSelectingTarget){
            Debug.Log($"Enemy pressed and set to attack");
            turnbaseScript.selectedGameObject.GetComponent<unitController>().playerHitSelectedTarget(gameObject);
        }
        else if(battleManager.isSelectingTarget){
            battleManager.selectedTargetForSpell = gameObject;
        }
    }
}
