using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Glowny komponent wywolujacy Box informacyjny
public class gameMessagebox : MonoBehaviour
{
    //Prefab MessageBox
    public static GameObject msgBox = Resources.Load("messages/messageBox") as GameObject;

    //Utworz wartosci MessageBox i wyswietl na srodku canvasu
    public static void createMessageBox(string title, string content){
        GameObject messageBox = Instantiate(msgBox,msgBox.transform.position,Quaternion.identity);
        RectTransform refxd = msgBox.GetComponent<RectTransform>();
        RectTransform newxd = messageBox.GetComponent<RectTransform>();
        newxd.anchorMin = refxd.anchorMax;
        newxd.anchorMin = refxd.anchorMin;
        newxd.anchoredPosition = refxd.anchoredPosition;
        newxd.sizeDelta = refxd.sizeDelta;
        messageBox.GetComponent<MessageBox>().setMessageBox(title,content);
    }
}
