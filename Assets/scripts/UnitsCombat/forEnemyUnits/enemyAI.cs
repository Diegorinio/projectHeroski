using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Komponent glowny odpowiedzialny za AI przeciwnika
//TODO: Zrobic lepiej
// Lepsze nazwy 
public class enemyAI : MonoBehaviour
{

    //List Tile po ktorej jednostka moze sie poruszac
    [SerializeField]
    private List<Tile> _collidersMovement = new List<Tile>();

    //List GameObject jednostek gracza
    [SerializeField]
    private List<GameObject> _collidersCharacters = new List<GameObject>();

    //Dany typ jednostki
    private Unit assignedEnemy;
    void Start()
    {
        assignedEnemy=gameObject.GetComponent<Unit>();
    }

    //Przypisz liste Tile ruchu
    public void changeCollidersMovement(List<Tile> colliders)
    {
        _collidersMovement = colliders;
    }

    //Przypisz/Zmien liste przeciwnikow(Jednostek gracza) ktorej zostaly wykryte i mozliwe do zaatakwaonia
    public void changeCollidersCharacters(List<GameObject> colliders){
        _collidersCharacters=colliders;
    }

    //Wyczysc movement i wykrytych jednostek
    public void resetAIColliders()
    {
        Debug.Log($"enemyAI colliders clear");
        if(_collidersMovement.Count>0)
        _collidersMovement.Clear();
        if(_collidersCharacters.Count>0)
        _collidersCharacters.Clear();
    }

    //Zrob losowa akcje
    //Jezeli mozesz sie tylko poruszyc to sie porusz
    //Jezeli sa obce jednostki w zasiegu to losuj czy atakujesz czy robisz ruch
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

    //Zrob ruch w losowej miejsce z Tile w zasiegu
    public void moveToRandomDirecion()
    {
        if(_collidersMovement.Count>0){
        int id = Random.Range(0, _collidersMovement.Count - 1);
        Transform rndCollider = _collidersMovement[id].transform;
        // gameObject.GetComponent<unitController>().characterMove(rndCollider.gameObject);
        gameObject.GetComponent<unitController>().characterMove(rndCollider.GetComponent<Tile>());
        }
    } 

    //Zaatakauj losowa jednostke gracza
    public void attackDamageToRandomPlayer(){
        if(_collidersCharacters.Count>0){
        int id=Random.Range(0,_collidersCharacters.Count-1);
        GameObject selectedHero=_collidersCharacters[id].transform.gameObject;
        Debug.Log($"Przeciwnik aatakowal {selectedHero.name} AI");
        // gameObject.GetComponent<unitController>().hitToSelectedTarget(selectedHero);
        gameObject.GetComponent<unitController>().playerHitSelectedTarget(selectedHero);
        }
    }

    public void attackRandomPlayer(){
        
    }

}
