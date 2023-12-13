using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTileSecondDetector : MonoBehaviour
{
    //private List<Collider2D> colliders = new List<Collider2D>();
    public GameObject enemyMarked;
    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!gameObject.GetComponentInParent<characterController>().targetEnemy){
            Debug.Log($"second detector: {collision.gameObject.tag}");
            enemyMarked = collision.gameObject;
            gameObject.GetComponentInParent<characterController>().targetEnemy = collision.gameObject;
            Camera.main.GetComponent<guiScript>().initializeGui();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyMarked = null;

            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
    }

    public void Test(){
        Debug.Log($"test polacznia {this.name}");
    }

    public GameObject getEnemy(){
        return enemyMarked;
    }
    private void OnDisable()
    {
        //for (int x = 0; x < colliders.Count; x++)
        //{
        //    colliders[x].GetComponent<SpriteRenderer>().color = Color.white;
        //    colliders[x].GetComponent<Block>().isActive = false;
        //}
    }
}
