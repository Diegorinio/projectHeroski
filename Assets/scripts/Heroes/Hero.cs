using System.Collections;
using UnityEngine;


public class Hero : MonoBehaviour
{
    private int heroID;
    private string heroName;
    private Sprite heroSprite;
    private Sprite[] spellIcons;
    [SerializeField]
    private string HeroTag;
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
        spellIcons=_hero.spellIcons;
        entryDialog=_hero.entryDialog;
        defeatDialog=_hero.defeatDialog;
        firstSpell = getSpellBySO(_hero.spellOne);
        secondSpell = getSpellBySO(_hero.spellTwo);
    }

    public string getHeroName(){
        return heroName;
    }
    public void castFirstSpell(){
        if(_heroSO.spellOne.getSpellRange()==SpellSO.spellRange.Target){
            // firstSpell.castSpell()
            battleManager.isSelectingTarget=true;
            StartCoroutine(waitUntilTargetIsSelected());

        }
        else if(_heroSO.spellOne.getSpellRange()==SpellSO.spellRange.Global){
            firstSpell.castSpellGlobal(getTargetTag());
        }
    }

    public void castSecondSpell(){
        if(_heroSO.spellTwo.getSpellRange()==SpellSO.spellRange.Target){
            battleManager.isSelectingTarget=true;
            StartCoroutine(waitUntilTargetIsSelected());

        }
        else if(_heroSO.spellTwo.getSpellRange()==SpellSO.spellRange.Global){
            secondSpell.castSpellGlobal(getTargetTag());
        }
    }
    public void thirdSpell(){
        Debug.Log($"3 spell");
    }

    public Sprite getHeroSprite(){
        return heroSprite;
    }
    public void setHeroTag(string tag){
        HeroTag = tag;
    }
    public string getHeroTag(){
        return HeroTag;
    }
    private string getTargetTag(){
        if(HeroTag=="Player")
            return "Enemy";
        else
            return "Player";
    }
    public heroSO getHeroSO(){
        return _heroSO;
    }

    private Spell getSpellBySO(SpellSO _spellSO){
        SpellSO.spellType _type = _spellSO.getSpellType();
        Spell addedComponent;
        switch(_type){
            case SpellSO.spellType.Damage:
            addedComponent = gameObject.AddComponent<DestructionSpell>();
            break;
            case SpellSO.spellType.Healing:
            addedComponent = gameObject.AddComponent<HealingSpell>();
            break;
            case SpellSO.spellType.Obstacle:
            addedComponent = gameObject.AddComponent<ObstacleSpell>();
            break;
            default:
            addedComponent = gameObject.AddComponent<DestructionSpell>();
            break;
        }
        // gameObject.GetComponent<Spell>().assignSpellSO(_spellSO);
        addedComponent.assignSpellSO(_spellSO);
        return addedComponent;
    }

    public Sprite[] getSpellImages(){
        return spellIcons;
    }

    private IEnumerator waitUntilTargetIsSelected(){
        yield return new WaitUntil(()=>battleManager.selectedTargetForSpell!=null);
        firstSpell.castSpell(battleManager.selectedTargetForSpell);
        battleManager.resetSpellTarget();
    }
}
