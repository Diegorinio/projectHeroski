using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeroBehaviour : MonoBehaviour
{
    private bool isSpellCasted=false;
    public static  EnemyHeroBehaviour Instance{get;private set;}
    private Hero enemyHero;

    void Awake(){
        if(Instance==null){
            Instance=this;
            enemyHero = mainEnemiesUnit.Instance.getSelectedHero();
        }
    }

    public void CastRandomSpell(){
        if(!isSpellCasted){
            GameObject target = SelectRandomTarget().gameObject;
        int rnd = Random.Range(0,2);
        switch(rnd){
            case 0:
            enemyHero.castFirstSpell(target);
            break;
            case 1:
            enemyHero.castSecondSpell(target);
            break;
        }
        setIsEnemyCasted(true);
        Debug.Log($"Enemy hero casted spell");
    }
    }

    public Unit SelectRandomTarget(){
        Unit[] rndUnit = mainPlayerUnit.Instance.getUnits();
        return rndUnit[Random.Range(0,rndUnit.Length-1)];
    }

    public void setIsEnemyCasted(bool state){
        isSpellCasted=state;
    }
}
