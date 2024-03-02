using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Glowny skrypt odpowiadajacy za kolejkowanie tury 
public class turnbaseScript : MonoBehaviour
{
    //Tile ktory zostal wcisniety do pathfinidingu
    public static Tile selectedTile;
    //Wybrany statyczny obiekt dostepny do wszystkich klas
    public static GameObject selectedGameObject;
    //chwilowy obiekt zeby sprawdzic czy dziala w inspektorze
    public GameObject temp_gameobject;
    //sprawdzenei czy obiekt jest wybrany
    public static bool isSelected;
    //chwilowa zmienan zeby sprawdzic w inspektorze
    public bool selectedCheck;
    [SerializeField]
    //dana tura 
    private int turn;
    // [SerializeField]
    //Lista Obiektow w kolejce
    private List<GameObject> quequeHeroes= new List<GameObject>();
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
        BattleManager = gameObject.GetComponent<battleManager>();

    }

    //Na starcie uruchom kurtyne odpowiedzialna za wyswietlanie panelu rundy
    void Start(){
        StartCoroutine(roundStart());
    }
    
    // Do wyrzucenia
    void Update()
    {
        selectedCheck=isSelected;
        temp_gameobject=selectedGameObject;
    }
    public int getTurn(){
        return turn;
    }

    //Metoda odpowiedzialna za nastepna ture
    public void nextTurn(){
        isSelected=false;
        selectedGameObject=null;
        checkGameState();
        if(!isFinished){
        Debug.Log($"Queque heroes size: {quequeHeroes.Count} and id is {turn}");
        if(turn>=quequeHeroes.Count-1){
            turn=0;
            round++;
        }
        else{
            turn++;
        }
        // turnText.text = round.ToString();
        Debug.Log($"state 2 {turn}");
        if(turn==0){
            StartCoroutine(roundStart());
        }
        else{
        setTurn();
        }
        }
        else{
            if(isWin){
                Debug.Log($"Koniec gry player wins");
                _gui.gameStateGameOver("Win");
            }
            else{
                Debug.Log($"Koniec gry enemies win");
                _gui.gameStateGameOver("Lose");
            }
        }
    }

    //Sprawdz czy dana jednostka nalezy do gracza
    public static bool IsHeroTurn(){
        return selectedGameObject.CompareTag("Player");
    }

    //Ustawia ture, odpala metode selectUnit z kontrolera danej jednostki
    public void setTurn(){
            if(!selectedGameObject)
                selectedGameObject=quequeHeroes[turn];
            if(IsHeroTurn())
                BattleManager.spellButtonsEnable(true);
            else
                BattleManager.spellButtonsEnable(false);
            StartCoroutine(waitForNextUnit(0.2f));
            // quequeHeroes[turn].GetComponent<unitController>().selectUnit();
    }

    //Usun GameObject jednostki z tury
    public void removeFromQueque(GameObject gObj){
        quequeHeroes.Remove(gObj);
    }

    //Odpowiada za wyswietelenie panelu startu tury 
    IEnumerator roundStart(){
        _gui.setGUI(round);
        _gui.showPanel(true);
        yield return new WaitForSeconds(_gui.panelShowTime);
        _gui.showPanel(false);
        setTurn();
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
    }

    //Po opusczzeniu walki dezaktywuje jednostki gracza( glownie zeby nie byly widoczne)
    void OnDisable(){
        mainEnemiesUnit.Instance.clearEnemyTeamList();
        foreach(var p in mainPlayerUnit.Instance.getUnitsAsGameObject()){
            p.SetActive(false);
        }
    }

    IEnumerator waitForNextUnit(float timer){
        // quequeHeroes[turn].GetComponent<unitController>().selectUnit();
        yield return new WaitForSeconds(timer);
         quequeHeroes[turn].GetComponent<unitController>().selectUnit();
    }
}