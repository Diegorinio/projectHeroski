using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDetector : Detector{
    // [SerializeField]
    // private List<Collider2D> detectedMColliders=new List<Collider2D>();
    // [SerializeField]
    // private List<Collider2D> detectedCharacterColliders = new List<Collider2D>();
    // [SerializeField]
    // private GameObject markedCharacter;
    // private characterController assignedCharacterController;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
    [SerializeField]
    private enemyAI aI;
    private void Start(){
        aI = gameObject.GetComponentInParent<enemyAI>();
    }
    public override void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("platform")){
            Tile tile = collider.gameObject.GetComponent<Tile>();
            tile.isActive=true;
            detectedMColliders.Add(collider);
        }
        else if(collider.gameObject.GetComponent<Hero>()){
            // if(!assignedCharacterController.targetEnemy){
            assignedCharacterController.targetEnemy=collider.gameObject;
            markedCharacter=collider.gameObject;
            Camera.main.GetComponent<guiScript>().initializeGui();
            detectedCharacterColliders.Add(collider);
            // }
        }
        // Debug.Log($"Detected movement colliders : {detectedMColliders.Count}");
        aI.changeCollidersMovement(detectedMColliders);
        aI.changeCollidersCharacters(detectedCharacterColliders);
    }

    public override void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.CompareTag("platform"))
        {
            //Debug.Log("o boze o kurwa");
            GameObject colliderPlatform = collider.gameObject;
            //colliderPlatform.GetComponent<SpriteRenderer>().color = Color.red;
            colliderPlatform.GetComponent<Tile>().isActive = false;
        }
        else if (collider.gameObject.GetComponent<Hero>())
        {
            markedCharacter= null;
            assignedCharacterController.targetEnemy=null;
            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
    }
}
