using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTileDetector : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
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
