using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGUI :characterGUI
{
    [SerializeField]
    Enemy assignedEnemy;
    // Start is called before the first frame update
    public override void Awake()
    {
        assignedEnemy=gameObject.GetComponent<Enemy>();
        // Debug.Log($"{assignedHero.getHeroName()} init");
        GameObject heroCanvas = gameObject.transform.Find("hero_canvas").gameObject;
        hpSlider=heroCanvas.transform.Find("hpSlider").GetComponent<Slider>();
        eventText=heroCanvas.transform.Find("eventText").GetComponent<Text>();
        hpSlider.maxValue=assignedEnemy.getHealth();
        Debug.Log($"{assignedEnemy.getHeroName()} {assignedEnemy.getHealth()} init end");
    }

    public override void Update()
    {
        hpSlider.value=assignedEnemy.getHealth();
    }
    // Update is called once per frame
    // void Update()
    // {
    //     hpSlider.value=assignedHero.getHealth();
    // }

    // IEnumerator showGuiEvent(float time){
    //     eventText.enabled=true;
    //     yield return new WaitForSeconds(time);
    //     eventText.enabled=false;
    // }

    // public void displayGuiEvent(string eventVal){
    //     eventText.text=eventVal;
    //     StartCoroutine(showGuiEvent(20));
    // }
}
