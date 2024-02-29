using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierChangerHandler : MonoBehaviour
{
    public Button T1B;
    public Button T2B;
    public Button T3B;
    public Button T4B;

    public GameObject citymanager;

    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    protected GameObject lastOpenTier;
    bool animation;
    bool firsttime;
    double oldposition;
    private void OnEnable()
    {
        T2.SetActive(false);
        T3.SetActive(false);
        T4 .SetActive(false);
        lastOpenTier = T1;
        firsttime = true;
        animation = false;
        if (citymanager.GetComponent<CityManager>().lvlKoszar != 0)
        {
            if ((int)citymanager.GetComponent<CityManager>().lvlKoszar < 4)
            {
                T4B.interactable = false; print("1");
                if ((int)citymanager.GetComponent<CityManager>().lvlKoszar < 3)
                {
                    T3B.interactable = false; print("2");
                    if ((int)citymanager.GetComponent<CityManager>().lvlKoszar < 2) { T2B.interactable = false; print("3"); }
                }
            }
        }

    }
    public void TierChanger(string Tier)
    {
        T1B.interactable = false;
        T2B.interactable = false;
        T3B.interactable = false;
        T4B.interactable = false;

        switch (Tier)
        {
            case "T1":
                if(T1==lastOpenTier)T1B.interactable=true;
                StartCoroutine(littleBitOfWaitingLastTier(lastOpenTier, T1));
                break;
            case "T2":
                if (T2 == lastOpenTier) T2B.interactable = true;
                StartCoroutine(littleBitOfWaitingLastTier(lastOpenTier, T2));
                break;
            case "T3":
                if (T3 == lastOpenTier) T3B.interactable = true;
                StartCoroutine(littleBitOfWaitingLastTier(lastOpenTier,T3));
                break;
            case "T4":
                if (T4 == lastOpenTier) T4B.interactable = true;
                StartCoroutine(littleBitOfWaitingLastTier(lastOpenTier, T4));
                break;

        }
    }

    IEnumerator littleBitOfWaitingLastTier(GameObject LateTierUsed, GameObject GoodTier)
    {
        if(firsttime){ oldposition = LateTierUsed.transform.position.x;firsttime = false;}

                for (double i = LateTierUsed.transform.position.x; i <= oldposition+5.5; i += 0.40)
                {
                    LateTierUsed.transform.position = new Vector3((float)i, LateTierUsed.transform.position.y, LateTierUsed.transform.position.z);
                    // yield return new WaitForSecondsRealtime(0.005f);
                }
                yield return StartCoroutine(littleBitOfWaitingNewTier(GoodTier));

                if(!lastOpenTier== LateTierUsed) LateTierUsed.SetActive(false);

    }
    IEnumerator littleBitOfWaitingNewTier(GameObject NewTier)
    {
            NewTier.SetActive(true);
            NewTier.transform.position = new Vector3(-5, NewTier.transform.position.y, NewTier.transform.position.z);
            for (double i = NewTier.transform.position.x; i <= oldposition; i += 0.40)
            {
                NewTier.transform.position = new Vector3((float)i, NewTier.transform.position.y, NewTier.transform.position.z);
                yield return new WaitForSecondsRealtime(0.005f);
            }
        lastOpenTier = NewTier;
        T1B.interactable = true;
        if ((int)citymanager.GetComponent<CityManager>().lvlKoszar >= 2)
        {
            T2B.interactable = true;
            if ((int)citymanager.GetComponent<CityManager>().lvlKoszar >= 3)
            {
                T3B.interactable = true;
                if ((int)citymanager.GetComponent<CityManager>().lvlKoszar >= 4)T4B.interactable = true; 
            }
        }
        
    }
    private void OnDisable()
    {
        if (firsttime) return;
        T1.transform.position = new Vector3((float)oldposition, T1.transform.position.y, T1.transform.position.z);
        T2.transform.position = new Vector3((float)oldposition, T2.transform.position.y, T2.transform.position.z);
        T3.transform.position = new Vector3((float)oldposition, T3.transform.position.y, T3.transform.position.z);
        T4.transform.position = new Vector3((float)oldposition, T4.transform.position.y, T4.transform.position.z);
    }
}
