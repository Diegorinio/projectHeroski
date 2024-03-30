using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    private void OnDisable()
    {
        float positionOfMap = this.GetComponent<RectTransform>().anchoredPosition.y;
        PlayerPrefs.SetFloat("PositionOfMap", positionOfMap);
        print(PlayerPrefs.GetFloat("PositionOfMap"));
    }
}
