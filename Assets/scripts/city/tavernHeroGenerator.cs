using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tavernHeroGenerator : MonoBehaviour
{
    public GameObject template;
    //ui elements
    Image _heroImg;
    Text _heroNameText;
    Text _heroRoleText;
    [SerializeField]
    Sprite heroImage;
    [SerializeField]
    string heroName;
    // [SerializeField]
    // Role role;
    [SerializeField]
    string[] names = {"Abuin","Ibdomar","Kalid","Benhari","Al","Shariri"};
    [SerializeField]
    string[] roles = {"knight","mage","piechota"};
    [SerializeField]
    string type;
    // List<Role> roles = new List<Role>();
    // Start is called before the first frame update

    void Awake(){
        _heroImg=gameObject.transform.Find("heroImage").GetComponent<Image>();
        _heroRoleText=gameObject.transform.Find("hero_role").GetComponent<Text>();
        _heroNameText=gameObject.transform.Find("hero_name").GetComponent<Text>();
    }
    void Start()
    {
        heroName=names[Random.Range(0,names.Length-1)];
        _heroNameText.text=heroName;
        // roles[Random.Range(0,roles.Length-1)];
        _heroRoleText.text=roles[Random.Range(0,roles.Length-1)];
        type=_heroRoleText.text;
        // switch(type){
        //     case "knight":
        //     role=new Knight();
        //     break;
        //     case "mage":
        //     role=new Mage();
        //     break;
        //     case "piechota":
        //     role=new Piechota();
        //     break;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        GameObject newHero= Instantiate(template,Vector3.zero,Quaternion.identity);
        Hero tmpHero = newHero.GetComponent<Hero>();
        tmpHero.setHeroName(heroName);
        tmpHero.setHeroHealth(100);
        switch(type){
            case "knight":
            newHero.AddComponent<Knight>();
            break;
            case "mage":
            newHero.AddComponent<Mage>();
            break;
            case "piechota":
            newHero.AddComponent<Piechota>();
            break;
        }
        Debug.Log($"Character added: {tmpHero.getHeroName()} with role {newHero.GetComponent<Role>().roleName}");
        mainPlayer.addToTeam(newHero);
        newHero.transform.name=heroName;
        newHero.transform.SetParent(GameObject.Find("mainPlayerManager").gameObject.transform);
        newHero.transform.position=GameObject.Find("mainPlayerManager").gameObject.transform.position;
        // newHero.transform.position=
        // newHero.SetActive(false);
        Destroy(gameObject);
    }
}
