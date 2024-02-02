using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tile : MonoBehaviour
{

    //TODO:
    //Zmiana nazw i lepiej nazwanych wlasciwosci
    [SerializeField]
    protected int posX,posY;

    //ustawienie czy Tile jest aktywny isActive? podwietl:brak podswietlenia
    public bool isActive = false;
    [SerializeField]
    //ustawienie czy Tile jest zajety przez inny obiekt
    //TODO: Wyrzucic i przepisac 
    protected bool isTaken = false;
    //Sprite danego Tile
    protected SpriteRenderer render;

    //Preset asset dla Tile zawierajacy nazwe i sprite 
    protected TileSO tilePreset;

    //Przypisany obiekt do danego Tile, np: gracz,przeszkoda,
    protected GameObject gameObjectOnTile;

    //Zaladuj preset
    protected virtual void setPreset(){
        setTilePreset("normalTile");
    }
    //Zaladuj preset na Awake
    private void Awake(){
        setPreset();
    }

    //Znajdz spriteRendere i zmien sprite na ten z assetu
    private void Start()
    {
        render=gameObject.GetComponent<SpriteRenderer>();
        render.sprite = tilePreset.tileSprite;
    }
    //Zaladuj preset
    protected void setTilePreset(string presetName){
        tilePreset = Resources.Load<TileSO>($"Tiles/{presetName}");
    }

    //Zaladuj preset ze zmiana sprajta
    protected void setTilePreset(string presetName, bool reload){
        if(reload){
        tilePreset = Resources.Load<TileSO>($"Tiles/{presetName}");
        render=gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(render.sprite.name+" : "+tilePreset.tileSprite.name);
        render.sprite=tilePreset.tileSprite;
        }
    }
    // Update ( trzeba to przepisac na zwykla funkcje aktywyjaca)
    // Jak narazie brak wplywu na wydajnosc ale to moze sie zmienic z czasem
    void Update()
    {
        if (isActive)
        {
            render.color = Color.green;
        }
        else if(!isActive||isTaken)
        {
            render.color = Color.grey;
        }
    }

    //Metoda odpowiedzialna za efekt jaki daje gdy gracz/przeciwnik wejdzie na dany Tile
    protected abstract void TileBehaviour();

    //Przypisz GameObject do danego Tile
    public virtual void SetGameObjectOnTile(GameObject obj){
        gameObjectOnTile=obj;
    }

    //Zwroc przypisany GameObject do danego Tile
    public GameObject GetGameObjectOnTile(){
        return gameObjectOnTile;
    }


    // Zwroc pozycje danego Tile x,y w Vector2Int w ogolnej mapie grida
    public Vector2Int getPosition(){
        return new Vector2Int(posX,posY);
    }

    // Ustaw pozycje danego Tile na x,y w ogolnej mapie grida
    public void setPosition(int x,int y){
        posX=x;
        posY=y;
    }

    // DLA GRACZA: Jezeli jest aktywny to moze sie poruszyc do danego Tile
    //gdy mozna sie poruszyc to uruchom zachowanie Tile
    public virtual void OnMouseDown()
    {
        Debug.Log($"Pressed {name}");
        if (turnbaseScript.isSelected && isActive && !isTaken)
        {
            //Wez daną jednostę z tury i przypisz go do Tile
            GameObject player = turnbaseScript.selectedGameObject;
            player.GetComponent<unitController>().characterMove(gameObject);
            isActive = false;
            TileBehaviour();
        }
        else
        {
            Debug.Log("NIE");
        }
    }
    public bool isBusy()
    {
        return isTaken;
    }
    public void makeBusy()
    {
        isTaken=true;
    }
    public void unMakeBusy(){
        isTaken=false;
    }
}