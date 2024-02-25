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
    private heroSO _heroSO;
    private Spell firstSpell;
    private Spell secondSpell;

    public void assignHeroSO(heroSO _hero){
        _heroSO = _hero;
        heroID=_hero.heroID;
        heroName=_hero.heroName;
        heroSprite=_hero.heroSprite;
        spellIcon=_hero.spellIcon;
        entryDialog=_hero.entryDialog;
        defeatDialog=_hero.defeatDialog;
        firstSpell = getSpellBySO(_hero.spellOne);
        secondSpell = getSpellBySO(_hero.spellTwo);
    }

    public string getHeroName(){
        return heroName;
    }
    public void castFirstSpell(){
    }
    public void castSecondSpell(){

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

    private Spell getSpellBySO(SpellSO _spellSO){
        SpellSO.spellType _type = _spellSO.getSpellType();
        switch(_type){
            case SpellSO.spellType.Damage:
            gameObject.AddComponent<DestructionSpell>();
            break;
            case SpellSO.spellType.Healing:
            gameObject.AddComponent<DestructionSpell>();
            break;
            case SpellSO.spellType.Obstacle:
            gameObject.AddComponent<DestructionSpell>();
            break;
            default:
            gameObject.AddComponent<DestructionSpell>();
            break;
        }
        return gameObject.GetComponent<Spell>();
    }
}
