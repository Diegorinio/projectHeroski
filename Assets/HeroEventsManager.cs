using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroEventsManager : MonoBehaviour
{
    public delegate void boxEventComplete();
    public static event boxEventComplete BoxEventComplete;

    public Hero assignedHero;
    public GameObject heroEventBox;
    private GameObject heroEventImage;
    public GameObject heroDialogBox;
    private GameObject heroDialogText;

    public IEnumerator createBoxEvent(Sprite eventImage){
        CreateBoxEvent(eventImage);
        yield return new WaitForSeconds(2);
        disableEventBox();
    }

    private void OnBoxEventComplete(){
        BoxEventComplete?.Invoke();
    }

    public void CreateBoxEvent(Sprite eventSprite){
        setEventBox(eventSprite);
    }

    public void CreateDialogEvent(Hero _hero){
        string tekst = _hero.getHeroEntryDialog();
        setDialogBox(tekst);
    }


    private void setEventBox(Sprite img){
        enableEventBox();
        if(heroEventImage!=null){
            heroEventImage.GetComponent<Image>().sprite = img;
        }
    }

    private void enableEventBox(){
        heroEventBox.SetActive(true);
        heroEventImage = findEventImage();
    }

    private void disableEventBox(){
        heroEventBox.SetActive(false);
    }


    private GameObject findEventImage(){
        if(heroEventBox.activeInHierarchy){
            return heroEventBox.transform.Find("eventImage").gameObject;
        }
        else{
            return null;
        }
    }


    private void setDialogBox(string text){
        enableDialogBox();
        if(heroDialogText!=null){
            heroDialogText.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    private void enableDialogBox(){
        heroDialogBox.SetActive(true);
        heroDialogText = findDialogText();
    }

    private void disableDialogBox(){
        heroEventBox.SetActive(false);
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
