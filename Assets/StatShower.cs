using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatShower : MonoBehaviour
{
    public GameObject cringe;
    private void Awake()
    {
        GameObject.Find("StatShow").SetActive(false);
    }
    public void showStatMenu()
    {
        cringe.SetActive(true);
    }
}
