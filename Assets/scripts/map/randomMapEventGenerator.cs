using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class randomMapEventGenerator : characterGenerator,IPointerClickHandler
{
    // characterGenerator Generator;
    // public GameObject template;
    [SerializeField]
    private List<GameObject> enemiesList;
    // [SerializeField]
    private GameObject eventPanel;
    Text eventNameText;
    Text eventEnemiesText;
    // [SerializeField]
    string[] eventNames = {"test","Kutang klan", "XD", "Potężna wichura"};
    string[] eventEnemies={"Chleb","Mieso","Wszystko","Gomez","Enemy1","test"};
    // [SerializeField]
    // string[] names = {"Chleb","Mieso","Wszystko","Gomez","Enemy1","test"};
    // // Start is called before the first frame update
    // string type="";
    // string[] roles = {"knight","mage","piechota"};
    Button fightBtn;

    void Start(){
        // Generator=gameObject.transform.parent.GetComponent<characterGenerator>();
        eventPanel=gameObject.transform.Find("eventInfoPanel").gameObject;
        eventNameText=eventPanel.transform.Find("eventName").GetComponent<Text>();
        eventNameText.text=eventNames[Random.Range(0,eventNames.Length-1)];
        eventEnemiesText = eventPanel.transform.Find("eventEnemies").GetComponent<Text>();
        fightBtn=eventPanel.transform.Find("goToFightBtn").GetComponent<Button>();
        fightBtn.onClick.AddListener(goToFight);
    }
    public void generateEvent(){
        int amount = Random.Range(1,4);
        for(int x=0;x<amount;x++){
            string _name = eventEnemies[Random.Range(0,eventEnemies.Length-1)];
            GameObject newEnemy = generateRandomCharacter(characterType.Enemy,_name);
            Enemy _enemy = newEnemy.GetComponent<Enemy>();
            _enemy.setHeroHealth(Random.Range(101,150));
            enemiesList.Add(newEnemy);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventPanel.activeInHierarchy)
            eventPanel.SetActive(false);
        else{
            eventPanel.SetActive(true);
            generateEvent();
        }
    }

    void goToFight(){
        foreach(var e in enemiesList){
            e.transform.SetParent(mainPlayer.Instance.gameObject.transform);
        }
        // mainEnemies.Instance.addToEnemiesList(enemiesList);
        mainPlayer.Instance.addEnemiesToTeamList(enemiesList);
        SceneManager.LoadScene(3);
    }
}
