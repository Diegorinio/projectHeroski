using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPlayer : MonoBehaviour
{
    public static mainPlayer Instance{get;private set;}
    //jak narazie na staticach
    public List<GameObject> teamHeroes;
    // Start is called before the first frame update
    void Awake(){
        if(Instance==null){
        Instance=this;
        DontDestroyOnLoad(gameObject);
        // teamHeroes=heroes;
        }
        // else{
        //     Destroy(gameObject);
        // }
        // Debug.Log(teamHeroes[0].GetComponent<Hero>().getHeroName());
    }

    public GameObject[] getHeroes(){
        return teamHeroes.ToArray();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToTeam(GameObject teamMember){
        if(teamHeroes.Count<5){
        Debug.Log($"added to team new hero {teamMember.GetComponent<Hero>().getHeroName()} with class {teamMember.GetComponent<Role>().roleName}");
        teamHeroes.Add(teamMember);
        }
    }
}
