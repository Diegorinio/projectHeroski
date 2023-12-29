using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class randomMapEventGenerator : MonoBehaviour,IPointerClickHandler
{
    characterGenerator Generator;
    public GameObject template;
    [SerializeField]
    private List<GameObject> enemiesList;
    [SerializeField]
    private GameObject eventPanel;
    Text eventNameText;
    Text eventEnemiesText;
    [SerializeField]
    string[] eventNames = {"test","Kutang klan", "XD", "Potężna wichura"};
    [SerializeField]
    string[] names = {"Chleb","Mieso","Wszystko","Gomez","Enemy1","test"};
    // Start is called before the first frame update
    string type="";
    string[] roles = {"knight","mage","piechota"};
    Button fightBtn;

    void Awake(){
        Generator=gameObject.transform.parent.GetComponent<characterGenerator>();
        eventPanel=gameObject.transform.Find("eventInfoPanel").gameObject;
        eventNameText=eventPanel.transform.Find("eventName").GetComponent<Text>();
        eventNameText.text=eventNames[Random.Range(0,eventNames.Length-1)];
        eventEnemiesText = eventPanel.transform.Find("eventEnemies").GetComponent<Text>();
        fightBtn=eventPanel.transform.Find("goToFightBtn").GetComponent<Button>();
    }

    void OnMouseDown(){
        if(eventPanel.activeInHierarchy)
            eventPanel.SetActive(false);
        else
            eventPanel.SetActive(true);
    }
    void Start()
    {
        generateEventEnemies();
        fightBtn.onClick.AddListener(goToFight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateEventEnemies(){
        int enemiesAmount = Random.Range(1,4);
        Debug.Log($"{enemiesAmount}");
        for(int x=0;x<enemiesAmount;x++){
            Debug.Log("enemy initialize"+enemiesAmount.ToString()+" "+x.ToString());
            GameObject newEnemy = Generator.generateRandomCharacter(characterGenerator.characterType.Enemy);
            Enemy tmpEnemy = newEnemy.GetComponent<Enemy>();
            string name = names[Random.Range(0,names.Length-1)];
            // string name=names[Random.Range(0,names.Length-1)];
            tmpEnemy.setHeroName(name);
            tmpEnemy.setHeroHealth(120);
            // type=roles[Random.Range(0,roles.Length-1)];
            // switch(type){
            //     case "knight":
            //     newEnemy.AddComponent<Knight>();
            //     break;
            //     case "mage":
            //     newEnemy.AddComponent<Mage>();
            //     break;
            //     case "piechota":
            //     newEnemy.AddComponent<Piechota>();
            //     break;
            // }
            newEnemy.transform.name=name;
            eventEnemiesText.text+=$"\n{name}";
            // newEnemy.transform.position=mainPlayer.Instance.gameObject.transform.position;
            // newEnemy.transform.SetParent(mainEnemies.Instance.gameObject.transform);
            // mainEnemies.Instance.addToEnemiesList(newEnemy);
            enemiesList.Add(newEnemy);
        }
        // mainEnemies.Instance.addToEnemiesList(enemiesList);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventPanel.activeInHierarchy)
            eventPanel.SetActive(false);
        else{
            eventPanel.SetActive(true);
            mainEnemies.Instance.addToEnemiesList(enemiesList);
        }
    }

    void goToFight(){
        foreach(var e in enemiesList){
            e.transform.SetParent(mainEnemies.Instance.gameObject.transform);
        }
        mainEnemies.Instance.addToEnemiesList(enemiesList);
        SceneManager.LoadScene(3);
    }
}
