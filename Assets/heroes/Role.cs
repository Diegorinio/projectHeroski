using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour
{
    public abstract void Awake();
    public string roleName;
    public int damage;
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
    public void dealDamageTo(Enemy enemy, int _atkID){
        // Debug.Log($"attack id: {_atkID}");
        enemy.getHit(attackDmgs[_atkID]);
    }
}
