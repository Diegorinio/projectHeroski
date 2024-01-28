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
    //Znajdz komponent Detector dla gracza lub przeciwnika
    void Start()
    {
        _unit = gameObject.GetComponent<Unit>();
        int distX = _unit.gridDistanceX;
        int distY=_unit.gridDistanceY;
        // Debug.Log($"{_unit.unitName} selected, distance{distX},{distY}");
        tileDetector=gameObject.GetComponent<Detector>();
    }

    //Metoda aktywujaca dana jednostke
    //Obiekt jest przypisywany do turnbaseScript jako aktywny obiekt w kolejce
    //Detektor rozpoczyna dzialanie przez wyszukanie Tile ruchu
    public void selectUnit(){
        turnbaseScript.isSelected=true;
        turnbaseScript.selectedGameObject = gameObject;
        // tileDetector.SetActive(true);
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

// Metoda ruchu gracza na tile
// Jezeli tile jest przypisany to ustaw na null
// Pozniej Przenies na Tile i przypisz jednostke do danego Tile
public void characterMove(GameObject _newTransform){
    if(assignedTile!=null){
    assignedTile.unMakeBusy();
    assignedTile.SetGameObjectOnTile(null);
    assignedTile=null;
    }
    Vector3 gObj = _newTransform.transform.position;
    gameObject.transform.position = new Vector3(gObj.x, gObj.y, -1);
    _newTransform.GetComponent<Tile>().makeBusy();
    assignedTile=_newTransform.GetComponent<Tile>();
    assignedTile.SetGameObjectOnTile(gameObject);
    disableClickable();
}



//Dobra troche namieszalem z atakami ale to może kiedy sie poprawi XD
//Metoda sprawdza czy dany cel jest w liscie wykrytych celow, jezeli tak to moze zaatakowac, glowne uzycie do AI przeciwnika
public void hitToSelectedTarget(GameObject target){
    if(targets.Contains(target)){
    _unit.dealDamageTo(target);
    disableClickable();
    }
}

//Metoda konca tury
// Znajduje glowny kontroler tury i uruchamia nastepna ture
// tileDetector przestaje wykrywanie i "wylacza" dane Tile ktore byly w zasiegu
public void disableClickable(){
    turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
    tileDetector.StopDetector();
    script.nextTurn();
}


//Dodaj do celu pojedynczy cel
//Spoko ale na wydajnosc lepiej dac dodanie drugiej listy 
// W przypadku foreach musi lapac komponenety wszyskich co moze kiedy sie zemscic
public void addToTargets(GameObject trg){
    targets.Add(trg);
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
