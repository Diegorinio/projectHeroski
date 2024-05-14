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
    [NonSerialized] public int wood;
    [NonSerialized] public int steel;
    [NonSerialized] public int X;
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
         //Ustaw gold na w duzo // do testow
        PlayerPrefs.SetInt("GoldInMenu",9999);
        //wczytanie save
        gold=PlayerPrefs.GetInt("GoldInMenu");
        //iron=PlayerPrefs.GetInt("IronInMenu");
        //wood+=PlayerPrefs.GetInt("WoodInMenu", wood);
        //steel+=PlayerPrefs.GetInt("SteelInMenu", steel);
        //X+=PlayerPrefs.GetInt("XInMenu", X);
        GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(gold.ToString());
        //GameObject.Find("Iron_Counter").GetComponent<TextMeshPro>().SetText(iron.ToString());
        //GameObject.Find("WoodLabel").GetComponent<TextMeshPro>().SetText("Wood: " + wood.ToString());
        //GameObject.Find("SteelLabel").GetComponent<TextMeshPro>().SetText("Steel: " + steel.ToString());
        //GameObject.Find("XlLabel").GetComponent<TextMeshPro>().SetText("X: " + X.ToString());
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
    //public void CheatGold() 
    //{
    //    gold +=1111111;
    //    GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(gold.ToString());
    //}
    //public void CheatIron()
    //{
    //    iron += 1111111;
    //    GameObject.Find("Iron_Counter").GetComponent<TextMeshPro>().SetText(iron.ToString());
    //}

    //public void ActivateCheats()
    //{
    //    if(GameObject.Find("cheatButton0")==null){
    //        return;
    //    }
    //    print("sasdasd");
    //    activateCheat += 1;
    //    if (activateCheat > 10)
    //    {
    //        for (int i = 0; i < cheatbuttons.Length; i++)
    //        {
    //            cheatbuttons[i].SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
    public void CheckifChange()
    {
        GameObject.Find("Gold_counter").GetComponent<TextMeshPro>().SetText(gold.ToString());
        //GameObject.Find("Iron_Counter").GetComponent<TextMeshPro>().SetText(iron.ToString());
        //GameObject.Find("WoodLabel").GetComponent<TextMeshPro>().SetText("Wood: "+wood.ToString());
        //GameObject.Find("SteelLabel").GetComponent<TextMeshPro>().SetText("Steel: "+steel.ToString());
        //GameObject.Find("XlLabel").GetComponent<TextMeshPro>().SetText("X: "+X.ToString());
        PlayerPrefs.SetInt("GoldInMenu", gold);
        //PlayerPrefs.SetInt("IronInMenu", iron);
        //PlayerPrefs.SetInt("WoodInMenu", wood);
        //PlayerPrefs.SetInt("SteelInMenu", steel);
        //PlayerPrefs.SetInt("XInMenu", X);



    }
}
