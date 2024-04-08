using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu(fileName="New Spell", menuName ="Spells/New Spell")]
public class SpellSO : ScriptableObject
{
    public enum spellType{Damage,Healing,Obstacle}
    public enum damageType{none,Fixed,Percentage}
    public enum healType{none,Fixed,Percentage}
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

    public string getSpellName(){
        return spellName;
    }

    public spell getSpellInfo(){
        var spellInfo = new spell();
            spellInfo.spellName = spellName;
            spellInfo.spellType = SpellType.ToString();
            spellInfo.spellRange = SpellRange.ToString();
            spellInfo.castType = getCastType();
            spellInfo.amountText = getAmountText();
        return spellInfo;
    }
    private string getAmountText(){
        if(SpellType==spellType.Damage){
            if(DamageType==damageType.Fixed){
                return $"Damage: {spellCastFixed}";
            }
            else{
                return $"Damage: {spellCastPercent}";
            }
        }
        else{
            if(HealType==healType.Fixed){
                return $"Heal: {spellCastFixed}";
            }
            else{
                return $"Heal: {spellCastPercent}";
            }
        }
    }
    private string getCastType(){
        if(DamageType!=damageType.none){
            return DamageType.ToString();
        }
        else{
            return HealType.ToString();
        }
    }
}
public struct spell{
    // public spell(string s_name, string s_type,string s_range,string s_c_type,string s_amountTxt){
    //     spellName = s_name;
    //     spellType = s_type;
    //     spellRange = s_range;
    //     castType = s_c_type;
    //     amountText = s_amountTxt;
    // }
    public string spellName{get;set;}
    public string spellType{get;set;}
    public string spellRange{get;set;}
    public string castType{get;set;}
    public string amountText{get;set;}
}
