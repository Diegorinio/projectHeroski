using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class enemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Collider2D> _collidersMovement = new List<Collider2D>();
    [SerializeField]
    private List<Collider2D> _collidersCharacters = new List<Collider2D>();
    [SerializeField]
    private Enemy assignedEnemy;
    void Start()
    {
        assignedEnemy=gameObject.GetComponent<Enemy>();
        //gameObject.transform.position = _colliders[(Random.Range(0, _colliders.Count))].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = _colliders[(Random.Range(0, _colliders.Count))].transform.position;
    }
    public void changeCollidersMovement(List<Collider2D> colliders)
    {
        //colliders = colliders.GroupBy(c => c.name).Select(d => d.First()).ToList();
        _collidersMovement.Clear();
        _collidersMovement = colliders.GroupBy(c => c.name).Select(d => d.First()).ToList();
    }
    public void changeCollidersCharacters(List<Collider2D> colliders){
        _collidersCharacters.Clear();
        _collidersCharacters=colliders.GroupBy(c=>c.name).Select(d=>d.First()).ToList();
    }
    public void resetColliders()
    {
        _collidersMovement.Clear();
        _collidersCharacters.Clear();
    }

    public void randomAction(){
        if(_collidersCharacters.Count>0){
                    int r = Random.Range(0,2);
        Debug.Log($"{assignedEnemy.transform.name} enemyAI selected action {r}");
        switch (r){
            case 0:
            moveToRandomDirecion();
            break;
            case 1:
            attackDamageToRandomPlayer();
            break;
        }
        }
        else{
            moveToRandomDirecion();
        }
    }
    public void moveToRandomDirecion()
    {
        if(_collidersMovement.Count>0){
        int id = Random.Range(0, _collidersMovement.Count - 1);
        Debug.Log($"moves list size {_collidersMovement.Count} wybor id: {id}");
        // gameObject.transform.position = _collidersMovement[id].transform.position;
        Transform rndCollider = _collidersMovement[id].transform;
        gameObject.transform.position = new Vector3(rndCollider.position.x,rndCollider.position.y,gameObject.transform.position.z);
        gameObject.GetComponent<characterController>().disableClickable();
        }
    }
    public void attackDamageToRandomPlayer(){
        if(_collidersCharacters.Count>0){
        int id=Random.Range(0,_collidersCharacters.Count-1);
        Hero selectedHero=_collidersCharacters[id].GetComponent<Hero>();
        assignedEnemy.dealDamageTo(selectedHero);
        gameObject.GetComponent<characterController>().disableClickable();
        }

    }

}
