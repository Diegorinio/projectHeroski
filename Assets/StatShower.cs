using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatShower : MonoBehaviour
{
    public UnitSO assignedUnit;
    public GameObject cringe;
    private Image unitImage;
    private TextMeshProUGUI unitName;
    private TextMeshProUGUI unitBasehp;
    private TextMeshProUGUI unitBaseDmg;
    private TextMeshProUGUI unitTier;
    private TextMeshProUGUI unitMoveDistance;

    private void Awake()
    {
        // cringe = GameObject.Find("StatShow");
        findComponents();
        cringe.SetActive(false);
    }
    public void showStatMenu()
    {
        cringe.SetActive(true);
        findComponents();

    }
    private void findComponents(){
        unitName = findComponentFromParent("unit_name");
        unitBasehp= findComponentFromParent("unit_base_hp");
        unitBaseDmg = findComponentFromParent("unit_base_dmg");
        unitTier = findComponentFromParent("unit_tier");
        unitMoveDistance = findComponentFromParent("unit_movement_distance_x_y");
        unitImage = cringe.transform.Find("unit_image").GetComponent<Image>();
        setUpComponentsValues(assignedUnit);
    }

    private void setUpComponentsValues(UnitSO _unit){
        unitName.text = _unit.unitName;
        unitBaseDmg.text = _unit.unitBaseDamage.ToString();
        unitBasehp.text = _unit.unitBaseHealth.ToString();
        unitTier.text = _unit.tier.ToString();
        Vector2 moveDistance = new Vector2(_unit.gridDistanceX,_unit.gridDistanceY);
        unitMoveDistance.text = $"X:{moveDistance.x},Y:{moveDistance.y}";
        unitImage.sprite = _unit.unitSprite;
        
    }
    private TextMeshProUGUI findComponentFromParent(string cmp_name){
        TextMeshProUGUI found = cringe.transform.Find(cmp_name).gameObject.GetComponent<TextMeshProUGUI>();
        if(found ==null){
            return null;
        }
        return found;
    }
}
