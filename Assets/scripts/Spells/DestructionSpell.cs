using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionSpell : Spell
{

    //castuj spell na wygraną jednostkę
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

    //castuj spell na wszystkie jednostki z tagiem
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

    //metoda do zadawania okreslonych obrazen na jednostke
    private void FixedSpellDamage(GameObject target,int dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }
    //metoda do zadawania okresloneych obrazen wszystkim jednostkom z tagiem
    private void FixedSpellDamage(int dmg, string tag){
        List<Unit> unitList = getUnitsListByTag(tag);
        foreach(var u in unitList){
            u.getHit(dmg);
        }
    }

    //procentowy damage jednostce
    private void PercentagetSpellDamage(GameObject target,float dmg){
        target.GetComponent<Unit>().getHit(dmg);
    }

    //procentowy damage wszystkim jednostek
    private void PercentagetSpellDamage(float dmg,string tag){
        List<Unit> unitList = getUnitsListByTag(tag);
        foreach(var u in unitList){
            u.getHit(dmg);
        }
    }


}
