using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrypt okreslajacy jednostke
public class Unit : MonoBehaviour
{
    [SerializeField]
    public string unitName{get;set;}
    [SerializeField]
    public Sprite unitSprite;
    [SerializeField]
    private int unitAmount;
    private int unitHealth;
    public int unitBaseHealth{get;set;}
    public int unitBaseDamage{get;set;}
    public int gridDistanceX{get;set;}
    public int gridDistanceY{get;set;}
    public unitGUI _gui{get;set;}

    public virtual void Awake(){}
    // Start gierze UniGUI ktore wyswietla ilosc jednostek
    void Start()
    {
        _gui=gameObject.GetComponent<unitGUI>();
    }

    //Ustaw ilosc jednostek
    public void setUnitAmount(int amount){
        unitAmount=amount;
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
            unitHealth=0;
            GameObject.FindFirstObjectByType<turnbaseScript>().removeFromQueque(gameObject);
            gameObject.SetActive(false);
            // mainPlayerUnit.Instance.removeFromUnits(this);
            // Destroy(gameObject);
            if(gameObject.CompareTag("Player")){
                mainPlayerUnit.Instance.removeFromUnits(this);
                Destroy(gameObject);
            }
            else if(gameObject.CompareTag("Enemy")){
                mainEnemiesUnit.Instance.removeFromUnits(this);
                Destroy(gameObject);
            }
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
