using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        _gui=gameObject.GetComponent<unitGUI>();
    }

    public void setUnitAmount(int amount){
        unitAmount=amount;
    }

    public int getUnitAmount(){
        return unitAmount;
    }
    public void addUnits(int amount){
        unitAmount+=amount;
    }

    public void setUnitName(string name){
        unitName=name;
    }

    public int getTotalDamage(){
        return unitAmount*unitBaseDamage;
    }

    public int getTotalHealth(){
        return unitAmount*unitBaseHealth;
    }

    public virtual void getHit(int dmg){
        // _gui.displ
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
    public virtual void dealDamageTo(GameObject _target){
        int dmg = getTotalDamage();
        _target.GetComponent<Unit>().getHit(dmg);
        Debug.Log($"hit {dmg} to {_target.name}");
    }
}
