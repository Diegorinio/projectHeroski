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
    public string[] attacks = new string[2];
    [SerializeField]
    private int[] attackDmgs= {};
    public abstract int Attack1();
    public abstract int Attack2();

    public virtual void Start(){
        attackDmgs= new int[] {Attack1(),Attack2()};
    }
    public int getAttack(int id){
        return attackDmgs[id];
    }
    public int getRandomAttack(){
        return attackDmgs[Random.Range(0,attackDmgs.Length-1)];
    }
    public void dealDamageTo(Enemy enemy, int _atkID){
        // Debug.Log($"attack id: {_atkID}");
        enemy.getHit(attackDmgs[_atkID]);
    }
}
