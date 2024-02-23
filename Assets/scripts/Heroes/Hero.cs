using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : MonoBehaviour
{
    private int heroID;
    private string heroName;
    private Sprite heroSprite;
    private Sprite spellIcon;
    private string entryDialog;
    private string defeatDialog;
    private Object[] spellsList;
    private heroSO _heroSO;

    public void assignHeroSO(heroSO _hero){
        _heroSO = _hero;
        heroID=_hero.heroID;
        heroName=_hero.heroName;
        heroSprite=_hero.heroSprite;
        spellIcon=_hero.spellIcon;
        entryDialog=_hero.entryDialog;
        defeatDialog=_hero.defeatDialog;
    }

    public string getHeroName(){
        return heroName;
    }
    public void firstSpell(){

    }
    public void secondSpell(){

    }
    public void thirdSpell(){
        Debug.Log($"3 spell");
    }

    public Sprite getHeroSprite(){
        return heroSprite;
    }
    public heroSO getHeroSO(){
        return _heroSO;
    }
}
