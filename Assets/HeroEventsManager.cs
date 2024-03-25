using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroEventsManager : MonoBehaviour
{

    public Hero assignedHero;
    public GameObject heroEventBox;
    private GameObject heroEventImage;
    public GameObject heroDialogBox;
    private GameObject heroDialogText;


    public void setEventBox(Image img){
        enableEventBox();
        if(heroEventImage!=null){
            heroEventImage.GetComponent<Image>().sprite = img.sprite;
        }
    }

    private void enableEventBox(){
        heroEventBox.SetActive(true);
        heroEventImage = findEventImage();
    }
    private GameObject findEventImage(){
        if(heroEventBox.activeInHierarchy){
            return heroEventBox.transform.Find("eventImage").gameObject;
        }
        else{
            return null;
        }
    }


    public void setDialogBox(string text){
        enableDialogBox();
        if(heroDialogText!=null){
            heroDialogText.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    private void enableDialogBox(){
        heroDialogBox.SetActive(true);
        heroDialogText = findDialogText();
    }

    private GameObject findDialogText(){
        if(heroDialogBox.activeInHierarchy){
            return heroDialogBox.transform.Find("dialogText").gameObject;
        }
        else{
            return null;
        }
    }
}
