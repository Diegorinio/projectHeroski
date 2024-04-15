using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class turnBaseScriptGUI : MonoBehaviour
{
    public Text text;
    //Panel konca gry
    [SerializeField]
    GameObject gameStatePanel;
    [SerializeField]
    //Panel startu rundy
    GameObject roundStartPanel;
    [SerializeField]
    //tekst rundy na glownym ekranie walki
    private Text roundTextGameUI;
    [SerializeField]
    //tekst rundy na panelu startowym rundy
    private Text roundTextPanelUI;
    // [SerializeField]
    // czas przez ktory panel startu rundy jest widoczny
    public float panelShowTime;
    public battleMangerUI battleUI;

    void Awake(){
        roundTextGameUI=GameObject.Find("roundText").GetComponent<Text>();
        roundTextPanelUI=roundStartPanel.transform.Find("roundTextPanel").GetComponent<Text>();
        battleUI = GetComponent<battleMangerUI>();
        // turnQueueElements = new List<GameObject>();
    }

    //Ustaw wartosci rund
    public void setGUI(int round){
        roundTextGameUI.text=round.ToString();
        roundTextPanelUI.text=round.ToString();
    }
    //Pokaz panel
    public void showPanel(bool state){
        roundStartPanel.SetActive(state);
    }

    // public void setUpTurnPanel(List<GameObject> unitsList){
    //     if(turnQueueElements.Count()>0){
    //         clearTurnQuequeList();
    //     }
    //     for(int id=0;id<unitsList.Count;id++){
    //         GameObject _unitTurnImage = new GameObject($"TurnElemnt {id}");
    //         _unitTurnImage.AddComponent<Image>().sprite = unitsList[id].GetComponent<Unit>().getUnitImage().sprite;
    //         _unitTurnImage.transform.SetParent(turnQueuePanel.transform);
    //         _unitTurnImage.GetComponent<RectTransform>().localScale = Vector3.one;
    //         turnQueueElements.Add(_unitTurnImage);
    //     }
    // }

    // private void clearTurnQuequeList(){
    //     for(int id=0;id<turnQueueElements.Count();id++){
    //         Destroy(turnQueueElements[id]);
    //     }
    // }

    
    //Pokaz ekran konca gry i kto wygral
    public void gameStateGameOver(string winner,int round){
        Text state = gameStatePanel.transform.Find("gameStateText").gameObject.GetComponent<Text>();
        // GameObject.Find("movement_panel").SetActive(false);
        int gwiazki = 4 - round;
        if (gwiazki <= 0 || gwiazki>3)gwiazki = 1;
        text.gameObject.SetActive(true);
        text.GetComponent<Text>().text = $"ZDOBY£EŒ {gwiazki} Gwiazdek GRATULUJE";
        //bierze gwiazdki od rund bym zmieni³ bo rundy d³ugo traja chyba xd
        if (PlayerPrefs.GetInt($"LVL 1 Stars") <= gwiazki) // PlayerPrefs.GetInt($"{this.name} Stars"); syntax dawanie gwiazdek 0,1,2,3|| 3 to  3 gwiazki
        {//                       ^ TEN lvl1 to placeholder! TRZEBA ZMIENIÆ BY BRA£O JAKI LVL ZOSTA£ WYBRANY  
            PlayerPrefs.SetInt($"LVL 1 Stars", gwiazki);
            //odblokowuje kolejny poziom XD

        }
        state.text = $"ZDOBY£EŒ {gwiazki} Gwiazdek GRATULUJE";;
        gameStatePanel.SetActive(true);
    }

    //Pokaz panel startu rundy przez dany czas
    public IEnumerator roundStart(float time){
        roundStartPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        roundStartPanel.SetActive(false);
    }
}