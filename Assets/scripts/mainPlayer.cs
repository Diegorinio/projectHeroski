using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPlayer : MonoBehaviour
{
    public static mainPlayer Instance{get;private set;}
    //jak narazie na staticach
    public List<GameObject> heroesTeam;

    public List<GameObject> enemyTeam;
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
        return heroesTeam.ToArray();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHeroToTeam(GameObject teamMember){
        //size tylko chwilowo
        if(heroesTeam.Count<5){
        Debug.Log($"added to team new hero {teamMember.GetComponent<Hero>().getHeroName()} with class {teamMember.GetComponent<Role>().roleName}");
        heroesTeam.Add(teamMember);
        }
    }

    public void addHeroesToTeamList(List<GameObject> members){
        if(heroesTeam.Count<5){
            heroesTeam.AddRange(members);
        }
    }

    public void addEnemyToTeam(GameObject member){
        if(enemyTeam.Count<5){
            enemyTeam.Add(member);
        }
    }
    public void addEnemiesToTeamList(List<GameObject> members){
        if(heroesTeam.Count<5){
            enemyTeam.AddRange(members);
        }
    }

}
