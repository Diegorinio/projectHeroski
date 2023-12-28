using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainEnemies : MonoBehaviour
{
    public static mainEnemies Instance {get;private set;}
    [SerializeField]
    private List<GameObject> enemiesInMap;
    // Start is called before the first frame update
    void Awake(){
        if(Instance==null){
        Instance=this;
        DontDestroyOnLoad(gameObject);
        // teamHeroes=heroes;
        }
        // else if(SceneManager.GetActiveScene().name=="city"){
        //     GameObject me = gameObject;
        //     SceneManager.MoveGameObjectsToScene(me,SceneManager.GetSceneByName("map"));
        // }
        // else{
        //     Destroy(gameObject);
        // }
        // Debug.Log(teamHeroes[0].GetComponent<Hero>().getHeroName());
    }
    public GameObject[] getEnemies(){
        return enemiesInMap.ToArray();
    }
    void Start()
    {
        
    }

    public void addToEnemiesList(List<GameObject> enemies){
        // //size tylko chwilowo
        // if(enemiesInMap.Count<5){
        //     enemiesInMap=enemies;
        // }
        enemiesInMap=enemies;
        Debug.Log($"enemies in map size: {enemiesInMap.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
