using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isActive = false;
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
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (playerCharacter.isCharacter && isActive)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 gObj = gameObject.transform.position;
            player.transform.position = new Vector3(gObj.x, gObj.y, gObj.z - 2);
            player.GetComponent<playerCharacter>().disableClickable();
        }
        else
        {
            Debug.Log("NIE");
        }
    }
}
