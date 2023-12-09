using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string bossName;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int dist;
    [SerializeField]
    private string[] bossAttacks = new string[2];
    [SerializeField]
    private Slider hpSlider;
    public void Awake()
    {
        Camera.main.GetComponent<guiScript>().bossName.text = bossName;
    }
    public void Start()
    {

    }
    public void Update()
    {
        hpSlider.value = hp;
    }
    public int Attack1()
    {
        throw new System.NotImplementedException();
    }

    public int Attack2()
    {
        throw new System.NotImplementedException();
    }

    public void dealDamageTo(Role enemy)
    {
        throw new System.NotImplementedException();
    }
    public void getHit(int dmg)
    {
        hp= hp-dmg;
        Debug.Log($"{this.name} otrzymal {dmg}");
    }

    public string getClassType(){
        return this.GetType().ToString();
    }
}
