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
        // Debug.Log($"_herotawern {_heroInTawern.thirdSpell()}");
        // _heroInTawern.thirdSpell();
    }
     void OnMouseDown(){
        Debug.Log("DODAJ do druzyny");
        _heroInTawern.thirdSpell();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
    }
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("DODAJ do druzyny pointer");
        _heroInTawern.thirdSpell();
        mainPlayerUnit.Instance.assignHeroToTeam(_heroInTawern);
    }
}
