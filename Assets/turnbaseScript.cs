using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnbaseScript : MonoBehaviour
{
    public static GameObject selectedGameObject;
    public static bool isSelected;
    public bool selectedCheck;
    [SerializeField]
    private int turn;
    [SerializeField]
    private List<GameObject> quequeHeroes= new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
       GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject o in findPlayer)
            {
                quequeHeroes.Add(o);
            } 
        findPlayer = GameObject.FindGameObjectsWithTag("Enemy");
        quequeHeroes.AddRange(findPlayer);
    }

    void Start(){
        setTurn();
    }
    // Update is called once per frame
    void Update()
    {
        selectedCheck=isSelected;
        if(!IsHeroTurn()){
            selectedGameObject.GetComponent<enemyAI>().randomAction();
        }
    }
    public int getTurn(){
        return turn;
    }
    public void nextTurn(){
        isSelected=false;
        selectedGameObject=null;
        if(turn==quequeHeroes.Count-1){
            turn=0;
        }
        else{
            turn++;
        }
        setTurn();
    }

    public static bool IsHeroTurn(){
        // if(selectedGameObject.GetComponent<Enemy>()){
        //     return false;
        // }
        return selectedGameObject.GetComponent<Hero>();
    }

    public void setTurn(){
        if (!selectedGameObject)
        {
            quequeHeroes[turn].GetComponent<characterController>().selectHero();
        }
    }
}