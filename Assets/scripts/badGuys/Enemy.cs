using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// [RequireComponent(typeof(characterGUI))]
public class Enemy : MonoBehaviour
{
    // [SerializeField]
    // private string bossName;
    // [SerializeField]
    // private int hp;
    // [SerializeField]
    // private string[] bossAttacks = new string[2];
    // [SerializeField]
    // private Slider hpSlider;
    // [SerializeField]
    // Sprite enemySprite;
    // [SerializeField]
    // public GameObject tileDetector;
    // public void Awake()
    // {
    //     Camera.main.GetComponent<guiScript>().bossName.text = bossName;
    //     gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;
    //     tileDetector = gameObject.transform.Find("tileDetector").gameObject;
    //     tileDetector.SetActive(false);
    // }
    // public void Start()
    // {

    // }
    // public void Update()
    // {
    //     hpSlider.value = hp;
    // }
    // public int Attack1()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public int Attack2()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public void dealDamageTo(Hero hero)
    // {
    //     int dmg = gameObject.GetComponent<Role>().getRandomAttack();
    //     hero.getHit(dmg);
    // }
    // public void getHit(int dmg)
    // {
    //     if(hp-dmg<=0){
    //         hp=0;
    //         GameObject.FindAnyObjectByType<turnbaseScript>().removeFromQueque(gameObject);
    //         gameObject.SetActive(false);
    //         this.enabled=false;
    //     }
    //     hp= hp-dmg;
    //     Debug.Log($"{this.name} otrzymal {dmg}");
    // }

    // public string getClassType(){
    //     return this.GetType().ToString();
    // }

    [SerializeField]
    private Sprite enemySprite;
    [SerializeField]
    private int health;
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private  characterGUI _gui;
    public void Awake(){
        _gui=gameObject.GetComponent<enemyGUI>();
        gameObject.GetComponent<SpriteRenderer>().sprite=enemySprite;
        // setUpGUI();
    }

    public void Update(){
        // hpSlider.value=health;
    }

    public void setHeroName(string name){
        enemyName=name;
    }

    public void setHeroHealth(int hp){
        health=hp;
    }
    public string getHeroName(){
        return enemyName;
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

    public void dealDamageTo(GameObject target){
        attackEvent atkEvent = gameObject.GetComponent<attackEvent>();
        if(target.GetComponent<Hero>()&&atkEvent.isSet){
            Hero h = target.GetComponent<Hero>();
            h.getHit(atkEvent.damage);
            atkEvent.isSet=false;
            turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
            script.nextTurn();
        }
    }


    public override string ToString()
    {
        return $"Hero name: {enemyName}";
    }
        public void OnMouseDown(){
        Debug.Log($"enemy selected {enemyName}");
        if(turnbaseScript.selectedGameObject.GetComponent<Hero>()){
            turnbaseScript.selectedGameObject.GetComponent<characterController>().hitToSelectedTarget(this.gameObject);
        }
    }
    
}
