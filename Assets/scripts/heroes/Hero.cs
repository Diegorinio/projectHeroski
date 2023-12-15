using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private Sprite heroSprite;
    [SerializeField]
    private int health;
    [SerializeField]
    private string heroName;
    [SerializeField]
    private heroGUI _gui;
    public void Awake(){
        _gui=gameObject.GetComponent<heroGUI>();
        gameObject.GetComponent<SpriteRenderer>().sprite=heroSprite;
        // setUpGUI();
    }

    public void Update(){
        // hpSlider.value=health;
    }

    public string getHeroName(){
        return heroName;
    }

    public int getHealth(){
        return health;
    }

    public void getHit(int dmg){
        _gui.displayGuiEvent($"-{dmg}");
        if((health-dmg)<=0){
            health=0;
            GameObject.FindFirstObjectByType<turnbaseScript>().removeFromQueque(gameObject);
            gameObject.SetActive(false);
            this.enabled=false;
        }
        else{
        health-=dmg;
        Debug.Log($"{this.name} otrzymal {dmg}");
        }
    }
}
