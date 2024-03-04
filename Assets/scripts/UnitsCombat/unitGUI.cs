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
    private Image unitImageSprite;
    bool isMoveFinished=false;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        _unit=gameObject.GetComponent<Unit>();
        GameObject heroCanvas = gameObject.transform.Find("hero_canvas").gameObject;
        unitAmountText = heroCanvas.transform.Find("unitAmountText").GetComponent<Text>();
        eventText=heroCanvas.transform.Find("eventText").GetComponent<Text>();
        unitImageSprite = heroCanvas.transform.Find("unit_sprite").GetComponent<Image>();
        unitImageSprite.sprite = _unit.getUnitSprite();
    }

    //Pokazuje ilosc jednostek, mozna to pozniej zmienic na metode wywolujaca zmiane ale mi sie nie chce
    public virtual void Update()
    {
        unitAmountText.text = _unit.getUnitAmount().ToString();
    }

    //Coroutine pokazujace event dla jednostki(leczenie, strata)
    IEnumerator showGuiEvent(float time){
        eventText.enabled=true;
        yield return new WaitForSeconds(time);
        eventText.enabled=false;
        // gameObject.GetComponent<unitController>().disableClickable(); 
    }

    //Metoda glowna do wyswietlania eventu
    public void displayGuiEvent(string eventVal){
        eventText.text=eventVal;
        StartCoroutine(showGuiEvent(2));
    }

    // IEnumerator showAnimEvent(float time,unitController atk){
    //     // battleManager.battleAnimPanel.GetComponent<SpiteAnimator>().setDamageText(dmg);
    //     battleManager.battleAnimPanel.SetActive(true);
    //     // SpiteAnimator animatorSprite = battleManager.battleAnimPanel.GetComponent<SpiteAnimator>();
    //     battleManager.battleAnimPanel.GetComponent<SpiteAnimator>().Func_PlayUIAnim();
    //     // animatorSprite.
    //     yield return new WaitUntil(()=>battleManager.battleAnimPanel.GetComponent<SpiteAnimator>().isAnimDone());
    //     battleManager.battleAnimPanel.SetActive(false);
    //     // isMoveFinished=true;
    //     atk.disableClickable();
    //     Debug.Log("Koniec scenki przejscie do nastepnego gracza");
    // }
    // public void displayAnimEvent(int dmg,GameObject attacker, GameObject victim){
    //     SpiteAnimator animatorSprite = battleManager.battleAnimPanel.GetComponent<SpiteAnimator>();
    //     Image _attacker = attacker.GetComponent<Unit>().getUnitImage();
    //     //  _victim.sprite = victim.GetComponent<Unit>().getUnitSprite();
    //     Image _victim = victim.GetComponent<Unit>().getUnitImage();
    //     animatorSprite.setAnimator(_attacker,_victim,dmg);
    //     // battleManager.battleAnimPanel.GetComponent<SpiteAnimator>().setDamageText(dmg);
    //     StartCoroutine(showAnimEvent(2f,attacker.GetComponent<unitController>()));
    //     // attacker.GetComponent<unitController>().disableClickable();
    // }

}
