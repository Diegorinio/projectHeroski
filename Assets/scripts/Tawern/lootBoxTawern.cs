using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class lootBoxTawern : MonoBehaviour
{
    Object[] heroesList;
    heroSO randomHero;
    public GameObject randomHeroImage;
    void Start(){
        heroesList = Resources.LoadAll("Heroes",typeof(heroSO));
        Debug.Log($"heroesList len {heroesList.Length}");
    }

    void OnMouseDown(){
        Debug.Log($"Chest click");
        randomHeroImage.SetActive(true);
        randomHero = heroesList[Random.Range(0,heroesList.Length)] as heroSO;
        randomHeroImage.GetComponent<Image>().sprite = randomHero.heroSprite;
    }

}
