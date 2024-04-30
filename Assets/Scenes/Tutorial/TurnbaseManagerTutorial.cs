using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnbaseManagerTutorial : MonoBehaviour
{
    public GameObject showPlayerGeneralArrow;
    public GameObject showEnemyGeneralInfo;
    public GameObject showQueueArrow;
    public GameObject showSpellsArrow;
    public bool isShowing = false;
    public bool firstStep = false;
    public bool secondStep = false;
    public turnbaseScript mainTurnScript;
    void Update(){
        if(!isShowing){
            switch(mainTurnScript.turn){
                case 0:
                if(!firstStep)
                ShowStartInfo();
                // isShowing=true;
                break;
            }
            if(turnbaseScript.IsHeroTurn()){
                if(!firstStep){
                    ShowStartInfo();
                }
                else if(firstStep && !secondStep && mainTurnScript.turn >2&&!isShowing){
                    ShowAboutBattle_Part2();
                    isShowing=true;
                }
            }
            Debug.Log($"TEstowa: {mainTurnScript.turn}");
        }
    }

    void ShowStartInfo(){
        isShowing=true;
        gameMessagebox.createDialogBox("Battle","This is battle screen, let me show you some  things", ()=>{
            ShowArrow(showPlayerGeneralArrow);
            gameMessagebox.createDialogBox("Your General", "This is your general",()=>{
                HideArrow(showPlayerGeneralArrow);
                ShowArrow(showEnemyGeneralInfo);
                gameMessagebox.createDialogBox("Enemy General", "This is enemy general which control enemy units, sometimes with some weird magic he can morph to your general", ShowSpellsInfo);
            });
        });
    }
    void ShowSpellsInfo(){
        HideArrow(showEnemyGeneralInfo);
        ShowArrow(showSpellsArrow);
        gameMessagebox.createDialogBox("Spells", "Every general have 2 skills, you can check them in Tawern",ShowAboutSpells);
    }
    void ShowAboutSpells(){
        gameMessagebox.createDialogBox("Spells", "There are 2 types of spells, Destructable which cause damage and Blessings which cause boost for units controlled by general", ShowAboutBattle_Part1);
    }

    void ShowAboutBattle_Part1(){
        gameMessagebox.createDialogBox("Battle","Your units have certain distance of movement and attack, let's start with moving your unit. Double click on any of green highlighted tile", ()=>{
            firstStep=true;
            isShowing=false;
        });
    }

    void ShowAboutBattle_Part2(){
        isShowing=true;
        gameMessagebox.createDialogBox("Units combat", "You attack with units by double clicking on enemy unit if there any in unit attack range",ShowAboutBattle_Units);
    }

    void ShowAboutBattle_Units(){
        gameMessagebox.createDialogBox("Unit combat pt2", "Units deal damage based on their amount, try to get closed to enemy and attack",ShowAboutBattle_SpellsCombat);
    }

    void ShowAboutBattle_SpellsCombat(){
        gameMessagebox.createDialogBox("Spells", "Each turn you can use one of your spell",ShowAboutBattle_Final);
    }

    void ShowAboutBattle_Final(){
        gameMessagebox.createDialogBox("Just fight", "I think that's all, now go and fight this battle!");
    }

    private void ShowArrow(GameObject arrow){
        arrow.SetActive(true);
    }
    private void HideArrow(GameObject arrow){
        arrow.SetActive(false);
    }
}
