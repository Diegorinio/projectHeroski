using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnbaseScript : MonoBehaviour
{
    public static GameObject selectedGameObject;
    public GameObject temp_gameobject;
    public static bool isSelected;
    public bool selectedCheck;
    [SerializeField]
    private int turn;
    [SerializeField]
    private List<GameObject> quequeHeroes= new List<GameObject>();
    [SerializeField]
    // private Text turnText;
    // [SerializeField]
    private int round=1;
    turnBaseScriptGUI _gui;
    // Start is called before the first frame update
    void Awake()
    {
        _gui=gameObject.GetComponent<turnBaseScriptGUI>();
       GameObject[] findPlayer = mainPlayer.teamHeroes.ToArray();
            foreach (GameObject o in findPlayer)
            {
                quequeHeroes.Add(o);
            } 
        findPlayer = GameObject.FindGameObjectsWithTag("Enemy");
        quequeHeroes.AddRange(findPlayer);
        // turnText=GameObject.Find("roundText").GetComponent<Text>();
        // turnText.text=round.ToString();
    }

    void Start(){
        StartCoroutine(roundStart());
    }
    // Update is called once per frame
    void Update()
    {
        selectedCheck=isSelected;
        temp_gameobject=selectedGameObject;
        // if(!IsHeroTurn()){
        //     selectedGameObject.GetComponent<enemyAI>().randomAction();
        // }
    }
    public int getTurn(){
        return turn;
    }
    public void nextTurn(){
        isSelected=false;
        selectedGameObject=null;
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
        // StartCoroutine(roundStart());
    }

    public static bool IsHeroTurn(){
        // if(selectedGameObject.GetComponent<Enemy>()){
        //     return false;
        // }
        return selectedGameObject.GetComponent<Hero>();
    }

    public void setTurn(){
        // if (!selectedGameObject)
        // {
            // _gui.setGUI(round);
            if(!selectedGameObject)
                selectedGameObject=quequeHeroes[turn];
            // if(!IsHeroTurn()){
            //     quequeHeroes[turn].GetComponent<characterController>().selectHero();
            // }
            // else{
            quequeHeroes[turn].GetComponent<characterController>().selectHero();
        // }
    }

    public void removeFromQueque(GameObject gObj){
        quequeHeroes.Remove(gObj);
    }
    IEnumerator roundStart(){
        _gui.setGUI(round);
        _gui.showPanel(true);
        yield return new WaitForSeconds(_gui.panelShowTime);
        _gui.showPanel(false);
        setTurn();
    }
}