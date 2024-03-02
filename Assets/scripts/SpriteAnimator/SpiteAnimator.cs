using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SpiteAnimator : MonoBehaviour
{
    public Image attackerImageSpace;
    public Image victimImageSpace;
    private Image attackerSourceImage;
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
    public void Func_PlayUIAnim()
    {
        Debug.Log("animation play");
        isDone=false;
        // damageText = 0;
        damageTextUI.text = "";
        m_IndexSprite=0;
        StartCoroutine(Func_PlayAnimUI());
        Debug.Log("$CHUJ AIMACJA KSONCZONE");
    }
    IEnumerator Func_PlayAnimUI()
    {
        Debug.Log("KURWa animacja dziala");
        yield return new WaitForSeconds(m_Speed);
        if(m_IndexSprite>=m_SpriteArray.Length){
            m_IndexSprite=m_SpriteArray.Length-1;
            damageTextUI.text = damageText.ToString();
            isDone=true;
        }
        if(!isDone){
        attackerImageSpace.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite++;
        StartCoroutine(Func_PlayAnimUI());
        }
        else{
            gameObject.SetActive(false);
            StopCoroutine(Func_PlayAnimUI());
        }
    }

    private void setVictimImage(Image img){
        victimSourceImage = img;
    }
    private void setAttackerImage(Image img){
        attackerSourceImage = img;
    }
    void Start(){
        // Debug.Log("animation play");
        // Func_PlayUIAnim();
    }

    public void setDamageText(int dmg){
        damageText = dmg;
    }

    public void setAnimator(Image attackerImage,Image victimImage,int dmg){
        setAttackerImage(attackerImage);
        setVictimImage(victimImage);
        setDamageText(dmg);
    }
}
