using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu(fileName="New Spell", menuName ="Spells/New Spell")]
public class SpellSO : ScriptableObject
{
    public enum spellType{Damage,Healing,Obstacle}
    public enum damageType{Fixed,Percentage}
    public enum healType{Fixed,Percentage}
    public enum spellRange{Target,Global}
    public string spellName;
    public spellType SpellType;
    public spellRange SpellRange;
    public damageType DamageType;
    public float spellCastPercent;
    public int spellCastFixed;
    public healType HealType;

    public spellType getSpellType(){
        return SpellType;
    }
    public damageType getDamageType(){
        return DamageType;
    }
    public healType getHealType(){
        return HealType;
    }
    public spellRange getSpellRange(){
        return SpellRange;
    }

}
