using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Unity.VisualScripting;

// Skrypt okreslajacy jednostke
public class Unit : MonoBehaviour
{


    protected int UnitType; 
    protected int tier;
    [SerializeField]
    public string unitName{get;set;}
    [SerializeField]
    public Sprite unitSprite{get;set;}
    [SerializeField]
    private int unitAmount;
    public int unitBaseHealth{get;private set;}
    public int unitBaseDamage{get;private set;}
    protected  Vector2Int gridMoveDistance;
    //do usuniecia
    protected Vector2Int gridAttackDistance;
    protected unitGUI _gui{get;set;}
    protected UnitSO _SO;

    // public virtual void Awake(){}
    public void unitInitialize(int _tier,UnitSO _unit){
        tier=_tier;
        unitName=_unit.unitName;
        unitBaseDamage=_unit.unitBaseDamage;
        unitBaseHealth=_unit.unitBaseHealth;
        gridMoveDistance = new Vector2Int(_unit.gridDistanceX,_unit.gridDistanceY);
        gridAttackDistance = new Vector2Int(2,4);
        unitSprite = _unit.unitSprite;
        _SO=_unit;
    }
    // Start gierze UniGUI ktore wyswietla ilosc jednostek
    void Start()
    {
        _gui=gameObject.GetComponent<unitGUI>();
    }

    public UnitSO getUnitSO(){
        return _SO;
    }
    public Image getUnitImage(){
        return transform.Find("hero_canvas").transform.Find("unit_sprite").GetComponent<Image>();
    }
    public void SetUnitType(int unittype)
    {
        UnitType = unittype;
    }
    public int getUnitType() { return UnitType;}

    public int getUnitTier(){
        return tier;
    }

    //Ustaw ilosc jednostek
    public void setUnitAmount(int amount){
        unitAmount=amount;
    }

    public Vector2Int getUnitMoveDistance(){
        return gridMoveDistance;
    }
    public void setTier(int _tier){
        tier=_tier;
    }
    //Zwraca ilosc dostepnych jednostek
    public int getUnitAmount(){
        return unitAmount;
    }
    //Dodaj jednostki do aktualnej ilosci jednostek
    public void addUnits(int amount){
        unitAmount+=amount;
    }
    //Zwroc damage ktory zadaje przez ilosc jednostek * atak per jednostka
    public int getTotalDamage(){
        return unitAmount*unitBaseDamage;
    }
    //Zwroc calokowite hp
    public int getTotalHealth(){
        return unitAmount*unitBaseHealth;
    }

    //Metoda okreslajaca otrzymanie obrazen, jezeli przez zadanie obrazenia ilosc jednostek<=0 to
    // wyrzuc jednostke z tury
    // usun ze sceny
    // usun z managera ktory trzyma jednostki miedzy scenami
    // dla przeciwnika mainPlayerUnit, dla uzytkownika mainEnemiesUnit

    //Do walki jednostek
    //Jednostka atakuje jednostke
    public virtual void getHit(int dmg){
        // int lost = (int)(dmg/unitBaseHealth);
        _gui.displayGuiEvent(dmg.ToString());
        // lostUnits(lost);
        lostUnits(dmg);
    }


    //Do czarow
    //Czary na bazie procenta damage
    public virtual void getHitBySpell(float procent_dmg){
        int lost = (int)(procent_dmg/100*unitAmount);
        _gui.displayGuiEvent(lost.ToString());
        lostUnits(lost);
    }
    //Czary na bazie stalego damage
    public virtual void getHitBySpell(int dmg){
        int lost = (int)(dmg/unitBaseHealth);
        _gui.displayGuiEvent(lost.ToString());
        lostUnits(lost);
    }


    //Metoda obliczajac ilosc straconych jednostek, jezeli po straceniu będzie <0 to usun jednostke z gry
    private void lostUnits(int amount){
        if(unitAmount-amount<=0){
            UnitDestroyed();
        }
        else{
            unitAmount-=amount;
            Debug.Log($"Stracono {amount} jednostek");
        }
    }

    private void UnitDestroyed(){
            turnbaseScript TBS = GameObject.FindFirstObjectByType<turnbaseScript>();
            TBS.removeFromQueque(gameObject);
            _gui.setUnitTextVal("");
            // gameObject.SetActive(false);
            if(gameObject.CompareTag("Player")){
                mainPlayerUnit.Instance.removeFromUnits(this);
            }
            else if(gameObject.CompareTag("Enemy")){
                mainEnemiesUnit.Instance.removeFromUnits(this);
            }
            gameObject.GetComponent<unitController>().moveFromTile();
            TBS.checkGameState();
            // RemoveComponents();
            Destroy(gameObject);

    }
    
    //Leczenie jednostki przez dodawanie na podstawie stalej 
    public virtual void healUnit(int heal){
        int toHeal = (int)(heal/unitBaseHealth);
        unitAmount+=toHeal;
        _gui.displayGuiEvent($"+{heal.ToString()}");
    }

    //Leczenie jednostki przez dodawanie na podstawie procenta ilosci jednostki
    public virtual void healUnit(float procent_heal){
        int toHeal = (int)(procent_heal/100*unitAmount);
        unitAmount+=toHeal;
        _gui.displayGuiEvent($"+{toHeal.ToString()}");
    }

    //Metoda zadajaca obrazenia do danej jednostki
    // Brany jest gameObject reprezentujacy jednostke i zadaje obrazenia przez komponent <Unit>
    public virtual void dealDamageTo(GameObject _target){

        
        int dmg = BattleSystem.getDealtDamage(this,_target.GetComponent<Unit>());
        //akcja na zakonczenie animacji 
        Action AnimationFinish = ()=>{
            _target.GetComponent<Unit>().getHit(dmg);
            gameObject.GetComponent<unitController>().disableClickable();
        };
        _gui.displayAnimEvent(dmg,gameObject,_target,AnimationFinish);
    }
    //Tutaj mozna wwalic obliczanie damage
}

