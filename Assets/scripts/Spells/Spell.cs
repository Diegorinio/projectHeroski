using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected string spellName;
    protected Image spellImage;
    protected SpellSO assignedSpellSO;

    public void assignSpellSO(SpellSO _spellSO){
        spellName = _spellSO.spellName;
        spellImage = _spellSO.spellImage;
    }
    public abstract void castSpell(GameObject target);
    public abstract void castSpellGlobal();
}
