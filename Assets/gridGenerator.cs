using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridGenerator : MonoBehaviour
{
    public GameObject block;
    public GameObject blockRef;
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    List<GameObject> gridMap = new List<GameObject>();
    void Start()
    {
        Vector3 blockPosition = blockRef.transform.position;
        float firstPos = blockPosition.x;
        for(int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                var generatedTile=Instantiate(block, new Vector2(blockPosition.x+1.2f, blockPosition.y), Quaternion.identity);
                blockPosition = new Vector2(blockPosition.x + 1.2f, blockPosition.y);
            }
            blockPosition = new Vector3(firstPos, blockPosition.y-1.2f);
        }
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject h in heroes)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
