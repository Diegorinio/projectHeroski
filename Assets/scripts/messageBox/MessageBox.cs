using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
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

    private GameObject background1;
    // Start is called before the first frame update
    void Awake()
    {
        //Znajdz teksty i poustawiaj wartosci
        Transform alignPanel = gameObject.transform.Find("alignPanel").transform;
        messageTitle = alignPanel.Find("messageTitle").GetComponent<TextMeshProUGUI>();
        messageTitle.color = Color.white;
        // messageTitle.fontStyle=FontStyles.Bold;
        messageContent = alignPanel.Find("messageContent").GetComponent<TextMeshProUGUI>();
        messageContent.color = Color.white;
        // messageContent.fontStyle=FontStyles.Bold;
        okButton = alignPanel.Find("okButton").gameObject;
        if (GameObject.Find("messageBox(Clone)") != null)
        {
            background = GameObject.Find("messageBox(Clone)");
            GameObject city = GameObject.Find("CityManager");
             background.GetComponent<Image>().sprite = city.GetComponent<CityManager>().tablica;
        }

        if (GameObject.Find("dialogBox(Clone)") != null)
        {
            GameObject city = GameObject.Find("CityManager");
            background1 = GameObject.Find("dialogBox(Clone)");
            background1.GetComponent<Image>().sprite = city.GetComponent<CityManager>().tablica;
        }
        //print("123488888");
    }

    private void OnEnable()
    {
        //GameObject msgTitle = GameObject.Find("messageTitle");
        //msgTitle.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((float)-1.5,383,0);

        //GameObject msgContent = GameObject.Find("messageContent");
        //msgContent.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((float)-1.5,-200,0);
        //okButton.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(0,-300,0);
        okButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("BTN1");
        okButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        //print("1234");



        
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
