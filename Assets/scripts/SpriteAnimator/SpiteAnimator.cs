using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpiteAnimator : MonoBehaviour
{
public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    public void Func_PlayUIAnim()
    {
        // IsDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if(m_IndexSprite>=m_SpriteArray.Length){
            m_IndexSprite=m_SpriteArray.Length-1;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite++;
        StartCoroutine(Func_PlayAnimUI());
    }

    void Start(){
        Debug.Log("animation play");
        Func_PlayUIAnim();
    }
}
