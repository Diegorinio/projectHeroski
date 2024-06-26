using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected string spellName;
    protected Image spellImage;
    [SerializeField]
    protected SpellSO assignedSpellSO;
    protected GameObject selectedTarget;
    protected bool isTargeting;
    protected virtual List<Unit> getUnitsListByTag(string tag){
        switch(tag){
            case "Player":
            return mainEnemiesUnit.Instance.getUnitsList();
            case "Enemy":
            return mainPlayerUnit.Instance.getUnitsList();
            default:
            return new List<Unit>();
        }
    }
    public void assignSpellSO(SpellSO _spellSO){
        assignedSpellSO = _spellSO;
        spellName = _spellSO.spellName;
    }
    //castuj spell na jeden cel
    public abstract void castSpell(GameObject target);

    //castuj spell na wiele celow o okreslonym tagu
    public abstract void castSpellGlobal(string tag);

}
