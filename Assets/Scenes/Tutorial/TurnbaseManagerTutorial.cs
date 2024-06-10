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
        }
    }

   void ShowStartInfo(){
    isShowing = true;
    gameMessagebox.createDialogBox("Battle", "This is the battle screen. Let me show you some things.", () => {
        ShowArrow(showPlayerGeneralArrow);
        gameMessagebox.createDialogBox("Your General", "This is your general.", () => {
            HideArrow(showPlayerGeneralArrow);
            ShowArrow(showEnemyGeneralInfo);
            gameMessagebox.createDialogBox("Enemy General", "This is the enemy general who controls enemy units. Sometimes, with some weird magic, he can morph into your general.", ShowSpellsInfo);
        });
    });
}

void ShowSpellsInfo(){
    HideArrow(showEnemyGeneralInfo);
    ShowArrow(showSpellsArrow);
    gameMessagebox.createDialogBox("Spells", "Every general has 2 skills. You can check them in the Tavern.", ShowAboutSpells);
}

void ShowAboutSpells(){
    HideArrow(showSpellsArrow);
    gameMessagebox.createDialogBox("Spells", "There are 2 types of spells: Destructive, which cause damage, and Blessings, which boost units controlled by the general.", ShowAboutBattle_Part1);
}

void ShowAboutBattle_Part1(){
    gameMessagebox.createDialogBox("Battle", "Your units have a certain distance for movement and attack. Let's start with moving your unit. Double click on any of the green highlighted tiles.", () => {
        firstStep = true;
        isShowing = false;
    });
}

void ShowAboutBattle_Part2(){
    isShowing = true;
    gameMessagebox.createDialogBox("Units Combat", "You attack with units by double clicking on an enemy unit if there is any in your unit's attack range.", ShowAboutBattle_Units);
}

void ShowAboutBattle_Units(){
    gameMessagebox.createDialogBox("Unit Combat pt2", "Units deal damage based on their amount. Try to get close to the enemy and attack.", ShowAboutBattle_SpellsCombat);
}

void ShowAboutBattle_SpellsCombat(){
    gameMessagebox.createDialogBox("Spells", "Each turn, you can use one of your spells.", ShowAboutBattle_Final);
}

void ShowAboutBattle_Final(){
    gameMessagebox.createDialogBox("Just Fight", "I think that's all. Now go and fight this battle!");
}
    private void ShowArrow(GameObject arrow){
        arrow.SetActive(true);
    }
    private void HideArrow(GameObject arrow){
        arrow.SetActive(false);
    }
}
