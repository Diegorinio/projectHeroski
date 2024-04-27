using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TawernHeroesShop : MonoBehaviour
{
    public TawernShopHeroInfoPanel tawernShop;
    public GameObject heroImagePrefab;
    private List<heroSO> heoresSOLists;
    private List<GameObject> _listOfImages= new List<GameObject>();
    void Awake(){
        heoresSOLists = heroSpawner.getHeroesSOList();
    }
    void OnEnable()
    {
        SetUpHeroesList();
    }

    void OnDisable(){
        clearImagesList();
    }
    private void clearImagesList(){
        for(int i=0;i<_listOfImages.Count;i++){
            Destroy(_listOfImages[i]);
        }
    }

    public void LoadAgain(){
        clearImagesList();
        SetUpHeroesList();
    }

    private void SetUpHeroesList(){
        int hiredHeroID = -1;
        if(mainPlayerUnit.Instance.getSelectedHero()!=null){
            hiredHeroID = mainPlayerUnit.Instance.getSelectedHero().getHeroSO().heroID;
        }

        foreach(var hero in heoresSOLists){
            GameObject newImage = Instantiate(heroImagePrefab,transform.position,Quaternion.identity,gameObject.transform);
            _listOfImages.Add(newImage);
            newImage.SetActive(true);
            newImage.GetComponent<Image>().sprite = hero.heroSprite;
            newImage.GetComponent<Button>().onClick.AddListener(()=>{ShowInTawernInfoPanel(hero);});
            if(hero.heroID!=hiredHeroID){
                newImage.transform.Find("hired_mark").gameObject.SetActive(false);
            }
        }
    }

    private void ShowInTawernInfoPanel(heroSO hero){
        tawernShop.SetUpTawerShopHero(hero);
    }
}
