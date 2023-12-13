using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTileMovement : MonoBehaviour
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
            colliderPlatform.GetComponent<Tile>().isActive = true;
                colliders.Add(collision);
        }
        gameObject.GetComponentInParent<enemyAI>().changeColliders(colliders);
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
