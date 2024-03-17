using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Glowny skrypt odpowiadajacy za kolejkowanie tury 
public class turnbaseScript : MonoBehaviour
{
    //Tile ktory zostal wcisniety do pathfinidingu
    public static Tile selectedTile;
    //Wybrany statyczny obiekt dostepny do wszystkich klas
    public static GameObject selectedGameObject;

    public GameObject _testSelectedGameObject;


    //sprawdzenei czy obiekt jest wybrany
    public static bool isSelected;
    [SerializeField]
    //dana tura 
    private int turn;
    // [SerializeField]
    //Lista Obiektow w kolejce
    private List<GameObject> quequeHeroes= new List<GameObject>();
    private Queue<GameObject> turnQueue = new Queue<GameObject>();
    [SerializeField]
    // private Text turnText;
    // [SerializeField]
    //zmienan odpowiedzialna za runde
    private int round=1;
    //GUI do skryptu wyswietlajace informacje
    turnBaseScriptGUI _gui;

    [SerializeField]
    //zmienan odpowiedzialna czy gra zostala ukonczone( przez brak przeciwnikow lub brak jednostek gracza)
    private bool isFinished=false;

    private battleManager BattleManager;

    //zmienna odpowiedzialna za sprawdzanie gdy gracz wygral
    private bool isWin=false;

    
    
    // Przed startem znajdz komponent GUI i zwroc jednostki gracza i przeciwnika z instacji mainPlayerUnit
    void Awake()
    {
        _gui=gameObject.GetComponent<turnBaseScriptGUI>();
       GameObject[] findPlayer = mainPlayerUnit.Instance.getUnitsAsGameObject();
       quequeHeroes.AddRange(findPlayer);
        GameObject[] findEnemies=mainEnemiesUnit.Instance.getUnitsAsGameObject();
        quequeHeroes.AddRange(findEnemies);
        quequeHeroes = quequeHeroes.OrderBy(x=>Guid.NewGuid()).ToList();
        BattleManager = gameObject.GetComponent<battleManager>();

    }

    //Na starcie uruchom kurtyne odpowiedzialna za wyswietlanie panelu rundy
    void Start(){
        StartCoroutine(roundStart());
    }

    public int getTurn(){
        return turn;
    }
    public bool checkIsFinished(){
        return isFinished;
    }

    //Metoda odpowiedzialna za nastepna ture
    public void nextTurn(){
        isSelected=false;
        selectedGameObject=null;
        checkGameState();
        if(!isFinished){
        Debug.Log($"Queque heroes size: {quequeHeroes.Count} and id is {turn}");
        if(turnQueue.Count==0){
            round++;
            StartCoroutine(roundStart());
        }
        else{
            GameObject nextUnit = turnQueue.Dequeue();
            turnQueue.Enqueue(nextUnit);
            setTurn();
        }
        }
        else{
            if(isWin){
                _gui.gameStateGameOver("Win");
            }
            else{
                _gui.gameStateGameOver("Lose");
            }
        }
    }

    //Sprawdz czy dana jednostka nalezy do gracza
    public static bool IsHeroTurn(){
        if(selectedGameObject!=null){
        return selectedGameObject.CompareTag("Player");
        }
        else{
            return false;
        }
    }

    //Ustawia ture, odpala metode selectUnit z kontrolera danej jednostki
    public void setTurn(){
            if(!selectedGameObject)
                selectedGameObject=turnQueue.Peek();
            if(IsHeroTurn()){
                BattleManager.spellButtonsEnable(true);
            }
            else{
                BattleManager.spellButtonsEnable(false);
            }
            if(!IsHeroTurn()){
                checkGameState();
            }
            _gui.setUpTurnPanel(turnQueue.ToList());
            Debug.Log(turnQueue.Peek().transform.name);
            StartCoroutine(waitForNextUnit(0.5f));
    }

    //Usun GameObject jednostki z tury
    public void removeFromQueque(GameObject gObj){
        quequeHeroes.Remove(gObj);
        int indexToRemove=-1;
        for(int i=0;i<turnQueue.Count;i++){
            if(turnQueue.ElementAt(i)==gObj){
                indexToRemove=i;
                break;
            }
        }
        if(indexToRemove!=-1){
            List<GameObject> tmp = turnQueue.ToList();
            tmp.RemoveAt(indexToRemove);
            turnQueue=new Queue<GameObject>(tmp);
        }
        // turnQueue = new Queue<GameObject>(quequeHeroes);
    }

    //Odpowiada za wyswietelenie panelu startu tury 
    IEnumerator roundStart(){
        _gui.setGUI(round);
        _gui.showPanel(true);
        yield return new WaitForSeconds(_gui.panelShowTime);
        _gui.showPanel(false);
        if(quequeHeroes.Count>0){
            foreach(GameObject unit in quequeHeroes){
                turnQueue.Enqueue(unit);
            }
            setTurn();
        }
    }

    //Sprawdza czy w grze są nadaj jednostki przeciwnika i gracza, jezeli ktorejs juz nie ma to koniec gry
    public void checkGameState(){
        int enemies=0,heroes=0;
        foreach(var i in quequeHeroes){
            if(i.CompareTag("Player")){
                heroes++;
            }
            else if(i.CompareTag("Enemy")){
                enemies++;
            }
        }
        if(heroes<=0){
            Debug.Log($"Heroes lose");
            isFinished=true;
            isWin=false;
        }
        else if(enemies<=0){
            Debug.Log($"Heroes win");
            isFinished=true;
            isWin=true;
        }
        if(isFinished){
        if(isWin){
                _gui.gameStateGameOver("Win");
            }
            else{
                _gui.gameStateGameOver("Lose");
            }
        }
    }

    //Po opusczzeniu walki dezaktywuje jednostki gracza( glownie zeby nie byly widoczne)
    void OnDisable(){
        mainEnemiesUnit.Instance.clearEnemyTeamList();
        foreach(var p in mainPlayerUnit.Instance.getUnitsAsGameObject()){
            p.SetActive(false);
        }
    }

    IEnumerator waitForNextUnit(float timer){
        yield return new WaitForSeconds(timer);
        Debug.Log($"Aktualna tura {turn} a rozmiar listy {quequeHeroes.Count}");
        checkGameState();
        if(isFinished){
            nextTurn();
        }
        else{
            turnQueue.Peek().GetComponent<unitController>().selectUnit();
            if(!IsHeroTurn()){
                int rnd = UnityEngine.Random.Range(0,6);
            if(rnd>=6){
            EnemyHeroBehaviour.Instance.CastRandomSpell();
            EnemyHeroBehaviour.Instance.setIsEnemyCasted(false);
            }
            }
        }
    }

    IEnumerator startingBattleScreen(float timer){
        yield return new WaitForSeconds(timer);
    }
}