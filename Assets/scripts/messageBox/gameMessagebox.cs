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
    public static GameObject msgBox;

    //Utworz wartosci MessageBox i wyswietl na srodku canvasu
    public static void createMessageBox(string title, string content, UnityAction okButtonClick=null){
        msgBox = Resources.Load("messages/messageBox") as GameObject;
        setBox(title,content,okButtonClick);
    }
    private static void okButtonOnClickEvent(GameObject obj){
        showEntities(true);
        Destroy(obj);
    }

    public static void createDialogBox(string title, string content,UnityAction okButtonClick=null){
        msgBox = Resources.Load("messages/dialogBox") as GameObject;
        setBox(title,content,okButtonClick);
    }

    private static void setBox(string title,string content,UnityAction okButtonClick=null){
        GameObject _canvas = GameObject.FindGameObjectsWithTag("mainCanvas")[0].gameObject;
        GameObject messageBox = Instantiate(msgBox,_canvas.transform);
        RectTransform refxd = msgBox.GetComponent<RectTransform>();
        RectTransform newxd = messageBox.GetComponent<RectTransform>();
        newxd.anchorMin = refxd.anchorMax;
        newxd.anchorMin = refxd.anchorMin;
        newxd.anchoredPosition = refxd.anchoredPosition;
        newxd.sizeDelta = refxd.sizeDelta;
        Button okBtn = messageBox.transform.GetComponentInChildren<Button>();
        messageBox.GetComponent<MessageBox>().setMessageBox(title,content);
        if(okButtonClick!=null){
            okBtn.onClick.AddListener(()=>okButtonClick());
        }
        showEntities(false);
        okBtn.onClick.AddListener(()=>okButtonOnClickEvent(messageBox));
    }

    public static void  showEntities(bool state){
        if(mainEnemiesUnit.Instance.getUnitsAsGameObject()!=null){
            foreach(var u in mainEnemiesUnit.Instance.getUnitsAsGameObject()){
                u.GetComponent<Image>().enabled=state;
            }
        }
        if(mainPlayerUnit.Instance.getUnitsAsGameObject()!=null){
            foreach(var u in mainPlayerUnit.Instance.getUnitsAsGameObject()){
                u.GetComponent<Image>().enabled=state;
            }
        }
    }
}
