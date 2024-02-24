using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Glowny komponent wywolujacy Box informacyjny
public class gameMessagebox : MonoBehaviour
{
    //Prefab MessageBox
    public static GameObject msgBox = Resources.Load("messages/messageBox") as GameObject;

    //Utworz wartosci MessageBox i wyswietl na srodku canvasu
    public static void createMessageBox(string title, string content, UnityAction okButtonClick=null){
        GameObject _canvas = GameObject.FindAnyObjectByType<Canvas>().gameObject;
        GameObject messageBox = Instantiate(msgBox,_canvas.transform);
        RectTransform refxd = msgBox.GetComponent<RectTransform>();
        RectTransform newxd = messageBox.GetComponent<RectTransform>();
        newxd.anchorMin = refxd.anchorMax;
        newxd.anchorMin = refxd.anchorMin;
        newxd.anchoredPosition = refxd.anchoredPosition;
        newxd.sizeDelta = refxd.sizeDelta;
        Button okBtn = messageBox.transform.GetComponentInChildren<Button>();
        messageBox.GetComponent<MessageBox>().setMessageBox(title,content);
        // messageBox.transform.GetComponentInChildren<Button>().onClick.AddListener(okButtonClick);
        if(okButtonClick==null){
            okBtn.onClick.AddListener(()=>okButtonOnClickEvent(messageBox));
        }
        else{
            okBtn.onClick.AddListener(()=>okButtonClick());
        }
    }
    private static void okButtonOnClickEvent(GameObject obj){
        Destroy(obj);
    }
}
