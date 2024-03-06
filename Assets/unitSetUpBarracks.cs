using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitSetUpBarracks : MonoBehaviour
{
    public unitSpawner.tier unitTier;
    public unitSpawner.unitType unitType;
    public unitSpawner.tier getUnitTier(){
        return unitTier;
    }
    public unitSpawner.unitType getUnitType(){
        return unitType;
    }
}
