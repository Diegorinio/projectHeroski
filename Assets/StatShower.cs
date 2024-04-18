using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatShower : MonoBehaviour
{
    public Unit assignedUnit;
    public GameObject cringe;
    private TextMeshProUGUI unitName;
    private TextMeshProUGUI unitBasehp;
    private TextMeshProUGUI unitBaseDmg;
    private TextMeshProUGUI unitTier;
    private TextMeshProUGUI unitMoveDistance;

    private void Awake()
    {
        cringe = GameObject.Find("StatShow");
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
        setUpComponentsValues(assignedUnit);
    }

    private void setUpComponentsValues(Unit _unit){
        unitName.text = _unit.unitName;
        unitBaseDmg.text = _unit.unitBaseDamage.ToString();
        unitBasehp.text = _unit.unitBaseHealth.ToString();
        unitTier.text = _unit.getUnitTier().ToString();
        Vector2 moveDistance = _unit.getUnitMoveDistance();
        unitMoveDistance.text = $"{moveDistance.x},{moveDistance.y}";
    }
    private TextMeshProUGUI findComponentFromParent(string cmp_name){
        TextMeshProUGUI found = transform.parent.Find(cmp_name).gameObject.GetComponent<TextMeshProUGUI>();
        if(found ==null){
            return null;
        }
        return found;
    }
}
