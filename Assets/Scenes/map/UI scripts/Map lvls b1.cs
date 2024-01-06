using System.Collections.Generic;
using System;
using UnityEditor.PackageManager.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.ComponentModel;
// using Palmmedia.ReportGenerator.Core.Common;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class Maplvls : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        private VisualElement root;

        private Button backToBigMap;

        //big map buttons
        public Button lvlBM1;
        public Button lvlBM2;
        public Button lvlBM3;
        public Button lvlBM4;

        //fight lvl buttons
        public Button lvl1;
        public Button lvl2;
        public Button lvl3;
        public Button lvl4;
        public Button lvl5;
        public Button lvl6;
        public Button lvl7;   
        public Button lvl8;  
        public Button lvl9;
        public Button lvl10;

        public Button nextBiom;

        //biomk�w mapa
        public GameObject bigMap;
        public GameObject BIOM1;
        public GameObject BIOM2;
        public GameObject BIOM3;
        public GameObject BIOM4;

        private void Awake()
        {
            uiDocumentCmp = gameObject.GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;

            nextBiom=root.Q<Button>("TONEXTB");
            backToBigMap=root.Q<Button>("BACKTOBIGMAP");

            //szybciej sprawdzi jak podziele :>
            if (this.gameObject.name == "mainMap")
            {
                lvlBM1 = root.Q<Button>("lvl-1");
                lvlBM2 = root.Q<Button>("lvl-2");
                lvlBM3 = root.Q<Button>("lvl-3");
                lvlBM4 = root.Q<Button>("lvl-4");

            }
            else
            {
                lvl1 = root.Q<Button>("lvl-1");
                lvl2 = root.Q<Button>("lvl-2");
                lvl3 = root.Q<Button>("lvl-3");
                lvl4 = root.Q<Button>("lvl-4");
                lvl5 = root.Q<Button>("lvl-5");
                lvl6 = root.Q<Button>("lvl-6");
                lvl7 = root.Q<Button>("lvl-7");
                lvl8 = root.Q<Button>("lvl-8");
                lvl9 = root.Q<Button>("lvl-9");
                lvl10 = root.Q<Button>("lvl-10");
                nextBiom=root.Q<Button>("TONEXTB");
            }
            //mapy
            bigMap = GameObject.Find("mainMap");
            BIOM1 = GameObject.Find("Biom1");
            BIOM2 = GameObject.Find("Biom2");
            BIOM3 = GameObject.Find("Biom3");
            BIOM4 = GameObject.Find("Biom4");


        }
        private void OnEnable()
        {
            //podobnie jak z przyciskami kompilator lubi p�aka� 
            if (this.gameObject.name == "mainMap")
            {
                lvlBM1.clicked += () => ChangeBigMapToBiom("B1");
                lvlBM2.clicked += () => ChangeBigMapToBiom("B2");
                lvlBM3.clicked += () => ChangeBigMapToBiom("B3");
                lvlBM4.clicked += () => ChangeBigMapToBiom("B4");

            }
            else
            {
                randomMapEventGenerator rndEvent= gameObject.AddComponent<randomMapEventGenerator>();
                backToBigMap.clicked += () => ChangeSceneToBigMap();
                // lvl1.clicked += () => FromBiomtoFight(lvl1.viewDataKey,"1");//strings
                lvl1.clicked += ()=>FromBiomtoFight(rndEvent);
                if (this.gameObject.tag == "MAP4") return;
                nextBiom.clicked += () => BiomtoNextBiom(this.gameObject.tag);
                // nextBiom.clicked+=()=>Debug.Log("Click");
            }
        }


        private void ChangeBigMapToBiom(string x)
        {
            bigMap.GetComponent<UIDocument>().sortingOrder = 10;
            switch (x)
            {
                case "B1":
                    BIOM1.GetComponent<UIDocument>().sortingOrder = 11;
                    break;
                case "B2":
                    BIOM2.GetComponent<UIDocument>().sortingOrder = 11;
                    break;
                case "B3":
                    BIOM3.GetComponent<UIDocument>().sortingOrder = 11;
                    break;
                default:
                    BIOM4.GetComponent<UIDocument>().sortingOrder = 11;
                    break;
            }
        }
        private void ChangeSceneToBigMap()
        {
            this.GetComponent<UIDocument>().sortingOrder = 10;
            bigMap.GetComponent<UIDocument>().sortingOrder = 11;
        }

        private void BiomtoNextBiom(string x)
        { 
            int i;
            Debug.Log($"Czym to {x}");
            string output = x.Substring(3); 
            this.GetComponent<UIDocument>().sortingOrder = 10;
            // GameObject.FindGameObjectWithTag("MAP" + i.ToString()).GetComponent<UIDocument>().sortingOrder = 11;
        }
        private void FromBiomtoFight(randomMapEventGenerator rndEvent)
        {
            Debug.Log(rndEvent.getRandomName());
            rndEvent.goToFight("battleScene");
            // string biom = x;
            // string lvl=y;
            //zapisywanie scen walki fight;B<numerbiomu>-<numerlvl>
            // SceneManager.LoadScene(sceneName: "fight;"+x+"-"+y);
        }

    }
}
