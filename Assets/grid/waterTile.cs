using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTile:Tile
{
    [SerializeField]
    private string tileEventName;
    [SerializeField]
    private Sprite eventTileSprite;

    protected override void Start()
    {
        base.Start();
        render.sprite = eventTileSprite;
    }
    public override void OnMouseDown()
    {
        Debug.Log($"Pressed {name}");
        if (turnbaseScript.isSelected && isActive && !isTaken)
        {
            //Wez daną jednostę z tury i przypisz go do Tile
            GameObject player = turnbaseScript.selectedGameObject;
            unitController controller = player.GetComponent<unitController>();
            Vector2Int dist = controller.getUnitDistance();
            controller.setUnitDistance(dist.x/2,dist.y/2);
            controller.characterMove(gameObject);
            isActive = false;
        }
        else
        {
            Debug.Log("NIE");
        }
    }
}
