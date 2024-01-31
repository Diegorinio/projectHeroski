using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public string Name;
    public Sprite heroSprite;

    public abstract void Skill();
}