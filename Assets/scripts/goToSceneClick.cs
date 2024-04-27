using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class goToSceneClick : MonoBehaviour
{
    //for tutorial
    private List<GameObject> Enemies = new List<GameObject>();
    private GameObject enemyHero;
    //Glownie do koliderow bo jestem lewniwy
    [SerializeField]
    private string sceneID;
    //Zaladuj scene
    public async void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "Menu") await Task.Delay(300);
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
        Debug.Log($"go to scene {sceneID}");
    }

    public void Tutorial_GoTo_Battle(){
        if(mainPlayerUnit.Instance.getSelectedHero()!=null && mainPlayerUnit.Instance.getUnitsList().Count>0){
            setEventUnits();

            if(mainPlayerUnit.Instance.getUnits().Length>0){
        setEventUnits();
        Debug.Log($"Random event generator!");
        foreach(var e in Enemies){
            Unit _unit = e.GetComponent<Unit>();
            mainEnemiesUnit.Instance.addUnitsToTeam(_unit);
        }
        mainEnemiesUnit.Instance.assignHeroToTeam(enemyHero.GetComponent<Hero>());
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
        
        }
    }
     else{
            gameMessagebox.createDialogBox("Prepare","Hire general and rectuit any unit to start");
            Debug.Log("Nie da sie ");
        }
    }
    void setEventUnits(){
        for(int x=0;x<1;x++){
        // GameObject newEnemy = unitSpawner.spawnRandomUnitToGameObject(unitSpawner.controllers.Enemy);
        GameObject newEnemy = unitSpawner.spawnRandomUnitGameObject(unitSpawner.tier.T1,unitSpawner.controllers.Enemy,Random.Range(100,300));
        newEnemy.transform.SetParent(mainEnemiesUnit.Instance.gameObject.transform);
        newEnemy.transform.localPosition=Vector3.zero;
        Enemies.Add(newEnemy);
        enemyHero = heroSpawner.spawnHeroGameObject(0,heroSpawner.HeroController.Enemy);

        }
    }
}
