using System;
using UnityEngine;
using UnityEngine.UI;


//Klasa generujaca jednostkix
public class unitSpawner : MonoBehaviour
{

    //Statyczna jako ze dostepna zawsze gdy potrzeba a w sumie potrzeba tylko jedna instacje

    //Zaladowanie GameObjectu Unit jako template
    public static GameObject unitTemplate=Resources.Load("Templates/UnitTemplates/unitTemplate") as GameObject;

    //Typy jednostek i jaki kontroler
    public enum unitType{Distance,Cavalery,Close};
    public enum controllers{Player,Enemy}
    public enum tier{T1,T2,T3};

    //Zespawnij dany typ i dana ilosc
    //główne użycie w koszarach
    public static GameObject spawnUnitGameObject(unitType type,int amount){
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        switch(type){
            case unitType.Distance:
            newUnit.AddComponent<distanceU>();
            break;
            case unitType.Close:
            newUnit.AddComponent<closeU>();
            break;
            case unitType.Cavalery:
            newUnit.AddComponent<cavaleryU>();
            break;
        }
        newUnit.transform.Find("unit_sprite").GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<Unit>().unitSprite;
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.GetComponent<Unit>().SetUnitType((int)type);
        newUnit.AddComponent<unitGUI>();
        newUnit.name=$"{type} {amount}";
        return newUnit;
    }

    //Zespawnij jednostke jako gameObject z wyborem kontrolera(player, enemy) i iloscia jednostek
    public static GameObject spawnUnitGameObject(unitType type, controllers controller,int amount){
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        switch(type){
            case unitType.Distance:
            newUnit.AddComponent<distanceU>();
            break;
            case unitType.Close:
            newUnit.AddComponent<closeU>();
            break;
            case unitType.Cavalery:
            newUnit.AddComponent<cavaleryU>();
            break;
        }
        newUnit.GetComponent<Unit>().SetUnitType((int)type);
        assignController(controller, newUnit);
        newUnit.transform.Find("unit_sprite").GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<Unit>().unitSprite;
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        newUnit.name=$"{type} {amount}";
        return newUnit;
    }

    //Jednostka z tierem //uzywane glownie do spawnu jednostek gracza
    public static GameObject spawnUnitGameObject(tier _tier,unitType type, controllers controller,int amount){
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        UnitSO _wantedUnitSO = getUnitSO(_tier,type);
        switch(type){
            case unitType.Distance:
            newUnit.AddComponent<distanceU>();
            break;
            case unitType.Close:
            newUnit.AddComponent<closeU>();
            break;
            case unitType.Cavalery:
            newUnit.AddComponent<cavaleryU>();
            break;
        }
        newUnit.GetComponent<Unit>().SetUnitType((int)type);
        newUnit.GetComponent<Unit>().unitInitialize(getTier(_tier),_wantedUnitSO);
        assignController(controller, newUnit);
        newUnit.transform.Find("hero_canvas").transform.Find("unit_sprite").GetComponent<Image>().sprite= newUnit.GetComponent<Unit>().unitSprite;
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        newUnit.name=$"{controller} {_tier} {type} {amount}";
        return newUnit;
    }

    public static UnitSO getUnitSO(tier _tier, unitType type){
        UnitSO returnUnitSO=null;
        switch(type){
            case unitType.Distance:
            returnUnitSO = Resources.Load<UnitSO>($"Units/{_tier}/distance{_tier}");
            break;
            case unitType.Close:
            returnUnitSO = Resources.Load<UnitSO>($"Units/{_tier}/close{_tier}");
            break;
            case unitType.Cavalery:
            returnUnitSO = Resources.Load<UnitSO>($"Units/{_tier}/cavalery{_tier}");
            break;
        }
        Debug.Log($"FOund SO is {returnUnitSO.unitName} {returnUnitSO.unitSprite}");
        return returnUnitSO;
    }

    private static int getTier(tier _tier){
        switch(_tier){
            case tier.T1:
            return 1;
            case tier.T2:
            return 2;
            case tier.T3:
            return 3;
            default:
            return 1;
        }
    }

