
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopmenu : MonoBehaviour
{
    public Text FreeCoinTime;
    //倒计时每30分钟
    public Button freecoin;
    public TimeSpan ctime;
    public int time = 0;  // 冷却时间
    public double cdtime = 0;
    public Image boximage;
    public bool cansavetime = false;
    public int num;
    public Sprite emptybox;
    public Sprite fullbox;
    public TimeSpan offtime;
    private static shopmenu instance;

    public static shopmenu Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (ES3.FileExists("SaveData.es3"))
        {
            if (ES3.KeyExists("offtime"))
            {
                offtime = GameMod.Instance.DateDiff(ES3.Load<DateTime>("offtime"), DateTime.Now); //读取离线时间？
                Debug.Log(offtime);
            }

        }
        else
        {
            offtime = TimeSpan.Zero;  //初始化
        }

        if (ES3.FileExists("SaveData.es3"))
        {
            num = ES3.Load<int>("cdtime") - (offtime.Days * 86400 + offtime.Hours * 3600 + offtime.Minutes * 60 + offtime.Seconds);
        }
        else
        {
            num = 0;
        }

        if (num <= 0)
        {
            freecoin.interactable = true;
            Setemptybox(fullbox);
        }
        else
        {
            Setemptybox(emptybox);
            freecoin.interactable = false;
            StartCoroutine(loadcd());
        }
    }

    public void Closemenu()  //关闭按钮
    {
        if (this.transform.localScale == Vector3.one)
        {
            this.transform.DOScale(0, 0.2f);
        }
        else
        {
            this.transform.DOScale(1, 0.2f);
        }

        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound); //点击音效
    }

    public void Setemptybox(Sprite sprite) {
        boximage.sprite = sprite;
        boximage.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    public void ClickFreeButton()
    {
        //获取金币
        cansavetime = true;
        PlayerprefController.AddIntValue("coin", (PlayerPrefs.GetInt("Level") + 1) * 50);
        MainMenuUI.Instance.UpdateCurrentCoin();
        GameObject go = Instantiate(Game_Controller.Instance.goldexp, MainMenuUI.Instance.coin4.position, Quaternion.identity);
        go.transform.SetParent(MainMenuUI.Instance.Canva); //移动到canva下
        go.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.SellWeapon);
        freecoin.interactable = false; //点击了之后进入冷却时间
        //记录下点击的时间
        time = 43200;
        ctime = new TimeSpan(DateTime.Now.Ticks);
        StartCoroutine(timecd());
        //切换空箱子图片
        Setemptybox(emptybox);
    }

    public IEnumerator timecd() //点击以后倒计时 
    {
        while (true)
        {
            TimeSpan now = new TimeSpan(DateTime.Now.Ticks);
            //把当前时间减去记录的点击时间
            cdtime = now.Subtract(ctime).TotalSeconds; //过了多久
            FreeCoinTime.text = DataManager.Instance.FormatTwoTimeForhour(time - (int)cdtime) ;
            if (time - (int)cdtime == 0) //
            {
                FreeCoinTime.text = "";
                break;
            }
            yield return new WaitForSeconds(1);
        }
        freecoin.interactable = true;
        Setemptybox(fullbox);
        StopCoroutine(timecd());
    }

    //进入游戏以后读取离线时间
    public IEnumerator loadcd()
    {
        while (true)
        {
            num--;
            FreeCoinTime.text = DataManager.Instance.FormatTwoTimeForhour(num) ;
            if (num <= 0)
            {
                FreeCoinTime.text = "";
                freecoin.interactable = true;
                Setemptybox(fullbox);
                break;

            }
            yield return new WaitForSeconds(1);
        }
        StopCoroutine(loadcd());
    }

    public void SaveCDtime()//储存冷却时间
    {
        ES3.Save<int>("cdtime", time - (int)cdtime);
    }

    public void OnApplicationQuit() //存储有点问题
    {
        if (cansavetime)
        {
            SaveCDtime(); //存储的是点击了的时间
        }
        else
        {
            //存储没点击 剩余的时间
            ES3.Save<int>("cdtime", num);
        }



    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {

            if (cansavetime)
            {
                SaveCDtime(); //存储的是点击了的时间
            }
            else
            {
                //存储没点击 剩余的时间
                ES3.Save<int>("cdtime", num);
            }

        }
    }


}
