using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrypt okreslajacy jednostke
public class Unit : MonoBehaviour
{
    protected int tier;
    [SerializeField]
    public string unitName{get;set;}
    [SerializeField]
    public Sprite unitSprite;
    [SerializeField]
    private int unitAmount;
    protected int unitBaseHealth;
    protected int unitBaseDamage;
    protected  Vector2Int gridMoveDistance;
    protected Vector2Int gridAttackDistance;
    protected unitGUI _gui{get;set;}

    public virtual void Awake(){}
    public void unitInitialize(UnitSO _unit){
        unitName=_unit.unitName;
        unitBaseDamage=_unit.unitBaseDamage;
        unitBaseHealth=_unit.unitBaseHealth;
        gridMoveDistance = new Vector2Int(_unit.gridDistanceX,_unit.gridDistanceY);
        gridAttackDistance = new Vector2Int(2,4);
        unitSprite = _unit.unitSprite;
    }
    // Start gierze UniGUI ktore wyswietla ilosc jednostek
    void Start()
    {
        _gui=gameObject.GetComponent<unitGUI>();
    }

    //Ustaw ilosc jednostek
    public void setUnitAmount(int amount){
        unitAmount=amount;
    }

    public Vector2Int getUnitMoveDistance(){
        return gridMoveDistance;
    }

    public Vector2Int getAttackDistance(){
        return gridAttackDistance;
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
    //wiadomo
    public void setUnitName(string name){
        unitName=name;
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
    public virtual void getHit(int dmg){
        int lost = (int)(dmg/unitBaseHealth);
        _gui.displayGuiEvent(lost.ToString());
        if(unitAmount-lost<=0){
            GameObject.FindFirstObjectByType<turnbaseScript>().removeFromQueque(gameObject);
            gameObject.SetActive(false);
            if(gameObject.CompareTag("Player")){
                mainPlayerUnit.Instance.removeFromUnits(this);
            }
            else if(gameObject.CompareTag("Enemy")){
                mainEnemiesUnit.Instance.removeFromUnits(this);
            }
            gameObject.GetComponent<unitController>().moveFromTile();
            Destroy(gameObject);
        }
        else{
            unitAmount-=lost;
            Debug.Log($"Stracono {lost} jednostek");
        }
    }

    //Metoda zadajaca obrazenia do danej jednostki
    // Brany jest gameObject reprezentujacy jednostke i zadaje obrazenia przez komponent <Unit>
    public virtual void dealDamageTo(GameObject _target){
        int dmg = getTotalDamage();
        _target.GetComponent<Unit>().getHit(dmg);
        Debug.Log($"hit {dmg} to {_target.name}");
    }
}
