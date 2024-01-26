using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasScalerToScreen : MonoBehaviour
{
    void Awake(){
        CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();
        scaler.referenceResolution = new Vector2(Screen.height,Screen.width);
    }
}
