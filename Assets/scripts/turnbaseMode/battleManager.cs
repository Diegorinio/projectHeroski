using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class battleManager: MonoBehaviour
{
    
    public Image playerSpriteTmp;
    public TextMeshProUGUI playerNameTmp;
    public Button[] spellButtons;
    // Start is called before the first frame update
    void Start()
    {
        Hero _assignedPlayerHero = mainPlayerUnit.Instance.getSelectedHero();
        playerSpriteTmp.sprite = _assignedPlayerHero.getHeroSprite();
        playerNameTmp.text = _assignedPlayerHero.getHeroName();
        Sprite[] spellIcons = _assignedPlayerHero.getSpellImages();
        spellButtons[0].GetComponent<Image>().sprite = spellIcons[0];
        spellButtons[0].onClick.AddListener(_assignedPlayerHero.castFirstSpell);
        spellButtons[1].GetComponent<Image>().sprite = spellIcons[1];
        spellButtons[1].onClick.AddListener(_assignedPlayerHero.castSecondSpell);
    }
}
