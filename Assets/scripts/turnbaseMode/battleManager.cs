using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class battleManager: MonoBehaviour
{
    
    public Image playerSpriteTmp;
    public TextMeshProUGUI playerNameTmp;
    public Image[] spellIcons;
    // Start is called before the first frame update
    void Start()
    {
        Hero _assignedPlayerHero = mainPlayerUnit.Instance.getSelectedHero();
        playerSpriteTmp.sprite = _assignedPlayerHero.getHeroSprite();
        playerNameTmp.text = _assignedPlayerHero.getHeroName();
    }
}
