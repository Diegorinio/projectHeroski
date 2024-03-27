using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeroBehaviour : MonoBehaviour
{
    public static PlayerHeroBehaviour Instance;
    public  bool isSelectingTarget;
    public  GameObject selectedTargetForSpell;
    public Button[] spellButtons;

    Hero _assignedPlayerHero;

    void Awake(){
        if(Instance==null){
            Instance = this;
        }
    }


    public void Initialize(){
        _assignedPlayerHero = mainPlayerUnit.Instance.getSelectedHero();
        Sprite[] spellIcons = _assignedPlayerHero.getSpellImages();
        spellButtons[0].GetComponent<Image>().sprite = spellIcons[0];
        spellButtons[0].onClick.AddListener(()=>{
            spellButtonEnable(0,false);
            _assignedPlayerHero.castFirstSpell();
            });
        
        spellButtons[1].GetComponent<Image>().sprite = spellIcons[1];
        spellButtons[1].onClick.AddListener(()=>{
            HeroEventsManager.BoxEventComplete += ()=>{Debug.Log($"Spell used");};
            StartCoroutine(GetComponent<HeroEventsManager>().createBoxEvent(spellIcons[1]));
            spellButtonEnable(1,false);
            _assignedPlayerHero.castSecondSpell();
            });
    }

    public void spellButtonsEnable(bool state){
        spellButtons[0].enabled = state;
        spellButtons[1].enabled = state;
    }

    private void spellButtonEnable(int id ,bool state){
        spellButtons[id].enabled=state;
    }

    public void setTargetForSpell(GameObject target){
        selectedTargetForSpell = target;
    }
    public void resetSpellTarget(){
        selectedTargetForSpell=null;
        isSelectingTarget=false;
    }
}
