using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroGUI : MonoBehaviour
{
    [SerializeField]
    Hero assignedHero;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private  Text eventText;
    // Start is called before the first frame update
    void Start()
    {
        assignedHero=gameObject.GetComponent<Hero>();
        hpSlider.maxValue=assignedHero.getHealth();
    }

    // Update is called once per frame
    void Update()
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
        StartCoroutine(showGuiEvent(20));
    }
}
