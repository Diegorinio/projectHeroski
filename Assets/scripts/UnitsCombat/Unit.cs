using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public string unitName{get;set;}
    [SerializeField]
    private Sprite unitSprite;
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
        gameObject.GetComponent<SpriteRenderer>().sprite=unitSprite;
    }

    public void setUnitAmount(int amount){
        unitAmount=amount;
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

    public void getHit(int dmg){
        // _gui.displ
        int lost = (int)(dmg/unitBaseHealth);
        if(unitAmount-lost<=0){
            unitHealth=0;
            GameObject.FindFirstObjectByType<turnbaseScript>().removeFromQueque(gameObject);
            gameObject.SetActive(false);
        }
        else{
            unitAmount-=lost;
            Debug.Log($"Stracono {lost} jednostek");
        }
    }
    public void dealDamageTo(GameObject _target){
        // Debug.Log($"attack id: {_atkID}");
        int dmg = getTotalDamage();
        // if(_target.CompareTag("Enemy")){
        //     _target.GetComponent<Unit>().getHit(dmg);
        // }else if(_target.CompareTag("Enemy")){
        //     _target.GetComponent<Unit>().getHit(dmg);
        // }
        _target.GetComponent<Unit>().getHit(dmg);
        Debug.Log($"hit {dmg} to {_target.name}");
    }
}
