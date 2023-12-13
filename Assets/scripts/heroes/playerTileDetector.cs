using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTileDetector : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField]
    private GameObject enemyMarked;
    void Start()
    {
        
    }
    void Update()
    {
        
    }


        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            //Debug.Log("o boze o kurwa");
            GameObject colliderPlatform = collision.gameObject;
            //colliderPlatform.GetComponent<SpriteRenderer>().color = Color.red;
            Tile tile = colliderPlatform.GetComponent<Tile>();
            //if (!tile.isBusy()) {
            //    tile.isActive = true;
            //    colliders.Add(collision);
            //}
            tile.isActive = true;
            colliders.Add(collision);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!gameObject.GetComponentInParent<characterController>().targetEnemy)
            {
                Debug.Log($"second detector: {collision.gameObject.tag}");
                enemyMarked = collision.gameObject;
                gameObject.GetComponentInParent<characterController>().targetEnemy = collision.gameObject;
                Camera.main.GetComponent<guiScript>().initializeGui();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            //Debug.Log("o boze o kurwa");
            GameObject colliderPlatform = collision.gameObject;
            //colliderPlatform.GetComponent<SpriteRenderer>().color = Color.red;
            colliderPlatform.GetComponent<Tile>().isActive = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyMarked = null;

            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
    }

    private void OnDisable()
    {
        for(int x = 0; x < colliders.Count; x++)
        {
            colliders[x].GetComponent<SpriteRenderer>().color = Color.white;
            colliders[x].GetComponent<Tile>().isActive = false;
            colliders[x].GetComponent<Tile>().MakeBusy();
        }
    }
}
