using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TawernShopHeroInfoPanel : MonoBehaviour
{
    [SerializeField]
    public GameObject infoPanel;
    public Image HeroImageSprite;
    public TextMeshProUGUI HeroNameText;
    public TextMeshProUGUI HeroPriceText;
    public Image[] spellIcons;
    public Button HireButton;
    public Button BuyButton;
    heroSO _HeroSO;

    public TawernHeroesShop shop;
    private void OnEnable()
    {
        HeroImageSprite.enabled = false;
        HeroNameText.enabled = false;
        HeroPriceText.enabled = false;
        if(mainPlayerUnit.Instance.getSelectedHero()!=null){
            heroSO _so = mainPlayerUnit.Instance.getSelectedHero().getHeroSO();
            SetUpTawerShopHero(_so);
            HireButton.enabled=false;
            Debug.Log("JEST KUPIONY");
            infoPanel.SetActive(true);
        }
        else{
            Debug.Log("NIE JEST KUPIONY");
            infoPanel.SetActive(false);
        }
    }

    void Start(){
    }
    public void SetUpTawerShopHero(heroSO _hero){
        HeroImageSprite.enabled=true;
        HeroNameText.enabled=true;
        HeroPriceText.enabled=true;
        _HeroSO = _hero;
        HeroImageSprite.sprite = _hero.heroSprite;
        HeroNameText.text = _hero.heroName;
        spellIcons[0].sprite = _hero.spellIcons[0];
        spellIcons[1].sprite = _hero.spellIcons[1];
        HeroPriceText.text = _hero.heroPrice.ToString();
        HireButton.onClick.AddListener(()=>{HireHeroToTeam(_HeroSO);});
        if(!isHeroAlreadyHired(_hero))
            HireButton.enabled=true;
        infoPanel.SetActive(true);
    }

    private bool isHeroAlreadyHired(heroSO newHeroSO){
        if(mainPlayerUnit.Instance.isHeroAssigned()){
        int hiredID = mainPlayerUnit.Instance.getSelectedHero().getHeroSO().heroID;
        if(newHeroSO.heroID==hiredID)
            return true;
        else
            return false;
        }
        else{
            return false;
        }
    }
    private void HireHeroToTeam(heroSO hero){
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(hero.heroID,heroSpawner.HeroController.Player);
        Hero _heroInTawern = spawnedHero.GetComponent<Hero>();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
        HireButton.enabled=false;
        shop.LoadAgain();
    }
}