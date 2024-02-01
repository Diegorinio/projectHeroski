using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tile : MonoBehaviour
{

    //TODO:
    //Zmiana nazw i lepiej nazwanych wlasciwosci
    //pozycja x,y na mapie glownego gridu
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
    protected TileSO tilePreset;

    //Przypisany obiekt do danego Tile, np: gracz,przeszkoda,
    protected GameObject gameObjectOnTile;
    protected virtual void setPreset(){
        setTilePreset("grassTile");
    }
    private void Start()
    {
        render=gameObject.GetComponent<SpriteRenderer>();
        setPreset();
        render.sprite = tilePreset.tileSprite;
    }

    protected void setTilePreset(string presetName){
        tilePreset = Resources.Load<TileSO>($"Tiles/{presetName}");
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
    protected abstract void TileBehaviour();
    // protected abstract void TileBehaviour(GameObject obj);

    //Przypisz GameObject do danego Tile
    //Wywolaj event dzialania tile w momencie przypisania Gameobjectu do Tile
    public virtual void SetGameObjectOnTile(GameObject obj){
        gameObjectOnTile=obj;
        TileBehaviour();
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
    public virtual void OnMouseDown()
    {
        Debug.Log($"Pressed {name}");
        if (turnbaseScript.isSelected && isActive && !isTaken)
        {
            //Wez daną jednostę z tury i przypisz go do Tile
            GameObject player = turnbaseScript.selectedGameObject;
            player.GetComponent<unitController>().characterMove(gameObject);
            isActive = false;
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
