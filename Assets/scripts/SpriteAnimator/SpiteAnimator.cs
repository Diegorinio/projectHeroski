using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SpiteAnimator : MonoBehaviour
{
    public Image attackerImageSpace;
    public Image victimImageSpace;
    [SerializeField]
    private Image victimSourceImage;

    public Sprite[] m_SpriteArray;
    public TextMeshProUGUI damageTextUI;
    [SerializeField]
    private int damageText;
    public float m_Speed = .02f;
    [SerializeField]
    private int m_IndexSprite;
    [SerializeField]
    private bool isDone;
    public void playAnim()
    {
        Debug.Log("animation play");
        isDone=false;
        // damageText = 0;
        damageTextUI.text = "";
        m_IndexSprite=0;
        StartCoroutine(_playAnim());
    }
    IEnumerator _playAnim()
    {
        yield return new WaitForSeconds(m_Speed);
        if(m_IndexSprite>=m_SpriteArray.Length){
            m_IndexSprite=m_SpriteArray.Length-1;
            isDone=true;
        }
        if(m_IndexSprite==m_SpriteArray.Length-1){
            damageTextUI.text = damageText.ToString();
        }
        if(!isDone){
        attackerImageSpace.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite++;
        StartCoroutine(_playAnim());
        }
        else{
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
            StopCoroutine(_playAnim());
        }
    }
    public bool isAnimDone(){
        return isDone;
    }

    private void setVictimImage(Image img){
        victimSourceImage = img;
        victimImageSpace.sprite=img.sprite;
    }
    private void setAttackerImageSequence(Sprite[] img){
        m_SpriteArray = new Sprite[img.Length];
        for(int x=0;x<img.Length;x++){
            m_SpriteArray[x]=img[x];
        }
    }

    public void setDamageText(int dmg){
        damageText = dmg;
    }

    public void setAnimator(Sprite[] attackerImage,Image victimImage,int dmg){
        setAttackerImageSequence(attackerImage);
        setVictimImage(victimImage);
        setDamageText(dmg);
    }
}
