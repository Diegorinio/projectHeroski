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
    public override void Awake()
    {
        aI = gameObject.GetComponentInParent<enemyAI>();
        assignedCharacterController=gameObject.GetComponentInParent<characterController>();
    }
    void OnEnable(){
        StartCoroutine(waaitForColliders());
    }
    public override void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("platform")){
            Tile tile = collider.gameObject.GetComponent<Tile>();
            tile.isActive=true;
            detectedMColliders.Add(collider);
        }
        else if(collider.gameObject.GetComponent<Hero>()){
            // if(!assignedCharacterController.targetEnemy){
            // assignedCharacterController.targetEnemy=collider.gameObject;
            // markedCharacter=collider.gameObject;
            // Camera.main.GetComponent<guiScript>().initializeGui();
            // detectedCharacterColliders.Add(collider);
            assignedCharacterController.addToTargets(collider.gameObject);
            // }
        }
        // Debug.Log($"Detected movement colliders : {detectedMColliders.Count}");
        aI.changeCollidersMovement(detectedMColliders);
        aI.changeCollidersCharacters(assignedCharacterController.targets);
        // Debug.Log($"detected characters: {detectedCharacterColliders.Count}");
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
            // markedCharacter= null;
            // assignedCharacterController.targetEnemy=null;
            // gameObject.GetComponentInParent<characterController>().targetEnemy = null;
            detectedCharacterColliders=new List<Collider2D>();
            detectedCharacterColliders=new List<Collider2D>();
            assignedCharacterController.clearTargets();
            aI.resetColliders();
        }
        aI.resetColliders();
    }

    IEnumerator waaitForColliders(){
        Debug.Log("enemy detector start looking for colliders");
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"enemy detector stop looking for colliders, found {detectedMColliders.Count-1}");
        aI.randomAction();
    }
}
