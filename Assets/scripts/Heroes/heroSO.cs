using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="new Hero", menuName ="Heroes/new Hero")]
public class heroSO : ScriptableObject
{
    public int heroID;
    public string heroName;
    public Sprite heroSprite;
    public Sprite spellIcon;
    public string entryDialog;
    public string defeatDialog;
}
