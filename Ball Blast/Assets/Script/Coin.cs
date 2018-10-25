using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    public Color a;
    public Color b;
   private  Image gold;

    private void Start()
    {
        gold = this.transform.GetComponent<Image>();

        StartCoroutine(waittime());

        Destroy(this.transform.gameObject,7f);
    }

    IEnumerator waittime() {
        yield return new WaitForSeconds(4f);


        StartCoroutine(AnimationEffect());

    }

    private IEnumerator AnimationEffect()//金币颜色闪烁
    {
        #region //闪烁
        //yield return new WaitForSeconds(0.2f);
        //this.a.a = 1f;
        //this.gold.color = this.a;
        //yield return new WaitForSeconds(0.2f);
        //this.b.a = 0f;
        //this.gold.color = this.b;
        //base.StartCoroutine(this.AnimationEffect());
        #endregion  
        this.transform.DOScale(0, 0.5f);
     


        yield break;
    }




}
