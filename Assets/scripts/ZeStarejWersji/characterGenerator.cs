using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterGenerator : MonoBehaviour
{
    public enum characterType{Hero,Enemy};
    [SerializeField]
    // private characterType charType;
    public enum characterClass{Knight,Mage,Piechota};
    private GameObject heroTemplate;
    // [SerializeField]
    private GameObject enemyTemplate;
    string[] names = {"Abuin","Ibdomar","Kalid","Benhari","Al","Shariri"};
    // Start is called before the first frame update

    void Awake(){
        heroTemplate=Resources.Load("Templates/hero/heroTemplate") as GameObject;
        enemyTemplate=Resources.Load("Templates/enemy/enemyTemplate")as GameObject;
    }

    public GameObject generateRandomCharacter(characterType type){
        GameObject newCharacter= null;
        switch(type){
            case characterType.Hero:
            newCharacter = Instantiate(heroTemplate);
            // tmpCharacter=(Hero)newCharacter.GetComponent<Hero>();
            break;
            case characterType.Enemy:
            newCharacter =Instantiate(enemyTemplate);
            // tmpCharacter=(Enemy)newCharacter.GetComponent<Enemy>();
            break;
        }
        Debug.Log($"char type {type}");
        characterClass charClass = (characterClass)Random.Range(0,3);
        addClassComponent(newCharacter, charClass);
        Hero _newCharacter = newCharacter.GetComponent<Hero>();
        setUpCharacter(_newCharacter);
        Debug.Log($"{_newCharacter.getHeroName()}");
        newCharacter.name=_newCharacter.getHeroName();
        return newCharacter;
    }

    public GameObject generateRandomCharacter(characterType type, string name){
                GameObject newCharacter= null;
        switch(type){
            case characterType.Hero:
            newCharacter = Instantiate(heroTemplate);
            // tmpCharacter=(Hero)newCharacter.GetComponent<Hero>();
            break;
            case characterType.Enemy:
            newCharacter =Instantiate(enemyTemplate);
            // tmpCharacter=(Enemy)newCharacter.GetComponent<Enemy>();
            break;
        }
        Debug.Log($"char type {type}");
        characterClass charClass=(characterClass)Random.Range(0,3);
        addClassComponent(newCharacter, charClass);
        Hero _newCharacter = newCharacter.GetComponent<Hero>();
        _newCharacter.setHeroName(name);
        // Debug.Log($"{_newCharacter.getHeroName()}");
        newCharacter.name=_newCharacter.getHeroName();
        return newCharacter;
    }

    public GameObject generateFromData(string name, characterType type, characterClass _c){
        GameObject newCharacter= null;
        switch(type){
            case characterType.Hero:
            newCharacter = Instantiate(heroTemplate);
            // tmpCharacter=(Hero)newCharacter.GetComponent<Hero>();
            break;
            case characterType.Enemy:
            newCharacter =Instantiate(enemyTemplate);
            // tmpCharacter=(Enemy)newCharacter.GetComponent<Enemy>();
            break;
        }
        Debug.Log($"char type {type}");
        characterClass charClass=_c;
        addClassComponent(newCharacter, charClass);
        Hero _newCharacter = newCharacter.GetComponent<Hero>();
        _newCharacter.setHeroName(name);
        // Debug.Log($"{_newCharacter.getHeroName()}");
        newCharacter.name=_newCharacter.getHeroName();
        return newCharacter;
    }

    public void addClassComponent(GameObject character,characterClass chClass){
        switch(chClass){
            case characterClass.Knight:
            character.AddComponent<Knight>();
            break;
            case characterClass.Mage:
            character.AddComponent<Mage>();
            break;
            case characterClass.Piechota:
            character.AddComponent<Piechota>();
            break;
        }
    }

    private void setUpCharacter(Hero character){
        character.setHeroName(names[Random.Range(0,names.Length-1)]);
        Role r = character.gameObject.GetComponent<Role>();
        character.setRole(r.roleName);
    }

    public string getRandomName(){
        return names[Random.Range(0,names.Length-1)];
    }

    public characterClass getRandomClass(){
        return (characterClass)Random.Range(0,3);
    }
    
}
