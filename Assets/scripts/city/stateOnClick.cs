using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateOnClick : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown(){
        if(obj.activeSelf){
            obj.SetActive(false);
        }else{
            obj.SetActive(true);
        }
    }
}
