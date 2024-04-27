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
    public List<Sprite> charactersImages;
    //Wartosci takie jak tytyl i wiadomosc
    [SerializeField]
    private TextMeshProUGUI messageTitle;
    [SerializeField]
    private TextMeshProUGUI messageContent;
    //Button zamykajacy
    private GameObject okButton;
    // Start is called before the first frame update
    void Awake()
    {
        //Znajdz teksty i poustawiaj wartosci
        messageTitle = gameObject.transform.Find("messageTitle").GetComponent<TextMeshProUGUI>();
        messageContent = gameObject.transform.Find("messageContent").GetComponent<TextMeshProUGUI>();
        okButton = gameObject.transform.Find("okButton").gameObject;
        gameObject.transform.Find("Image").GetComponent<Image>().sprite = getRandomImage();

    }
    private Sprite getRandomImage(){
        return charactersImages[Random.Range(0,charactersImages.Count)];
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
