using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// [RequireComponent(typeof(Enemy))]
public class enemyAI : MonoBehaviour
{
    // [SerializeField]
    private List<Collider2D> _collidersMovement = new List<Collider2D>();
    // [SerializeField]
    private List<Collider2D> _collidersCharacters = new List<Collider2D>();
    // [SerializeField]
    private Unit assignedEnemy;
    void Start()
    {
        assignedEnemy=gameObject.GetComponent<Unit>();
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
        _collidersMovement = colliders.GroupBy(c => c.GetComponent<Tile>().isActive||!c.GetComponent<Tile>().isBusy()).Select(d => d.First()).ToList();
    }
    public void changeCollidersCharacters(List<Collider2D> colliders){
        _collidersCharacters.Clear();
        _collidersCharacters=colliders.GroupBy(c=>c.name).Select(d=>d.First()).ToList();
    }
    public void resetAIColliders()
    {
        Debug.Log($"enemyAI colliders clear");
        if(_collidersMovement.Count>0)
        _collidersMovement.Clear();
        if(_collidersCharacters.Count>0)
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
        resetAIColliders();
    }
    public void moveToRandomDirecion()
    {
        if(_collidersMovement.Count>0){
        int id = Random.Range(0, _collidersMovement.Count - 1);
        // Debug.Log($"moves list size {_collidersMovement.Count} wybor id: {id} move to tile: {_collidersMovement[id].transform.name}");
        // gameObject.transform.position = _collidersMovement[id].transform.position;
        // if(_collidersMovement[id].GetComponent<Tile>().isBusy()){
        //     moveToRandomDirecion();
        // }
        Transform rndCollider = _collidersMovement[id].transform;
        // gameObject.transform.position = new Vector3(rndCollider.position.x,rndCollider.position.y,gameObject.transform.position.z);
        gameObject.GetComponent<unitController>().characterMove(rndCollider.gameObject);
        // gameObject.GetComponent<characterController>().disableClickable();
        // resetColliders();
        }
    } 
    bool isTileTaken(Tile tile){
        return tile.isBusy();
    }
    public void attackDamageToRandomPlayer(){
        if(_collidersCharacters.Count>0){
        attackEvent atkEvent = gameObject.GetComponent<attackEvent>();
        // atkEvent.setDamageValue(gameObject.GetComponent<Role>().getRandomAttack());
        int id=Random.Range(0,_collidersCharacters.Count-1);
        GameObject selectedHero=_collidersCharacters[id].transform.gameObject;
        // assignedEnemy.dealDamageTo(selectedHero);
        // selectedHero.GetComponent<characterController>().hitToSelectedTarget(sele)
        gameObject.GetComponent<unitController>().hitToSelectedTarget(selectedHero);
        // assignedEnemy.dealDamageTo(selectedHero);
        // gameObject.GetComponent<characterController>().disableClickable();
        // resetColliders();
        }
    }

}
