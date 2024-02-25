using System.Collections;
using System.Collections.Generic;
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
    private void FixedSpellHeal(GameObject target,int heal){
        target.GetComponent<Unit>().healUnit(heal);
    }
    private void PercentagetSpellHeal(GameObject target,float heal){
        target.GetComponent<Unit>().healUnit(heal);
    }

}
