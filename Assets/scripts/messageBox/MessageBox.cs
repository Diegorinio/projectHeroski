using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageTitle;
    [SerializeField]
    private TextMeshProUGUI messageContent;
    private GameObject okButton;
    // Start is called before the first frame update
    void Awake()
    {
        messageTitle = gameObject.transform.Find("messageTitle").GetComponent<TextMeshProUGUI>();
        messageContent = gameObject.transform.Find("messageContent").GetComponent<TextMeshProUGUI>();
        okButton = gameObject.transform.Find("okButton").gameObject;
        // Dobra walic to zrobie zeby tylko dzialalo
        if(GameObject.FindFirstObjectByType<Canvas>()){
            gameObject.transform.SetParent(GameObject.FindAnyObjectByType<Canvas>().transform);
            gameObject.transform.localScale = Vector2.one;
        }
        okButton.GetComponent<Button>().onClick.AddListener(okButtonOnClickEvent);

    }
    public void okButtonOnClickEvent(){
        Destroy(gameObject);
    }
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
