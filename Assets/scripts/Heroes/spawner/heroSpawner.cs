using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class heroSpawner : MonoBehaviour
{
    public enum HeroController{Player,Enemy}
    public static List<Object> heroesSO;

    public static void LoadHeroes(){
        heroesSO = Resources.LoadAll("Heroes",typeof(heroSO)).ToList();
        Debug.Log($"HeroesSO len chuj2137 {heroesSO.Count}");
    }

    public static heroSO getHeroSoByID(int id)
    {
        LoadHeroes();
        heroSO resultSO = null;
        foreach(heroSO h in heroesSO){
            if(h.heroID==id){
                resultSO = h;
                break;
            }
        }
        return resultSO;
    }

    public static GameObject spawnHeroGameObject(int id,HeroController controller){
        heroSO spawningHero = getHeroSoByID(id);
        Debug.Log($"heroesSO len chuuuj {heroesSO.Count} and {heroesSO[0].GetType()}");
        GameObject heroObject = new GameObject(spawningHero.heroName);
        heroObject.AddComponent<Hero>();
        heroObject.GetComponent<Hero>().assignHeroSO(spawningHero);
        heroObject.GetComponent<Hero>().setHeroTag(controller.ToString());
        return heroObject;
    }
}

