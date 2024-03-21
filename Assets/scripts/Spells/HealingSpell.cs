using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class HealingSpell : Spell
{
    public override void castSpell(GameObject target)
    {
        switch(assignedSpellSO.DamageType){
            case SpellSO.damageType.Fixed:
            FixedSpellHeal(target,assignedSpellSO.spellCastFixed);
            break;
            case SpellSO.damageType.Percentage:
            PercentagetSpellHeal(target,assignedSpellSO.spellCastPercent);
            break;
        }
    }

    public override void castSpellGlobal(string tag)
    {
        switch(assignedSpellSO.DamageType){
            case SpellSO.damageType.Fixed:
            FixedSpellHeal(assignedSpellSO.spellCastFixed,tag);
            break;
            case SpellSO.damageType.Percentage:
            PercentagetSpellHeal(assignedSpellSO.spellCastPercent,tag);
            break;
        }
    }

    private void FixedSpellHeal(GameObject target,int heal){
        target.GetComponent<Unit>().healUnit(heal);
    }

    private void FixedSpellHeal(int heal, string tag){
        List<Unit> unitList = getUnitsListByTag(tag);
        foreach(var u in unitList){
            u.healUnit(heal);
        }
    }
    private void PercentagetSpellHeal(GameObject target,float heal){
        target.GetComponent<Unit>().healUnit(heal);
    }
    private void PercentagetSpellHeal(float heal,string tag){
        List<Unit> unitList = getUnitsListByTag(tag);
        foreach(var u in unitList){
            u.healUnit(heal);
        }
    }

}
