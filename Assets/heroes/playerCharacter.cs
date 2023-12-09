using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCharacter : MonoBehaviour
{
    bool isClicked;
    public Vector3 distance;
    private Collider2D collider;
    public static bool isCharacter;
    public static GameObject selectedGameObject;
    public GameObject selectedG;
    [SerializeField]
    bool isSelected;
    public GameObject tileDetector;

    //dobra chuj zrobie tak
    public GameObject targetEnemy;
    void Start()
    {
        //tileDetector = gameObject.transform.GetChild(0).gameObject;
        int dist = gameObject.GetComponent<Role>().gridDistance;
        Debug.Log($"dist: {dist}");
        tileDetector.transform.localScale = new Vector3(dist, dist,dist);
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        selectedG = selectedGameObject;
        if (isClicked)
        {
            Vector3 pos = GetMousePos() + distance;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }

    private void OnMouseDown()
    {
        //if (!isCharacter)
        //{
        if (selectedGameObject)
        {
            GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject o in findPlayer)
            {
                o.transform.tag = "Untagged";
                GameObject det = o.GetComponent<playerCharacter>().tileDetector;
                det.SetActive(false);
            }
            selectedGameObject = null;
        }
            isCharacter = true;
            isSelected = true;
            gameObject.transform.tag = "Player";
        selectedGameObject = gameObject;
        tileDetector.SetActive(true);
        Camera.main.GetComponent<guiScript>().chuj();

    }


    private Vector3 GetMousePos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(pos);
    }

    public void disableClickable()
    {
        isCharacter = false;
        isClicked = false;
        isSelected = false;
        GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject o in findPlayer)
        {
            o.transform.tag = "Untagged";
            GameObject det = o.GetComponent<playerCharacter>().tileDetector;
            det.SetActive(false);
        }
        gameObject.transform.tag = "Untagged";
        selectedGameObject = null;
    }
}