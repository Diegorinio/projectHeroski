using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Hero : MonoBehaviour
{
    private int heroID;
    private string heroName;
    private Sprite heroSprite;
    private Sprite spellIcon;
    private string entryDialog;
    private string defeatDialog;

    public void assignHeroSO(heroSO _hero){
        heroID=_hero.heroID;
        heroName=_hero.heroName;
        heroSprite=_hero.heroSprite;
        spellIcon=_hero.spellIcon;
        entryDialog=_hero.entryDialog;
        defeatDialog=_hero.defeatDialog;
    }
    public abstract void firstSpell();
    public abstract void secondSpell();
    public abstract void thirdSpell();
}
