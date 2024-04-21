using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LVL : MonoBehaviour
{
    public Button activator;
    public GameObject Lock;
    public int stars;
    public GameObject Starplace;
    public GameObject StarFrom;
    private void OnEnable()
    {
        randomMapEventGenerator rndEvent = gameObject.AddComponent<randomMapEventGenerator>();
        StarFrom = GameObject.Find("MANAGER");
        activator.onClick.AddListener(()=>{TaskOnClick(rndEvent);});
        if (PlayerPrefs.HasKey($"{this.name} Stars"))
        {
            stars = PlayerPrefs.GetInt($"{this.name} Stars");
            Starplace.GetComponent<Image>().sprite = StarFrom.GetComponent<LoobyMapManager>().stars[stars];
        }
        else
        {
            stars = 0;
            Starplace.GetComponent<Image>().sprite = StarFrom.GetComponent<LoobyMapManager>().stars[stars];
        }
        if (this.name.Substring(0, 4) == "BOSS"){
            if (stars == 0)
            {
                Starplace.SetActive(false);

            }
        }
        

    }
    private void TaskOnClick(randomMapEventGenerator _rndevent)
    {
        if (Lock != null)
        {
            if (Lock.activeSelf)
            {
                //animacja :>
                return;
            }
        }
        //tu przechodzi do walki i mo�e animacja
        PlayerPrefs.SetString("BattleNow", this.name);
        print($"WALKA : {this.name}");
        _rndevent.goToFight("battleScene");

    }

}
