using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroEventsManager : MonoBehaviour
{
    public delegate void boxEventCompleteHandle();
    public static event boxEventCompleteHandle BoxEventComplete;

    public delegate void dialogEventCompleteHandle();
    public static event dialogEventCompleteHandle DialogEventComplete;

    //player
    public GameObject playerEventBox;
    //Enemy
    public GameObject enemyEventBox;

    private GameObject heroEventImage;
    private GameObject tmpEventBox;

    public GameObject enemyDialogBox;
    public GameObject playerDialogBox;
    private GameObject heroDialogText;
    private GameObject tmpDialogBox;

    public enum casterType{Player,Enemy};


    //EVENT BOX

    public void CreateBoxEvent(casterType heroCastType,Sprite eventSprite){
        switch(heroCastType){
            case casterType.Player:
            tmpEventBox = playerEventBox;
            break;
            case casterType.Enemy:
            tmpEventBox = enemyEventBox;
            break;
        }
        StartCoroutine(createBoxEvent(eventSprite));
    }


    private IEnumerator createBoxEvent(Sprite eventImage){
        createBox(eventImage);
        yield return new WaitForSeconds(1.8f);
        disableEventBox();
        OnBoxEventComplete();
    }

    private void createBox(Sprite eventSprite){
        setEventBox(eventSprite);
    }


    private void setEventBox(Sprite img){
        enableEventBox();
        if(heroEventImage!=null){
            heroEventImage.GetComponent<Image>().sprite = img;
        }
    }

    private void enableEventBox(){
        tmpEventBox.SetActive(true);
        heroEventImage = findEventImage();
    }

    private void disableEventBox(){
        tmpEventBox.SetActive(false);
    }

    private GameObject findEventImage(){
        if(tmpEventBox.activeInHierarchy){
            return tmpEventBox.transform.Find("eventImage").gameObject;
        }
        else{
            return null;
        }
    }

    private void OnBoxEventComplete(){
        BoxEventComplete?.Invoke();
    }


    //DIALOG BOX


    public void CreateDialogEvent(Hero _hero){
        string _heroTag = _hero.getHeroTag();
        switch(_heroTag){
            case "Player":
            tmpDialogBox = playerDialogBox;
            break;
            case "Enemy":
            tmpDialogBox = enemyDialogBox;
            break;
        }
        string tekst = _hero.getHeroEntryDialog();
        setDialogBox(tekst);
        StartCoroutine(createDialogEvent(tekst));
    }

    private IEnumerator createDialogEvent(string txt){
        createDialog(txt);
        yield return new WaitForSeconds(1.8f);
        disableDialogBox();
        OnDialogEventComplete();
    }

    private void createDialog(string txt){
        setDialogBox(txt);
    }
    private void setDialogBox(string text){
        enableDialogBox();
        if(heroDialogText!=null){
            heroDialogText.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    private void enableDialogBox(){
        tmpDialogBox.SetActive(true);
        heroDialogText = findDialogText();
    }

    private void disableDialogBox(){
        tmpDialogBox.SetActive(false);
    }

    private GameObject findDialogText(){
        if(tmpDialogBox.activeInHierarchy){
            return tmpDialogBox.transform.Find("dialogText").gameObject;
        }
        else{
            return null;
        }
    }

    private void OnDialogEventComplete(){
        DialogEventComplete?.Invoke();
    }
}
