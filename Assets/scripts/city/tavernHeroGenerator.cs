using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tavernHeroGenerator : characterGenerator
{
    Image _heroImg;
    Text _heroNameText;
    Text _heroRoleText;
    // [SerializeField]
    Sprite heroImage;
    // [SerializeField]
    string _heroName;
    string _heroRole;
    public GameObject tmpChar;
    characterClass _role;

    void Start(){
        _heroName=getRandomName();
        _role = getRandomClass();
        _heroRole=_role.ToString();
        Debug.Log($"parent {gameObject.transform.parent.name}");
        _heroImg=gameObject.transform.Find("heroImage").GetComponent<Image>();
        _heroRoleText=gameObject.transform.Find("hero_role").GetComponent<Text>();
        _heroNameText=gameObject.transform.Find("hero_name").GetComponent<Text>();
        _heroNameText.text=_heroName;
        // roles[Random.Range(0,roles.Length-1)];
        _heroRoleText.text=_heroRole;
    }

    void OnMouseDown(){
        if(mainPlayer.Instance.getHeroes().Length<5){
        GameObject newHero = generateFromData(_heroName,characterType.Hero,_role);
        newHero.transform.SetParent(mainPlayer.Instance.transform);
        mainPlayer.Instance.addHeroToTeam(newHero);
        Destroy(gameObject);
        }
    }
}
