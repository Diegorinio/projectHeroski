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
        if (turnbaseScript.isSelected && isActive)
        {
            GameObject player = turnbaseScript.selectedGameObject;
            // Vector3 gObj = gameObject.transform.position;
            // player.transform.position = new Vector3(gObj.x, gObj.y, player.transform.position.z);
            player.GetComponent<characterController>().characterMove(gameObject.transform);
            isActive = false;
            // isTaken = true;
            // player.GetComponent<characterController>().disableClickable();
        }
        else
        {
            Debug.Log("NIE");
        }
    }
    private void OnTriggerEnter2D(Collider2D obj){
        if(obj.transform.CompareTag("Player")||obj.transform.CompareTag("Enemy")){
            isTaken=true;
            Debug.Log($"TILE: {obj.gameObject.transform.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D obj){
        if(obj.transform.CompareTag("Player")||obj.transform.CompareTag("Enemy")){
            isTaken=false;
            Debug.Log($"TILE detect leave: {obj.gameObject.transform.name}");
        }
    }

    public bool isBusy()
    {
        return isTaken;
    }
    public void MakeBusy()
    {
        isTaken = !isTaken;
    }
}
