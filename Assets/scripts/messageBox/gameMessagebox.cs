using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
