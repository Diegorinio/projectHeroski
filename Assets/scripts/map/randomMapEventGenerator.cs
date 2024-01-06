using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class randomMapEventGenerator : characterGenerator
{
    [SerializeField]
    private List<GameObject> enemiesList=new List<GameObject>();
    [SerializeField]
    private List<string> enemiesListNames=new List<string>();
    [SerializeField]
    // private GameObject eventPanel;
    Text eventNameText;
    Text eventEnemiesText;
    string[] eventNames = {"test","Kutang klan", "XD", "Potężna wichura"};
    string[] eventEnemies={"Chleb","Mieso","Wszystko","Gomez","Enemy1","test"};
    Button fightBtn;

    void Start(){
        // Generator=gameObject.transform.parent.GetComponent<characterGenerator>();
        // eventPanel=gameObject.transform.Find("eventInfoPanel").gameObject;
        // eventNameText=eventPanel.transform.Find("eventName").GetComponent<Text>();
        // eventNameText.text=eventNames[Random.Range(0,eventNames.Length-1)];
        // eventEnemiesText = eventPanel.transform.Find("eventEnemies").GetComponent<Text>();
        // fightBtn=eventPanel.transform.Find("goToFightBtn").GetComponent<Button>();
        // fightBtn.onClick.AddListener(goToFight);
        // generateEvent();
        int amount = Random.Range(1,4);
        for(int x=0;x<amount;x++){
            enemiesListNames.Add(eventNames[Random.Range(0,eventNames.Length-1)]);
        }
    }
    public void generateEvent(){
        // for(int x=0;x<amount;x++){
        //     string _name = eventEnemies[Random.Range(0,eventEnemies.Length-1)];
        //     GameObject newEnemy = generateRandomCharacter(characterType.Enemy,_name);
        //     Enemy _enemy = newEnemy.GetComponent<Enemy>();
        //     _enemy.setHeroHealth(Random.Range(101,150));
        //     enemiesList.Add(newEnemy);
        // }
        foreach(var name in enemiesListNames){
            GameObject newEnemy = generateRandomCharacter(characterType.Enemy,name);
            Enemy _enemy = newEnemy.GetComponent<Enemy>();
            _enemy.setHeroHealth(Random.Range(101,160));
            enemiesList.Add(newEnemy);
            newEnemy.transform.SetParent(mainPlayer.Instance.gameObject.transform);
            newEnemy.transform.localPosition=Vector3.zero;
        }
    }
    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     if(eventPanel.activeInHierarchy)
    //         eventPanel.SetActive(false);
    //     else{
    //         eventPanel.SetActive(true);
    //     }
    // }

    public void goToFight(string biom){
        generateEvent();
        // foreach(var e in enemiesList){
        //     e.transform.SetParent(mainPlayer.Instance.gameObject.transform);
        // }
        mainPlayer.Instance.addEnemiesToTeamList(enemiesList);
        SceneManager.LoadScene("battleScene");
    }
}
