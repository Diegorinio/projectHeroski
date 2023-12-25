using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField]
    bool isTaken = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(!isActive||isTaken)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (turnbaseScript.isSelected && isActive && !isTaken)
        {
            GameObject player = turnbaseScript.selectedGameObject;
            // Vector3 gObj = gameObject.transform.position;
            // player.transform.position = new Vector3(gObj.x, gObj.y, player.transform.position.z);
            player.GetComponent<characterController>().characterMove(gameObject);
            isActive = false;
            // isTaken=true;
            // isTaken = true;
            // player.GetComponent<characterController>().disableClickable();
        }
        else
        {
            Debug.Log("NIE");
        }
    }
    // private void OnTriggerEnter2D(Collider2D obj){
    //     // Debug.Log($"TILE detected: {obj.gameObject.transform.name}");
    //     if(obj.transform.CompareTag("Player")||obj.transform.CompareTag("Enemy")){
    //         isTaken=true;
    //         Debug.Log($"TILE: {obj.gameObject.transform.name}");
    //     }
    // }
    // private void OnTriggerStay2D(Collider2D coll){
    //     if(coll.CompareTag("Player")){
    //         Debug.Log("TILE PLAYER DETECTED");
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D obj){
    //     if(obj.transform.CompareTag("Player")||obj.transform.CompareTag("Enemy")){
    //         isTaken=false;
    //         Debug.Log($"TILE detect leave: {obj.gameObject.transform.name}");
    //     }
    // }

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
