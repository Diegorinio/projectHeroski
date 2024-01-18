using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMessagebox : MonoBehaviour
{
    public static GameObject msgBox = Resources.Load("messages/messageBox") as GameObject;

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
