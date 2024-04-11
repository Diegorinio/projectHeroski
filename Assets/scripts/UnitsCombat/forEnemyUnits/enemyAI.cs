using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Komponent glowny odpowiedzialny za AI przeciwnika
//TODO: Zrobic lepiej
// Lepsze nazwy 
public class enemyAI : MonoBehaviour
{

    // 0-agresive 1- normal 2- passive
    private Hero enemyGeneral;
    //List Tile po ktorej jednostka moze sie poruszac
    [SerializeField]
    private List<Tile> _collidersMovement = new List<Tile>();

    //List GameObject jednostek gracza
    [SerializeField]
    private List<GameObject> _collidersCharacters = new List<GameObject>();
    private List<GameObject> _colliderCharactersCountered = new List<GameObject>();

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
        for(int x = 0; x < colliders.Count-1; x++)
        {
            if (BattleSystem.IsCounter(gameObject.GetComponent<Unit>(),colliders[x].GetComponent<Unit>())) {
                _colliderCharactersCountered.Add(colliders[x]);
            }
        }
    }

    //Wyczysc movement i wykrytych jednostek
    public void resetAIColliders()
    {
        gameObject.GetComponent<Detector>().StopDetector();
        Debug.Log($"enemyAI colliders clear");
        if(_collidersMovement.Count>0)
        _collidersMovement.Clear();
        if(_collidersCharacters.Count>0)
        _collidersCharacters.Clear();
        _colliderCharactersCountered.Clear();
        // gameObject.GetComponent<Detector>().StopDetector();
    }

    //Zrob losowa akcje
    //Jezeli mozesz sie tylko poruszyc to sie porusz
    //Jezeli sa obce jednostki w zasiegu to losuj czy atakujesz czy robisz ruch
    public void randomAction(){
        StartCoroutine(waitToMakeRandomDecision(0.6f));
    }
    private IEnumerator waitToMakeRandomDecision(float time){
        yield return new WaitForSeconds(time);
        enemyGeneral=mainEnemiesUnit.Instance.getSelectedHero();
        enemyGeneral.getTypeOfGeneral();
        print(enemyGeneral.getTypeOfGeneral());
        print((int)enemyGeneral.getTypeOfGeneral());
        print("sadasdasdasd");
        if ((int)enemyGeneral.getTypeOfGeneral() == 0)
        {//agresywny
            if (_collidersCharacters.Count > 0)
            {
            attackDamageToRandomPlayer();
            }
            else
            {
                moveToRandomDirecion();
            }


        }
        else if ((int)enemyGeneral.getTypeOfGeneral()==1)
        {//normalny
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
        else
        {//pasywny
            if (_colliderCharactersCountered.Count > 0)
            {
            attackDamageToRandomPlayer();
            }
            else
            {
                moveToRandomDirecion();
            }

        }
        resetAIColliders();

    }
    //Zrob ruch w losowej miejsce z Tile w zasiegu
    public void moveToRandomDirecion()
    {
        if(_collidersMovement.Count>0){
        int id = Random.Range(0, _collidersMovement.Count - 1);
        Transform rndCollider = _collidersMovement[id].transform;
        gameObject.GetComponent<unitController>().characterMovePerTile(rndCollider.GetComponent<Tile>());
        }
    } 

    //Zaatakauj losowa jednostke gracza jesli jest to kontruje atack kontrowana
    public void attackDamageToRandomPlayer(){
        if ((int)enemyGeneral.getTypeOfGeneral() == 0 || (int)enemyGeneral.getTypeOfGeneral() == 1)
        {
            if (_collidersCharacters.Count > 0)
            {
                if (_colliderCharactersCountered.Count > 0)
                {
                    attackPlayerUnitCountered();
                }
                attackPlayerUnit();
            }
        }
        else
        {
            if (_colliderCharactersCountered.Count > 0)
            {
                    attackPlayerUnitCountered();
            }


        }

    }
    private void attackPlayerUnit()
    {
        int id=Random.Range(0,_collidersCharacters.Count-1);
        GameObject selectedHero=_collidersCharacters[id].transform.gameObject;
        BattleSystem.IsCounter(gameObject.GetComponent<Unit>(),selectedHero.GetComponent<Unit>());
        Debug.Log($"Przeciwnik aatakowal {selectedHero.name} AI");
        gameObject.GetComponent<unitController>().goToNearestTileAndDealDamage(selectedHero);

    }
    private void attackPlayerUnitCountered()
    {
        int id=Random.Range(0,_colliderCharactersCountered.Count-1);
        print("XDDDDDDDDDDDDDDDDDDDDDDDDDDDD123321");
        GameObject selectedHero=_colliderCharactersCountered[id].transform.gameObject;
        BattleSystem.IsCounter(gameObject.GetComponent<Unit>(),selectedHero.GetComponent<Unit>());
        Debug.Log($"Przeciwnik aatakowal {selectedHero.name} AI");
        gameObject.GetComponent<unitController>().goToNearestTileAndDealDamage(selectedHero);

        
    }
}
