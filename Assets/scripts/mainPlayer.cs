using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPlayer : MonoBehaviour
{
    //jak narazie na staticach
    public static List<GameObject> teamHeroes;
    [SerializeField]
    private List<GameObject> heroes;
    // Start is called before the first frame update

    void Awake(){
        DontDestroyOnLoad(this);
        teamHeroes=heroes;
        // Debug.Log(teamHeroes[0].GetComponent<Hero>().getHeroName());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addToTeam(GameObject teamMember){
        Debug.Log($"added to team new hero {teamMember.GetComponent<Hero>().getHeroName()} with class {teamMember.GetComponent<Role>().roleName}");
        teamHeroes.Add(teamMember);
    }
}
