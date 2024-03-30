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
    // public static bool isSelectingTarget;
    // public static GameObject selectedTargetForSpell;
    public Image playerHeroSprite;
    public TextMeshProUGUI playerHeroNameText;
    // public Button[] spellButtons;

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
        PlayerHeroBehaviour.Instance.Initialize();
    }

    private void SetHeroForEnemy(){
        Hero _assignedEnemyHero = mainEnemiesUnit.Instance.getSelectedHero();
        enemyHeroSprite.sprite = _assignedEnemyHero.getHeroSprite();
        enemyHeroNameText.text = _assignedEnemyHero.getHeroName();
    }
}
