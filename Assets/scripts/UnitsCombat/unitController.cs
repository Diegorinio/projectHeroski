using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrypt okreslajacy zachowanie jednostki
public class unitController : MonoBehaviour
{
 [SerializeField]
    //GameObject wykrywajacy trigger z mapa i przeciwnikiem
    private GameObject tileDetector;
    // public GameObject targetEnemy;
    // [SerializeField]
    private List<GameObject> targets=new List<GameObject>();
    [SerializeField]
    //Komponent <Unit> przypisany do GameObjectu
    private Unit _unit;

    [SerializeField]
    //Tile okreslajacy blok na mapie na ktorym sie znajduje
    private Tile assignedTile;
    //W Start znadz tileDetector i zmien jego rozmiar do zasiegu poruszania jednostki
    void Start()
    {
        _unit = gameObject.GetComponent<Unit>();
        int distX = _unit.gridDistanceX;
        int distY=_unit.gridDistanceY;
        Debug.Log($"{_unit.unitName} selected, distance{distX},{distY}");
        tileDetector=gameObject.transform.Find("tileDetector").gameObject;
        tileDetector.transform.localScale = new Vector3(distX, distY, 0);
        tileDetector.SetActive(false);
    }
    //Metoda aktywujaca dana jednostke
    //Obiekt jest przypisywany do turnbaseScript jako aktywny obiekt w kolejce
    //Aktywacja tileDetectora
    public void selectHero(){
        turnbaseScript.isSelected=true;
        turnbaseScript.selectedGameObject = gameObject;
        tileDetector.SetActive(true);
}

//Ustaw tileDetector, uzywane przez to ze detektor jest dodawany pozniej zaleznie czy jednostka jest Player/Enemy
public void setTileDetector( GameObject detector){
    tileDetector=detector;
}

// Metoda ruchu gracza na tile mapy, _newTransform jest pozycja Tile, gracz przechodzi na pozycje 
public void characterMove(GameObject _newTransform){
    if(assignedTile!=null){
    assignedTile.unMakeBusy();
    assignedTile=null;
    }
    Vector3 gObj = _newTransform.transform.position;
    gameObject.transform.position = new Vector3(gObj.x, gObj.y, -1);
    disableClickable();
    _newTransform.GetComponent<Tile>().makeBusy();
    assignedTile=_newTransform.GetComponent<Tile>();
    // assignedTile.unMakeBusy();
}

//Dobra troche namieszalem z atakami ale to może kiedy sie poprawi XD
//Metoda sprawdza czy dany cel jest w liscie wykrytych celow, jezeli tak to moze zaatakowac, glowne uzycie do AI przeciwnika
public void hitToSelectedTarget(GameObject target){
    if(targets.Contains(target)){
    _unit.dealDamageTo(target);
    disableClickable();
    }
}

//Metoda wylaczajaca detektor brak detekcji triggerow, nastepnie przechodzi do nastepnej tury
public void disableClickable(){
    tileDetector.SetActive(false);
    turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
    script.nextTurn();
}


public void addToTargets(GameObject trg){
    targets.Add(trg);
}

public void clearTargets(){
    targets.Clear();
}

//Ustawienie aktualnego Tile pozycji na mapie
public void setTile(Tile tile){
    assignedTile=tile;
}
}
