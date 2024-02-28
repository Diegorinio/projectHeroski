using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrypt okreslajacy zachowanie jednostki, ruch atak itd
public class unitController : MonoBehaviour
{
    // Komponent detektor do wykrywania Tile i przeciwnikow
    [SerializeField]
    private Detector tileDetector;
    
    //Lista celow ktore moze zaatakowac po wykryciu w polu ruchu
    [SerializeField]
    private List<GameObject> targets=new List<GameObject>();


    //Komponent <Unit> przypisany do GameObjectu
    private Unit _unit;

    [SerializeField]
    //Tile okreslajacy blok na mapie na ktorym sie znajduje
    private Tile assignedTile;
    //Znajdz komponent Unit danej jednostki
    //Ustaw dystan ruchu
    Vector2Int dist;
    [SerializeField]
    private GameObject enemyTarget;
    //Znajdz komponent Detector dla gracza lub przeciwnika
    void Start()
    {
        enemyTarget=null;
        _unit = gameObject.GetComponent<Unit>();
        dist = _unit.getUnitMoveDistance();
        tileDetector=gameObject.GetComponent<Detector>();
    }

    //Metoda aktywujaca dana jednostke
    //Obiekt jest przypisywany do turnbaseScript jako aktywny obiekt w kolejce
    //Detektor rozpoczyna dzialanie przez wyszukanie Tile ruchu
    public void selectUnit(){
        turnbaseScript.isSelected=true;
        turnbaseScript.selectedGameObject = gameObject;
        enemyTarget=null;
        tileDetector.StartDetector();
        Debug.Log($"{_unit.name} actived");
}

//Zworc Tile na ktorym znajduje sie jednostka
public Tile getAssignedTile(){
    return assignedTile;
}

//Zwroc typ jednostki 
public Unit getAssignedUnit(){
    return _unit;
}

public Vector2Int getUnitDistance(){
    return dist;
}

public Detector getDetector(){
    return tileDetector;
}

public Vector2Int getBaseUnitDistance(){
    return _unit.getUnitMoveDistance();
}

public void setUnitDistance(Vector2Int vector){
    dist = vector;
}

public void setNormalDistance(){
    setUnitDistance(_unit.getUnitMoveDistance());
}

// Metoda ruchu gracza na tile
// Jezeli tile jest przypisany to ustaw na null
// Pozniej Przenies na Tile i przypisz jednostke do danego Tile
//rusza jednostke na dany Tile
//Do rozstawiania na planszy na poczatku gry
//Takze jako glowny skrypt do poruszania sie 

public void characterMove(GameObject _newTransform,bool isStart){
    if(assignedTile!=null){
        moveFromTile();
    }
    Transform trns = (Transform)_newTransform.GetComponent<RectTransform>();
    Vector3 gObj = trns.position;
    gameObject.transform.position = new Vector3(gObj.x, gObj.y, -1);
    Debug.Log($"Chuj dupa cipa {gObj.x},{gObj.y}");
    assignedTile=_newTransform.GetComponent<Tile>();
    assignedTile.SetGameObjectOnTile(gameObject);
    if(!isStart)
        disableClickable();
}
///
//


//Przy ruchu usun wlasnosci tile na ktory poprzednio stala jednostka
public void moveFromTile(){
    assignedTile.unMakeBusy();
    assignedTile.SetGameObjectOnTile(null);
    assignedTile=null;
}

//Ruch jednostki ale po każdym Tile po kolei
//do ruchu po tile
public void characterMove(Tile _targetTile){
    List<Tile> movePath = GridMap.getPathToTile(gameObject,_targetTile.gameObject);
    for(int x=1;x<movePath.Count;x++){
        characterMove(movePath[x].gameObject,true);
        if(movePath[x] is waterTile){
            _targetTile=movePath[x];
            break;
        }
    }
    _targetTile.makeBusy();
    _targetTile.castTileBehaviour();
    disableClickable();
}

//Ruch do tile ale obok wybraneego Tile 
// zastowanie do ruchu blisko przeciwnika
private void characterMoveTroughList(List<Tile> tiles){
    if(tiles.Count>0){
    Tile current_tile=tiles[0];
    for(int x=1;x<tiles.Count;x++){
        characterMove(tiles[x].gameObject,true);
        if(tiles[x] is waterTile){
            current_tile=tiles[x];
            break;
        }
        else{
            current_tile=tiles[x];
        }
    }

    current_tile.makeBusy();
    current_tile.castTileBehaviour();
    disableClickable();
    }
}


//Metoda sprawdza czy dany cel jest w liscie wykrytych celow, jezeli tak to moze zaatakowac, glowne uzycie do AI przeciwnika
public void hitToSelectedTarget(GameObject target){
    if(targets.Contains(target)){
    _unit.dealDamageTo(target);
    disableClickable();
    }
}


//Metoda do ataku
//Podchodzi pod naglizszy Tile obok przeciwnika i zadaje obrazenia
public void playerHitSelectedTarget(GameObject target){
    if(targets.Contains(target)){
        Debug.Log($"{enemyTarget==target}");
        if(enemyTarget==target){
            if(_unit is IDistance){
                _unit.dealDamageTo(target);
                disableClickable();
            }
            else{
            List<Tile> movePath = GridMap.getPathToNeighbourObject(gameObject,target);
            _unit.dealDamageTo(target);
            characterMoveTroughList(movePath);
            }
        }
        else if(enemyTarget==null || enemyTarget!=target){
            enemyTarget=target;
            if((_unit is IDistance)){
                GridMap.enableTile(target.GetComponent<unitController>().getAssignedTile(),Color.red);
            }
            else{
                GridMap.ShowPathNearGameObject(gameObject,target);
            Debug.Log($"Found path to target and clicked 1 time");
            }
        }
    }
}

public void goToNearestTileAndDealDamage(GameObject target){
    if(_unit is IDistance){
                _unit.dealDamageTo(target);
                disableClickable();
            }
            else{
            List<Tile> movePath = GridMap.getPathToNeighbourObject(gameObject,target);
            _unit.dealDamageTo(target);
            characterMoveTroughList(movePath);
            }
}

//Metoda konca tury
// Znajduje glowny kontroler tury i uruchamia nastepna ture
// tileDetector przestaje wykrywanie i "wylacza" dane Tile ktore byly w zasiegu
public void disableClickable(){
    enemyTarget=null;
    turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
    tileDetector.StopDetector();
    script.nextTurn();
}




//Dodaj do listy celow druga liste znalezionych celow
public void addToTargets(List<GameObject> trgs){
    targets.AddRange(trgs);
}

//Wyczysc liste celow, zastosowanie przy koncu tury zeby nie duplikowalo
public void clearTargets(){
    targets.Clear();
}


//Przypisanie Tile do jednostki
public void setTile(Tile tile){
    assignedTile=tile;
}
}
