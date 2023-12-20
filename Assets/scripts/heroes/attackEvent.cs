using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEvent : MonoBehaviour
{
    public bool isSet;
    [SerializeField]
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDamageValue(int dmg){
        damage=dmg;
        isSet=true;
    }
}
