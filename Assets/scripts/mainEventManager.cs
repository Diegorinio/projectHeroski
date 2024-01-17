using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainEventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
}
