using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTileDetector : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    public GameObject enemyMarked;
    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!gameObject.GetComponentInParent<characterController>().targetEnemy)
            {
                enemyMarked = collision.gameObject;
                gameObject.GetComponentInParent<characterController>().targetEnemy = collision.gameObject;
                //gameObject.GetComponentInParent<enemyAI>().moveToRandomDirecion();
            }
        }
        else if (collision.gameObject.CompareTag("platform"))
        {
            GameObject colliderPlatform = collision.gameObject;
            colliderPlatform.GetComponent<Tile>().isActive = true;
            colliders.Add(collision);
        }
        // gameObject.GetComponentInParent<enemyAI>().changeColliders(colliders);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<characterController>())
        {
            enemyMarked = null;
            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
        else if (collision.gameObject.CompareTag("platform"))
        {
            GameObject colliderPlatform = collision.gameObject;
            colliderPlatform.GetComponent<Tile>().isActive = false;
        }
    }

    public GameObject getEnemy()
    {
        return enemyMarked;
    }
    private void OnDisable()
    {
        for (int x = 0; x < colliders.Count; x++)
        {
            colliders[x].GetComponent<SpriteRenderer>().color = Color.white;
            colliders[x].GetComponent<Tile>().isActive = false;
        }
    }

    public List<Collider2D> getColliders()
    {
        return colliders;
    }
}
