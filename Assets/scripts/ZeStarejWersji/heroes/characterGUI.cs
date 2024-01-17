using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterGUI : MonoBehaviour
{
    [SerializeField]
    Hero assignedHero;
    [SerializeField]
    public Slider hpSlider{get;set;}
    [SerializeField]
    public  Text eventText;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        assignedHero=gameObject.GetComponent<Hero>();
        Debug.Log($"{assignedHero.getHeroName()} init");
        GameObject heroCanvas = gameObject.transform.Find("hero_canvas").gameObject;
        hpSlider=heroCanvas.transform.Find("hpSlider").GetComponent<Slider>();
        eventText=heroCanvas.transform.Find("eventText").GetComponent<Text>();
        hpSlider.maxValue=assignedHero.getHealth();
        Debug.Log($"{assignedHero.getHeroName()} init end");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        hpSlider.value=assignedHero.getHealth();
    }

    IEnumerator showGuiEvent(float time){
        eventText.enabled=true;
        yield return new WaitForSeconds(time);
        eventText.enabled=false;
    }

    public void displayGuiEvent(string eventVal){
        eventText.text=eventVal;
        StartCoroutine(showGuiEvent(2));
    }
}