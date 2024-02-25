using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionSpell : Spell
{

    public override void castSpell(GameObject target)
    {
        switch(assignedSpellSO.DamageType){
            case SpellSO.damageType.Fixed:
            FixedSpellDamage(target,assignedSpellSO.spellCastFixed);
            break;
            case SpellSO.damageType.Percentage:
            PercentagetSpellDamage(target,assignedSpellSO.spellCastPercent);
            break;
        }
    }
    private void FixedSpellDamage(GameObject target,int dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }
    private void PercentagetSpellDamage(GameObject target,float dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }

}
