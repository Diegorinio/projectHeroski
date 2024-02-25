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

    public override void castSpellGlobal(string tag)
    {
        switch(assignedSpellSO.DamageType){
            case SpellSO.damageType.Fixed:
            FixedSpellDamage(assignedSpellSO.spellCastFixed,tag);
            break;
            case SpellSO.damageType.Percentage:
            PercentagetSpellDamage(assignedSpellSO.spellCastPercent,tag);
            break;
        }
    }

    private void FixedSpellDamage(GameObject target,int dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }
    private void FixedSpellDamage(int dmg, string tag){
        List<Unit> unitList = new List<Unit>();
        switch(tag){
            case "Player":
            unitList = mainPlayerUnit.Instance.getUnitsList();
            break;
            case "Enemy":
            unitList = mainEnemiesUnit.Instance.getUnitsList();
            break;
        }
        foreach(var u in unitList){
            u.getHit(dmg);
        }
    }
    private void PercentagetSpellDamage(GameObject target,float dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }
    private void PercentagetSpellDamage(float dmg,string tag){
        List<Unit> unitList = new List<Unit>();
        switch(tag){
            case "Player":
            unitList = mainPlayerUnit.Instance.getUnitsList();
            break;
            case "Enemy":
            unitList = mainEnemiesUnit.Instance.getUnitsList();
            break;
        }
        foreach(var u in unitList){
            u.getHit(dmg);
        }
    }


}
