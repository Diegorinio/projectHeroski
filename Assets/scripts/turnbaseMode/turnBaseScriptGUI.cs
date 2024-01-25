using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnBaseScriptGUI : MonoBehaviour
{
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

    void Awake(){
        roundTextGameUI=GameObject.Find("roundText").GetComponent<Text>();
        roundTextPanelUI=roundStartPanel.transform.Find("roundTextPanel").GetComponent<Text>();
    }

    public void setGUI(int round){
        roundTextGameUI.text=round.ToString();
        roundTextPanelUI.text=round.ToString();
    }

    public void showPanel(bool state){
        roundStartPanel.SetActive(state);
    }

    public void gameStateGameOver(string winner){
        Text state = gameStatePanel.transform.Find("gameStateText").gameObject.GetComponent<Text>();
        state.text = winner;
        gameStatePanel.SetActive(true);
        GameObject.Find("moveblocks").SetActive(false);
    }

    public IEnumerator roundStart(float time){
        roundStartPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        roundStartPanel.SetActive(false);
    }
}
