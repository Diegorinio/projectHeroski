using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    private string spellName;
    private Sprite spellImage;
    public abstract void castSpell();
}
