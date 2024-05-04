using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Prefab MessageBox 
public class MessageBox : MonoBehaviour
{
    //Wartosci takie jak tytyl i wiadomosc
    [SerializeField]
    private TextMeshProUGUI messageTitle;
    [SerializeField]
    private TextMeshProUGUI messageContent;
    //Button zamykajacy
    private GameObject okButton;
    private GameObject background;
    // Start is called before the first frame update
    void Awake()
    {
        //Znajdz teksty i poustawiaj wartosci
        messageTitle = gameObject.transform.Find("messageTitle").GetComponent<TextMeshProUGUI>();
        messageTitle.color = Color.white;
        messageTitle.fontStyle=FontStyles.Bold;
        messageContent = gameObject.transform.Find("messageContent").GetComponent<TextMeshProUGUI>();
        messageContent.color = Color.white;
        messageContent.fontStyle=FontStyles.Bold;
        okButton = gameObject.transform.Find("okButton").gameObject;
        background = GameObject.Find("messageBox(Clone)");
        GameObject city = GameObject.Find("CityManager");
        background.GetComponent<Image>().sprite = city.GetComponent<CityManager>().tablica;

    }
    private void OnEnable()
    {
        GameObject msgTitle = GameObject.Find("messageTitle");
        msgTitle.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((float)-1.5,383,0);

        GameObject msgContent = GameObject.Find("messageContent");
        msgContent.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((float)-1.5,-200,0);
        okButton.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(0,-300,0);
        okButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("BTN1");
        okButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;



        
    }

    //Metody do ustawiania tytulu,wiadomosci albo wszystkiego naraz
    //Metoda odpowiedzialna za klikniecie buttona OK
    public void setMessageTitle(string title){
        messageTitle.text = title;
    }
    public void setMessageContent(string content){
        messageContent.text=content;
    }
    public void setMessageBox(string title,string content){
        messageTitle.text=title;
        messageContent.text=content;
    }
}
