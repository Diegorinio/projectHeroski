using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField]
    public List<Collider2D> detectedMColliders;
    [SerializeField]
    public List<Collider2D> detectedCharacterColliders;
    [SerializeField]
    public GameObject markedCharacter{get;set;}
    public characterController assignedCharacterController{get;set;}
    // Start is called before the first frame update
    void Awake()
    {
        assignedCharacterController = gameObject.GetComponentInParent<characterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("platform")){
            Tile tile = collider.gameObject.GetComponent<Tile>();
            tile.isActive=true;
            detectedMColliders.Add(collider);
        }
        else if(collider.gameObject.CompareTag("Enemy")){
            if(!assignedCharacterController.targetEnemy){
            assignedCharacterController.targetEnemy=collider.gameObject;
            markedCharacter=collider.gameObject;
            Camera.main.GetComponent<guiScript>().initializeGui();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.CompareTag("platform"))
        {
            //Debug.Log("o boze o kurwa");
            GameObject colliderPlatform = collider.gameObject;
            //colliderPlatform.GetComponent<SpriteRenderer>().color = Color.red;
            colliderPlatform.GetComponent<Tile>().isActive = false;
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            markedCharacter= null;

            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
    }
    private void OnDisable()
    {
        foreach(Collider2D coll in detectedMColliders){
            coll.GetComponent<SpriteRenderer>().color=Color.white;
            coll.GetComponent<Tile>().isActive=false;
        }
    }
}
