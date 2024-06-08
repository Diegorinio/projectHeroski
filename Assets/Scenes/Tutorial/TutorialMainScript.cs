using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMainScript : MonoBehaviour
{
    public GameObject showGoldArrow;
    public GameObject showTawernArrow;
    public GameObject showBarracksArrow;
    public GameObject showEQArrow;
    public GameObject battleArrow;
    public GameObject CityHallArrow;
    void Start()
    {
        PrefsManager.clearPrefs();
        Tutorial_City_LaunchDialog();
    }

    private void  Tutorial_City_LaunchDialog(){
        gameMessagebox.createDialogBox("Welcome","Welcome in Dual Chash, i'm Justine and  let me show you basics",Tutorial_City_About_City);
    }
    private void Tutorial_City_About_City(){
        gameMessagebox.createDialogBox("City","We are in city right now, you can find some buildings here, let me show you and talk about them",Tuotorial_City_About_Gold);
    }
    private void Tuotorial_City_About_Gold(){
        ShowArrow(showGoldArrow);
        gameMessagebox.createDialogBox("Gold","There is panel with gold that you have, you can spend gold on new generals and units. Here you go, get some gold to start",Tutorial_City_CityHall);
        // PrefsManager.addGold(400);
    }
    private void Tutorial_City_CityHall(){
        ShowArrow(CityHallArrow);
        HideArrow(showGoldArrow);
        gameMessagebox.createDialogBox("City Hall","Enter city hall to get collected gold from mine",Tutorial_City_About_Tawern);
    }

    private void Tutorial_City_About_Tawern(){
        HideArrow(CityHallArrow);
        ShowArrow(showTawernArrow);
        gameMessagebox.createDialogBox("Tawern","Tawern is right here, inside you can hire generals for your team to fight alongside your team",Tutorial_City_About_Barracks);
    }

    private void Tutorial_City_About_Barracks(){
        HideArrow(showTawernArrow);
        ShowArrow(showBarracksArrow);
        gameMessagebox.createDialogBox("Barracks","In barracks you can recruit units to your team",Tutorial_City_About_EQ_Panel);
    }

    private void Tutorial_City_About_EQ_Panel(){
        HideArrow(showBarracksArrow);
        ShowArrow(showEQArrow);
        gameMessagebox.createDialogBox("Team","In this panel you can see your assigned General and your actual units state", Tutorial_City_About_Enter_Battle);
    }

    private void Tutorial_City_About_Enter_Battle(){
        HideArrow(showEQArrow);
        ShowArrow(battleArrow);
        gameMessagebox.createDialogBox("Battle Map","This is battle map, right there you can fight to earn some gold and rise up in Dueling League",Tutorial_City_Ending);
    }

    private void Tutorial_City_Ending(){
        HideArrow(battleArrow);
        gameMessagebox.createDialogBox("Get ready","Hire general from Tawer, recruit units from Barracks and go take some fight");
        PlayerPrefs.SetInt("isTutorialComplete",1);
        Debug.Log($"Status tutorial: {PlayerPrefs.GetInt("isTutorialComplete")}");
    }

    private void ShowArrow(GameObject arrow){
        arrow.SetActive(true);
    }

    private void HideArrow(GameObject arrow){
        arrow.SetActive(false);
    }
}
