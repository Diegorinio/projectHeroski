using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tawernHero : MonoBehaviour, IPointerDownHandler
{
    private Hero _heroInTawern;
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("DODAJ do druzyny pointer");
        // _heroInTawern.thirdSpell();
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(1,heroSpawner.HeroController.Player);
        _heroInTawern = spawnedHero.GetComponent<Hero>();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
        Destroy(gameObject);
    }
}
