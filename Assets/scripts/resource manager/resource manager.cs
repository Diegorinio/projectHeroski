using System;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class resourcemanager : MonoBehaviour
{
    protected Animator animatorCmp;
    protected Button buttonCmp;
    //zmienic liczbe zaleznie od cheatow
    public GameObject[] cheatbuttons;
    //
    [NonSerialized] public int gold;
    [NonSerialized] public int iron;
    protected bool menuIsOn=true;
    protected byte activateCheat=0;
    private void Start()
    {
        cheatbuttons = new GameObject[2];
        animatorCmp = GetComponent<Animator>();
        buttonCmp = GetComponent<Button>();
        if (GameObject.Find("cheatButton0") !=null)
        {
            for (int i = 0; i <cheatbuttons.Length; i++) {
                cheatbuttons[i]=GameObject.Find("cheatButton"+(i+1).ToString());
                Debug.Log($"chuuuuj {i}");
                cheatbuttons[i].SetActive(false);
            }
        }
    }
    public void ManagerOnClick()
    {
        if (!menuIsOn)
        {
            animatorCmp.ResetTrigger("closeR");
            animatorCmp.SetTrigger("openR");
            menuIsOn = true;
        }
        else
        {
            animatorCmp.ResetTrigger("openR");
            animatorCmp.SetTrigger("closeR");
            menuIsOn = false;
        }
    }
    public void CheatGold() 
    {
        gold +=1111111;
        GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(gold.ToString());
    }
    public void CheatIron()
    {
        iron += 1111111;
        GameObject.Find("Iron_Counter").GetComponent<TextMeshPro>().SetText(iron.ToString());
    }

    public void ActivateCheats()
    {
        if(GameObject.Find("cheatButton0")==null){
            return;
        }
        activateCheat += 1;
        if (activateCheat > 10)
        {
            for (int i = 0; i < cheatbuttons.Length; i++)
            {
                cheatbuttons[i].SetActive(true);
            }
        }
        else
        {
            return;
        }
    }
}
