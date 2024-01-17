using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnBaseScriptGUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameStatePanel;
    [SerializeField]
    GameObject roundStartPanel;
    [SerializeField]
    private Text roundTextGameUI;
    [SerializeField]
    private Text roundTextPanelUI;
    // [SerializeField]
    public float panelShowTime;

    void Awake(){
        // roundStartPanel=GameObject.Find("roundStartPanel");
        roundTextGameUI=GameObject.Find("roundText").GetComponent<Text>();
        roundTextPanelUI=roundStartPanel.transform.Find("roundTextPanel").GetComponent<Text>();
        // panelShowTime=1.5f;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator roundStart(float time){
        roundStartPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        roundStartPanel.SetActive(false);
    }
}
