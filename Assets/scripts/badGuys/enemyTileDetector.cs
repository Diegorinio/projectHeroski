using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTileDetector : MonoBehaviour
{

    public GameObject enemyMarked;
    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<characterController>())
        {
            if (!gameObject.GetComponentInParent<characterController>().targetEnemy)
            {
                //Debug.Log($"second detector: {collision.gameObject.tag}");
                enemyMarked = collision.gameObject;
                gameObject.GetComponentInParent<characterController>().targetEnemy = collision.gameObject;
                gameObject.GetComponentInParent<enemyAI>().moveToRandomDirecion();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<characterController>())
        {
            enemyMarked = null;
            gameObject.GetComponentInParent<characterController>().targetEnemy = null;
        }
    }

    public GameObject getEnemy()
    {
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
