using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinNumber : MonoBehaviour
{
    private Text tm;//钱的组件

    public Color textColor1;//文本颜色

    public Color textColor2; //文本颜色

    private float maxX;

    public float tweentime;
    private float minX; //最小左边
    public bool candestory=false;

    private float direction = 1f;

    //public void OnFadeComplete(string name) {

    //    if (name=="Fade")
    //    {
    //        GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
    //        StopCoroutine("WaitFadeComplete");
    //    }

    //}


    //IEnumerator WaitFadeComplete(string name ,float waittime)
    //{
    //    yield return new WaitForSeconds(waittime);
    //    OnFadeComplete(name);

    //}





    private void Awake()
    {
        this.tm = this.transform.GetComponent<Text>();
      
    }


    private void Start()
    {
        //StartCoroutine(this.Candestory());
       StartCoroutine(this.AnimationEffect());
        this.maxX = transform.localPosition.x + 100f; //设定移动范围
        this.minX = transform.localPosition.x - 100f;//设定移动范围
        this.transform.GetComponent<Text>().DOFade(0, tweentime);
        Destroy(this.transform.gameObject,tweentime);

    }


    public void setCoinadd(int ca)
    {
        this.tm.text = ca.ToString() + "$";
    }


    private void Update()
    {
       this.transform.Translate(Vector3.up * Time.deltaTime * 0.2f);  //移动数字 向上移动1f
       this.transform.Translate(Vector3.right * Time.deltaTime * this.direction * 0.1f);//向右边移动  1f是移动速度
        if (this.transform.localPosition.x <= this.minX) //小于minx：已经移动到了左边
        {
            this.direction = 1f;  //正数往右边移动
        }
        else if (base.transform.localPosition.x >= this.maxX)  //大于maxx： 已经移动到了右边
        {
            this.direction = -1f;  //负数左边移动
        }
    }


    private IEnumerator AnimationEffect()//金币颜色闪烁
    {
        yield return new WaitForSeconds(0.2f);
        this.textColor1.a = 1f;
        this.tm.color = this.textColor1;
        yield return new WaitForSeconds(0.2f);
        this.textColor2.a = 1f;
        this.tm.color = this.textColor2;
        base.StartCoroutine(this.AnimationEffect());
        yield break;
    }

}
