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
    [SerializeField]
    // private characterClass charClass;
    public GameObject heroTemplate;
    public GameObject enemyTemplate;
    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject generateRandomCharacter(characterType type){
        GameObject newCharacter=null;
        switch(type){
            case characterType.Hero:
            newCharacter = Instantiate(heroTemplate,Vector3.zero,Quaternion.identity);
            // tmpCharacter=(Hero)newCharacter.GetComponent<Hero>();
            break;
            case characterType.Enemy:
            newCharacter =Instantiate(enemyTemplate,Vector3.zero,Quaternion.identity);
            // tmpCharacter=(Enemy)newCharacter.GetComponent<Enemy>();
            break;
        }
        Debug.Log($"char type {type}");
        characterClass charClass = (characterClass)Random.Range(0,3);
        newCharacter = addClassComponent(newCharacter, charClass);
        return newCharacter;
    }

    public GameObject addClassComponent(GameObject character,characterClass chClass){
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

        return character;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
