using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour
{
    // public static GameObject selectedGameObject;
    // [SerializeField]
    // private GameObject selectedG;
    // [SerializeField]
    // bool isSelected;
    [SerializeField]
    private GameObject tileDetector;
    public GameObject targetEnemy;
    [SerializeField]
    public List<GameObject> targets=new List<GameObject>();
    [SerializeField]
    private Role assignedRole;
    void Start()
    {
        assignedRole = gameObject.GetComponent<Role>();
        int dist = assignedRole.gridDistance;
        Debug.Log($"dist: {dist}");
        tileDetector=gameObject.transform.Find("tileDetector").gameObject;
        tileDetector.transform.localScale = new Vector3(dist, dist, dist);
        tileDetector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
    }

    public void selectHero(){
        turnbaseScript.isSelected=true;
        turnbaseScript.selectedGameObject = gameObject;
        tileDetector.SetActive(true);
        // gameObject.GetComponent<Hero>().
        Camera.main.GetComponent<guiScript>().initializeGui();
}

public void characterMove(Transform _newTransform){
    Vector3 gObj = _newTransform.transform.position;
    gameObject.transform.position = new Vector3(gObj.x, gObj.y, gameObject.transform.position.z);
    disableClickable();
}
public void hitToSelectedTarget(GameObject target){
    attackEvent atkEvent = gameObject.GetComponent<attackEvent>();
    int dmg = atkEvent.damage;
    if(target.GetComponent<Enemy>() && atkEvent.isSet){
        Enemy trg = target.GetComponent<Enemy>();
        assignedRole.dealDamageTo(trg,dmg);
        atkEvent.isSet=false;
        disableClickable();
    }
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
}