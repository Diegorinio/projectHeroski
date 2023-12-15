using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tavern : MonoBehaviour, IPointerDownHandler
{
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel=gameObject.transform.Find("tavern_panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log($"tavern click {gameObject.transform.name}");
        panel.SetActive(true);
    }

    private void OnMouseDown(){
        Debug.Log("on mouse down test");
    }
}
