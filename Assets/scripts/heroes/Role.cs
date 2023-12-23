using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour
{
    // [SerializeField]
    // private Sprite heroSprite;
    public virtual void Awake()
    {
        // gameObject.GetComponent<SpriteRenderer>().sprite = heroSprite;
    }
    public string roleName;
    public int damage;
    [SerializeField]
    public int gridDistance;
    public string[] attacksNames = new string[2];
    [SerializeField]
    private int[] attackDmgs= {};
    public abstract int Attack1();
    public abstract int Attack2();

    public virtual void Start(){
        attackDmgs= new int[] {Attack1(),Attack2()};
    }
    public int getAttack(int id){
        // int r =Random.Range(0,2);
        switch(id){
            case 0:
            return Attack1();
            case 1:
            return Attack2();
            default:
            return Attack1();
        }
    }
    public int getRandomAttack(){
        int r =Random.Range(0,2);
        switch(r){
            case 0:
            return Attack1();
            case 1:
            return Attack2();
            default:
            return Attack1();
        }
        // return attackDmgs[Random.Range(0,attackDmgs.Length-1)];
    }
    public void dealDamageTo(GameObject _target, int dmg){
        // Debug.Log($"attack id: {_atkID}");
        if(_target.GetComponent<Enemy>()){
            _target.GetComponent<Enemy>().getHit(dmg);
        }else if(_target.GetComponent<Hero>()){
            _target.GetComponent<Hero>().getHit(dmg);
        }
    }
}
