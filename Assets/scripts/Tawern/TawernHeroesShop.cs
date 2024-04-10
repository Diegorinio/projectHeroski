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
    // Start is called before the first frame update
    void Awake(){
        heoresSOLists = heroSpawner.getHeroesSOList();
    }
    void OnEnable()
    {
        foreach(var hero in heoresSOLists){
            GameObject newImage = Instantiate(heroImagePrefab,transform.position,Quaternion.identity,gameObject.transform);
            _listOfImages.Add(newImage);
            newImage.GetComponent<Image>().sprite = hero.heroSprite;
            newImage.GetComponent<Button>().onClick.AddListener(()=>{ShowInTawernInfoPanel(hero);});
        }
    }

    void OnDisable(){
        clearImagesList();
    }
    private void clearImagesList(){
        for(int i=0;i<_listOfImages.Count;i++){
            Destroy(_listOfImages[i]);
        }
    }

    private void ShowInTawernInfoPanel(heroSO hero){
        tawernShop.SetUpTawerShopHero(hero);
    }
}
