using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tawernHero : MonoBehaviour, IPointerDownHandler
{
    private Hero _heroInTawern;
    public void assignSOToHeroTawern(heroSO _hero){
        gameObject.GetComponent<Hero>().assignHeroSO(_hero);
        _heroInTawern = gameObject.GetComponent<Hero>();
        _heroInTawern.setHeroTag("Player");
        // Debug.Log($"_herotawern {_heroInTawern.thirdSpell()}");
        // _heroInTawern.thirdSpell();
    }
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("DODAJ do druzyny pointer");
        // _heroInTawern.thirdSpell();
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(1,heroSpawner.HeroController.Player);
        _heroInTawern = spawnedHero.GetComponent<Hero>();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
    }
}
