using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSO : ScriptableObject
{
    public string tileName;
    public Sprite[] tileSprites;

    public Sprite getRandomSprite(){
        return tileSprites[Random.Range(0,tileSprites.Length)];
    }
}