    // Dodaj komponenty potrzebne jednostce gracza
    private static void assignPlayerAttributes(GameObject obj){
        obj.AddComponent<Detector>();
    }

    private static void removePlayerAttributes(GameObject obj){
        Destroy(obj.GetComponent<Detector>());
    }

    // Dodaj komponenety potrzebne jednostce przeciwnika
    private static void assignEnemyAttributes(GameObject obj){
        obj.AddComponent<enemyAI>();
        obj.AddComponent<Enemy>();
        obj.AddComponent<enemyDetector>();
        obj.transform.Find("hero_canvas").transform.Find("cardColor").GetComponent<Image>().color=Color.red;
        obj.transform.tag="Enemy";
    }

    private static void removeEnemyAttributes(GameObject obj){
        Destroy(obj.GetComponent<enemyAI>());
        Destroy(obj.GetComponent<Enemy>());
        Destroy(obj.GetComponent<enemyDetector>());
    }


    //Utworz losowy typ jednostki
    public static GameObject spawnRandomUnitToGameObject(){
        unitType type = (unitType)UnityEngine.Random.Range(0,3);
        int amount = UnityEngine.Random.Range(100,200);
        GameObject newUnit = spawnUnitGameObject(type,amount);
        newUnit.GetComponent<Unit>().setUnitAmount(UnityEngine.Random.Range(100,200));
        return newUnit;
    }

    //Losowa jednostka z tierem
    //Glowne zastowoanie do losowej jednostki na map event
    public static GameObject spawnRandomUnitGameObject(tier _tier,controllers controller,int amount){
        GameObject newUnit = Instantiate(unitTemplate,new Vector3(0,0,0),Quaternion.identity);
        unitType type = (unitType)UnityEngine.Random.Range(0,3);
        UnitSO _wantedUnitSO = getUnitSO(_tier,type);
        switch(type){
            case unitType.Distance:
            newUnit.AddComponent<distanceU>();
            break;
            case unitType.Close:
            newUnit.AddComponent<closeU>();
            break;
            case unitType.Cavalery:
            newUnit.AddComponent<cavaleryU>();
            break;
        }
        newUnit.GetComponent<Unit>().unitInitialize(getTier(_tier),_wantedUnitSO);
        assignController(controller, newUnit);
        newUnit.transform.Find("hero_canvas").transform.Find("unit_sprite").GetComponent<Image>().sprite= newUnit.GetComponent<Unit>().unitSprite;
        newUnit.GetComponent<Unit>().setUnitAmount(amount);
        newUnit.AddComponent<unitGUI>();
        newUnit.name=$"{controller} {type} {amount} {_tier}";
        return newUnit;
    }

    public static void assignController(controllers controller, GameObject obj){
        switch(controller){
            case controllers.Player:
            assignPlayerAttributes(obj);
            break;
            case controllers.Enemy:
            assignEnemyAttributes(obj);
            break;
        }
    }

    public static void removeController(controllers controller, GameObject obj){
        switch(controller){
            case controllers.Player:
            removePlayerAttributes(obj);
            break;
            case controllers.Enemy:
            removeEnemyAttributes(obj);
            break;
        }
    }

    public static Color colorCardByTag(GameObject obj){
        string tag = obj.transform.tag;
        if(tag=="Player"){
            return Color.green;
        }
        else{
            return Color.red;
        }
    }

    //Utworz lososwy typ jednostki z wyborem czy to jednostka przeciwna czy gracza
    public static GameObject spawnRandomUnitToGameObject(controllers controller){
        GameObject _newUnit = spawnRandomUnitToGameObject();
        switch(controller){
            case controllers.Player:
            assignPlayerAttributes(_newUnit);
            break;
            case controllers.Enemy:
            assignEnemyAttributes(_newUnit);
            break;
        }
        _newUnit.name +=$" {controller}";
        _newUnit.SetActive(false);
        return _newUnit;
    }
}
