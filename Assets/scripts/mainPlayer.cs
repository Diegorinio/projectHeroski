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
        teamHeroes=heroes;
        Debug.Log(teamHeroes[0].GetComponent<Hero>().getHeroName());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
