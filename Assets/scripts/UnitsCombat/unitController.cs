using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitController : MonoBehaviour
{
 [SerializeField]
    private GameObject tileDetector;
    // public GameObject targetEnemy;
    [SerializeField]
    public List<GameObject> targets=new List<GameObject>();
    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private Tile assignedTile;
    void Start()
    {
        _unit = gameObject.GetComponent<Unit>();
        int dist = _unit.gridDistance;
        Debug.Log($"{_unit.unitName} selected, distance{dist}");
        tileDetector=gameObject.transform.Find("tileDetector").gameObject;
        tileDetector.transform.localScale = new Vector3(dist, dist, dist);
        tileDetector.SetActive(false);
    }

    public void selectHero(){
        turnbaseScript.isSelected=true;
        turnbaseScript.selectedGameObject = gameObject;
        tileDetector.SetActive(true);
        // gameObject.GetComponent<Hero>().
        // Camera.main.GetComponent<guiScript>().initializeGui();
}

public void characterMove(GameObject _newTransform){
    if(assignedTile!=null){
    assignedTile.GetComponent<Tile>().unMakeBusy();
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
public void hitToSelectedTarget(GameObject target){
    _unit.dealDamageTo(target);
    disableClickable();
}
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

public void setTile(Tile tile){
    assignedTile=tile;
}
}
