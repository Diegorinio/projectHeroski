using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleMangerUI : MonoBehaviour
{
    public GameObject turnQueuePanel;
    private List<GameObject> turnQueueElements;
    public Sprite cardImage;
    // Start is called before the first frame update
    void Start()
    {
        turnQueueElements = new List<GameObject>();
    }

    public void setUpTurnPanel(List<GameObject> unitsList){
        if(turnQueueElements.Count>0){
            clearTurnQueueElements();
        }
        for(int id=0;id<unitsList.Count;id++){
            GameObject _unitTurnCardImage = setUpCardForUnit(unitsList[id]);
            GameObject  _unitImage = new GameObject($"TurnElement {id}");
            _unitImage.AddComponent<Image>().sprite = unitsList[id].GetComponent<Unit>().getUnitImage().sprite;
            _unitTurnCardImage.transform.SetParent(turnQueuePanel.transform);
            _unitImage.transform.SetParent(_unitTurnCardImage.transform);
            _unitTurnCardImage.GetComponent<RectTransform>().localScale = Vector3.one;
            turnQueueElements.Add(_unitTurnCardImage);
        }
    }

    private void clearTurnQueueElements(){
        for(int id=0;id<turnQueueElements.Count;id++){
            Destroy(turnQueueElements[id]);
        }
    }

    private GameObject setUpCardForUnit(GameObject unit){
        GameObject _newCard  = new GameObject("unitCard");
        Image _newCardImg = _newCard.AddComponent<Image>();
        _newCardImg.sprite = cardImage;
        _newCardImg.color = unitSpawner.colorCardByTag(unit);
        return _newCard;
    }
}
