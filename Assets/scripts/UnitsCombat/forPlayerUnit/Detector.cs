using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(characterController))]
public class Detector : MonoBehaviour
{
    [SerializeField]
    public List<Tile> detectedMColliders;
    [SerializeField]
    public List<Collider2D> detectedCharacterColliders;
    [SerializeField]
    // public GameObject markedCharacter{get;set;}
    public unitController assignedCharacterController;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        detectedMColliders = new List<Tile>();
        detectedCharacterColliders = new List<Collider2D>();
        assignedCharacterController = gameObject.GetComponentInParent<unitController>();
        assignedCharacterController.setTileDetector(gameObject);
    }

    public void Start(){
        Tile _assignedtile = assignedCharacterController.getAssignedTile();
        List<Tile> _tiles = new List<Tile>();
        Vector2Int aTile = _assignedtile.getPosition();
        detectedMColliders = GridMap.calculateMapTiles(_assignedtile.getPosition(),assignedCharacterController.getAssignedUnit().gridDistanceX);
        Debug.Log($"Znaleziono {_tiles.Count}");
        foreach(var t in detectedMColliders){
            t.isActive=true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public virtual void OnTriggerEnter2D(Collider2D collider){
    //     if(collider.gameObject.CompareTag("platform")){
    //         Tile tile = collider.gameObject.GetComponent<Tile>();
    //         if(!tile.isBusy())
    //             tile.isActive=true;
    //         detectedMColliders.Add(collider);
    //     }
    //     else if(collider.gameObject.CompareTag("Enemy")){
    //         // if(!assignedCharacterController.targetEnemy){
    //         // assignedCharacterController.targetEnemy=collider.gameObject;
    //         assignedCharacterController.addToTargets(collider.gameObject);
    //         // markedCharacter=collider.gameObject;
    //     }
    // }

    // public virtual void OnTriggerExit2D(Collider2D collider){
    //     if (collider.gameObject.CompareTag("platform"))
    //     {
    //         //Debug.Log("o boze o kurwa");
    //         GameObject colliderPlatform = collider.gameObject;
    //         //colliderPlatform.GetComponent<SpriteRenderer>().color = Color.red;
    //         colliderPlatform.GetComponent<Tile>().isActive = false;
    //     }
    //     else if (collider.gameObject.CompareTag("Enemy"))
    //     {
    //         // markedCharacter= null;
    //         // assignedCharacterController.targetEnemy=null;
    //         assignedCharacterController.clearTargets();
    //     }
    // }
    private void OnDisable()
    {
        //tutaj wyskakuje blad, nie wiem co jest ale cos jest. Ale bez tego nie zmeinia koloru wiec nie wiem 
        if(detectedMColliders.Count>0){
        foreach(Tile t in detectedMColliders){
            // coll.GetComponent<SpriteRenderer>().color=Color.white;
            t.isActive=false;
        }
        }
        clearDetectorColliders();
    }

    private void clearDetectorColliders(){
        detectedMColliders=new List<Tile>();
        detectedCharacterColliders=new List<Collider2D>();
    }
}
