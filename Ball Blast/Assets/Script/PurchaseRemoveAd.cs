//购买成功后执行的
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseRemoveAd : MonoBehaviour {
    public Image tween;
    private void Start()
    {
        tween = this.transform.GetChild(6).GetChild(0).GetComponent<Image>();
        tween.transform.DOLocalMoveX(233.6f, 3f).SetLoops(-1,LoopType.Yoyo);
    }



    public void Closemenu()  //关闭按钮
    {
        if (this.transform.localScale==Vector3.one)
        {
            this.transform.DOScale(0, 0.2f);
        }
        else
        {
            this.transform.DOScale(1, 0.2f);
        }
    
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound); //点击音效
    }






   


}
