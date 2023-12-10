using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour
{
    // public static GameObject selectedGameObject;
    [SerializeField]
    private GameObject selectedG;
    [SerializeField]
    bool isSelected;
    [SerializeField]
    private GameObject tileDetector;
    public GameObject targetEnemy;
    void Start()
    {
        int dist = gameObject.GetComponent<Role>().gridDistance;
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
        Camera.main.GetComponent<guiScript>().initializeGui();
}
public void disableClickable(){
    tileDetector.SetActive(false);
    turnbaseScript script = GameObject.FindObjectOfType<turnbaseScript>();
    script.nextTurn();
    turnbaseScript.isSelected=false;
}
}