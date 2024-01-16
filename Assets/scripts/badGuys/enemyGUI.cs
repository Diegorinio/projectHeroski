using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGUI :characterGUI
{
    // [SerializeField]
    // Enemy assignedEnemy;
    // // Start is called before the first frame update
    // public override void Awake()
    // {
    //     assignedEnemy=gameObject.GetComponent<Enemy>();
    //     GameObject heroCanvas = gameObject.transform.Find("hero_canvas").gameObject;
    //     hpSlider=heroCanvas.transform.Find("hpSlider").GetComponent<Slider>();
    //     eventText=heroCanvas.transform.Find("eventText").GetComponent<Text>();
    //     hpSlider.maxValue=assignedEnemy.getHealth();
    //     Debug.Log($"{assignedEnemy.getHeroName()} {assignedEnemy.getHealth()} init end");
    // }

    // public override void Update()
    // {
    //     hpSlider.value=assignedEnemy.getHealth();
    // }
}
