using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class battleManager: MonoBehaviour
{

    //Dla Gracza
    // public static GameObject battleAnimPanel;
    public static bool isSelectingTarget;
    public static GameObject selectedTargetForSpell;
    public Image playerHeroSprite;
    public TextMeshProUGUI playerHeroNameText;
    public Button[] spellButtons;

    public static GameObject battleAnimPanel;
    public GameObject _battleAnimPanel;


    //Dla przeciwnika
    public Image enemyHeroSprite;
    public TextMeshProUGUI enemyHeroNameText;
    // Start is called before the first frame update
    void Start()
    {
        battleAnimPanel = _battleAnimPanel;
        SetHeroForPlayer();
        SetHeroForEnemy();
    }

    private void SetHeroForPlayer(){
        Hero _assignedPlayerHero = mainPlayerUnit.Instance.getSelectedHero();
        playerHeroSprite.sprite = _assignedPlayerHero.getHeroSprite();
        playerHeroNameText.text = _assignedPlayerHero.getHeroName();
        Sprite[] spellIcons = _assignedPlayerHero.getSpellImages();
        spellButtons[0].GetComponent<Image>().sprite = spellIcons[0];
        spellButtons[0].onClick.AddListener(()=>{spellButtonEnable(0,false);_assignedPlayerHero.castFirstSpell();});
        
        spellButtons[1].GetComponent<Image>().sprite = spellIcons[1];
        spellButtons[1].onClick.AddListener(()=>{spellButtonEnable(1,false);_assignedPlayerHero.castSecondSpell();});
    }

    private void SetHeroForEnemy(){
        Hero _assignedEnemyHero = mainEnemiesUnit.Instance.getSelectedHero();
        enemyHeroSprite.sprite = _assignedEnemyHero.getHeroSprite();
        enemyHeroNameText.text = _assignedEnemyHero.getHeroName();
    }

    public void spellButtonsEnable(bool state){
        spellButtons[0].enabled = state;
        spellButtons[1].enabled = state;
    }

    private void spellButtonEnable(int id ,bool state){
        spellButtons[id].enabled=state;
    }

    public static void setTargetForSpell(GameObject target){
        selectedTargetForSpell = target;
    }
    public static void resetSpellTarget(){
        selectedTargetForSpell=null;
        isSelectingTarget=false;
    }


}
