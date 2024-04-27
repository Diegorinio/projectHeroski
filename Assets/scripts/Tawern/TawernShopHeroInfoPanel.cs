using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TawernShopHeroInfoPanel : MonoBehaviour
{
    public Image HeroImageSprite;
    public TextMeshProUGUI HeroNameText;
    public TextMeshProUGUI HeroPriceText;
    public Image[] spellIcons;
    public Button HireButton;
    public Button BuyButton;
    heroSO _HeroSO;

    public TawernHeroesShop shop;
    public void SetUpTawerShopHero(heroSO _hero){
        _HeroSO = _hero;
        HeroImageSprite.sprite = _hero.heroSprite;
        HeroNameText.text = _hero.heroName;
        spellIcons[0].sprite = _hero.spellIcons[0];
        spellIcons[1].sprite = _hero.spellIcons[1];
        HeroPriceText.text = _hero.heroPrice.ToString();
        HireButton.onClick.AddListener(()=>{HireHeroToTeam(_HeroSO);});
    }
    private void HireHeroToTeam(heroSO hero){
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(hero.heroID,heroSpawner.HeroController.Player);
        Hero _heroInTawern = spawnedHero.GetComponent<Hero>();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
        shop.LoadAgain();
    }
}