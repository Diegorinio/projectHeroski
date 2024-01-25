using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Prosty skrypt ktory wylacza/wlacza dany obiekt po klinieciu
public class stateOnClick : MonoBehaviour
{
    public GameObject obj;
    void Start()
    {
        obj.SetActive(false);   
    }

    public void OnMouseDown(){
        if(obj.activeSelf){
            obj.SetActive(false);
        }else{
            obj.SetActive(true);
        }
    }
}
