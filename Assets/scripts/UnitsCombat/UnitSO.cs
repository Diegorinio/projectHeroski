using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[CreateAssetMenu(fileName="New unit", menuName ="Units/ new Unit")]
public class UnitSO :ScriptableObject
{
    public string unitName;
    public Sprite unitSprite;
    public enum tiers{t1,t2,t3,t4};
    public int unitType;
    public tiers tier;
    public int unitBaseHealth;
    public int unitBaseDamage;

    public int gridDistanceX,gridDistanceY;

    public Sprite[] attackAnimationSprites;

    public Sprite[] getAttackSprites(){
        return attackAnimationSprites;
    }
}
