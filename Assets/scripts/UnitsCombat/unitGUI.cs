using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitGUI : MonoBehaviour
{
[SerializeField]
    Unit _unit;
    // [SerializeField]
    // public Slider hpSlider{get;set;}
    private  Text eventText;
    private Text unitAmountText;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        _unit=gameObject.GetComponent<Unit>();
        Debug.Log($"{_unit.unitName} init");
        GameObject heroCanvas = gameObject.transform.Find("hero_canvas").gameObject;
        unitAmountText = heroCanvas.transform.Find("unitAmountText").GetComponent<Text>();
        // hpSlider=heroCanvas.transform.Find("hpSlider").GetComponent<Slider>();
        eventText=heroCanvas.transform.Find("eventText").GetComponent<Text>();
        // hpSlider.maxValue=_unit.getTotalHealth();
        Debug.Log($"{_unit.unitName} init end");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        unitAmountText.text = _unit.getUnitAmount().ToString();
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
