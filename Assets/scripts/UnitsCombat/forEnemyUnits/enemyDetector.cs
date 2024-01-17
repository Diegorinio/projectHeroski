using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyDetector : Detector{
    [SerializeField]
    private enemyAI aI;
    public override void Awake()
    {
        detectedCharacterColliders = new List<Collider2D>();
        detectedMColliders = new List<Collider2D>();
        aI = gameObject.GetComponentInParent<enemyAI>();
        assignedCharacterController=gameObject.GetComponentInParent<unitController>();
        assignedCharacterController.setTileDetector(gameObject);
    }
    void OnEnable(){
        if(!turnbaseScript.IsHeroTurn())
            StartCoroutine(waaitForColliders());
    }
    public override void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("platform")){
            Tile tile = collider.gameObject.GetComponent<Tile>();
            tile.isActive=true;
            detectedMColliders.Add(collider);
        }
        else if(collider.gameObject.CompareTag("Player")){
            // if(!assignedCharacterController.targetEnemy){
            // assignedCharacterController.targetEnemy=collider.gameObject;
            // markedCharacter=collider.gameObject;
            // Camera.main.GetComponent<guiScript>().initializeGui();
            detectedCharacterColliders.Add(collider);
            assignedCharacterController.addToTargets(collider.gameObject);
            // assignedCharacterController.addToTargets(collider.gameObject);
            // }
        }
        // Debug.Log($"Detected movement colliders : {detectedMColliders.Count}");
        detectedMColliders=detectedMColliders.GroupBy(c=>c.transform.name).Select(d=>d.First()).ToList();
        aI.changeCollidersMovement(detectedMColliders);
        aI.changeCollidersCharacters(detectedCharacterColliders);
        // Debug.Log($"detected characters: {detectedCharacterColliders.Count}");
    }

    public override void OnTriggerExit2D(Collider2D collider){
    }

    IEnumerator waaitForColliders(){
        Debug.Log("enemy detector start looking for colliders");
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"enemy detector stop looking for colliders, found {detectedMColliders.Count-1}");
        aI.randomAction();
    }
}
