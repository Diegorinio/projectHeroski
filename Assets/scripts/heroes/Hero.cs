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
    private Slider hpSlider;
    public void Awake(){
        gameObject.GetComponent<SpriteRenderer>().sprite=heroSprite;
        setUpGUI();
    }

    public void Update(){
        hpSlider.value=health;
    }

    public string getHeroName(){
        return heroName;
    }

    public void getHit(int dmg){
        health-=dmg;
        Debug.Log($"{this.name} otrzymal {dmg}");
    }

    public void setUpGUI(){
        hpSlider.maxValue=health;
    }
}
