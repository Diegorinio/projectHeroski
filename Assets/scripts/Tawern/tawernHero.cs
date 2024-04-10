using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tawernHero : MonoBehaviour, IPointerDownHandler
{
    private Hero _heroInTawern;
    public int heroID;
    public void OnPointerDown(PointerEventData eventData){
        GameObject spawnedHero = heroSpawner.spawnHeroGameObject(heroID,heroSpawner.HeroController.Player);
        _heroInTawern = spawnedHero.GetComponent<Hero>();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
        Destroy(gameObject);
    }
}
