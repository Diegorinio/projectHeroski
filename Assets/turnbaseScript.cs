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

    // Update is called once per frame
    void Update()
    {
        selectedCheck=isSelected;
        if (!selectedGameObject)
        {
            quequeHeroes[turn].GetComponent<characterController>().selectHero();
        }
        if (selectedGameObject.GetComponent<Enemy>())
        {
            selectedGameObject.GetComponent<enemyAI>().moveToRandomDirecion();
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
    }
}
