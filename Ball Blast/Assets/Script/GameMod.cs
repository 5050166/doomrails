using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMod : MonoBehaviour
{
    public string GameMods;
    public GameObject LevelMod;  //游戏mod
    public Button DeathModButton;//死亡模式按钮
    public GameObject showlevel;
    public GameObject Homebutton;
    public GameObject AdButton;
    public GameObject Tips;    //购物车提示圈
    public GameObject DeathModtip;
    public Text CurrentLevel;    //通过等级
    public Text CurrentTime;
    public Text ColdTime; //冷却时间
    public Text Ready;
    public Text go;
    public TimeSpan offlinetimes;//离线时间
    public int number;
    public bool Isplayerdie;
    public float time;
    public bool can = false;
    public Text timetext;
    public TimeSpan ClickTime;     //记录下来点击按钮的time
    public Text AdButtonText;

    public AudioSource BGM;  //主场景bgm 
    public AudioSource GameModBGM;

    private static GameMod instance;
    public static GameMod Instance
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
        GameModBGM = this.transform.GetComponent<AudioSource>();
        GameModBGM.loop = true;
        GameModBGM.volume = 1.0f;

        Isplayerdie = false;
        CurrentLevel.text = "Max  LV." + PlayerPrefs.GetInt("Level");
        CurrentTime.text = "Max Time:" + (Mathf.Round(PlayerPrefs.GetFloat("HighTime"))).ToString() + "S";
        if (ES3.FileExists("SaveData.es3"))
        {
        offlinetimes = DateDiff(ES3.Load<DateTime>("time"), DateTime.Now);
        }
        AdButtonText = AdButton.transform.GetChild(0).GetComponent<Text>();
        AdButtonText.text = "+" + (DataManager.Instance.getCurrentLevel() * 20 + 100);

        //   if (offlinetimes.Days > 0 || offlinetimes.Hours > 0 || offlinetimes.Minutes > 10)
        //    {
        if (ES3.FileExists("SaveData.es3"))
        {
        number = ES3.Load<int>("DeathModTime") - (offlinetimes.Minutes * 60 + offlinetimes.Seconds);
        }


        if (number < 0)
        {
            ColdTime.text = "coin x 3";
            DeathModButton.interactable = true;
        }
        else //离线时间不足10分钟
        {

            DeathModButton.interactable = false;
            StartCoroutine(ColdTimes());

        }
        DeathModButton = DeathModButton.transform.GetComponent<Button>();
    }


    public IEnumerator ColdTimes()
    {
        while (true)
        {
            number--;
            ColdTime.text = DataManager.Instance.FormatTwoTime(number);
            if (number <= 0)
            {
                ColdTime.text = "coin x 3";
                DeathModButton.interactable = true;

                break;
            }
            yield return new WaitForSeconds(1);
        }
        ColdTime.text = "coin x 3";
        StopCoroutine(ColdTimes());
    }
    private TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)  //比较两个时间的差值
    {
        //string dateDiff = null;
        TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
        TimeSpan ts2 = new
        TimeSpan(DateTime2.Ticks);
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        // dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
        //return dateDiff;
        return ts;
    }

    public void CardPlayButton() //点击cardplay按钮  关卡模式
    {
        GameMods = "LevelMod";
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
        this.transform.DOScale(0f, 0.2f);
        LevelMod.gameObject.SetActive(true);
        showlevel.gameObject.SetActive(true);
        Homebutton.gameObject.transform.DOScale(1, 0.2f);


    }
    public void TimePlayMod()   //计时模式
    {
        BGM.Pause(); //暂停主界面bgm
        GameModBGM.clip = AudioManager.Instance.TimeModBGM;
        GameModBGM.Play();
        StartCoroutine(wait());
        StartCoroutine(tips());
        GameMods = "TimeMod";
        DeathModtip.transform.GetChild(0).GetComponent<Text>().text = "survive as long as possible ";
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
        timetext.gameObject.SetActive(true);
        this.transform.DOScale(0f, 0.2f);
        this.AdButton.transform.DOScale(0, 0.2f);
        MainUI.Instance.StartGame();
        MainMenuUI.Instance.slider.gameObject.SetActive(false);
        Background.Instance.Move();  //移动游戏背景
        can = true;// 开始计时

    }

    public void DeathMod() //死亡模式
    {
        BGM.Pause(); //暂停主界面bgm
        GameModBGM.clip = AudioManager.Instance.TimeModBGM;
        GameModBGM.Play();
        StartCoroutine(wait());
        StartCoroutine(tips());
        this.DeathModButton.interactable = false;//关闭交互 并开始倒计时3分钟
        GameMods = "DeathMod";
        DeathModtip.transform.GetChild(0).GetComponent<Text>().text = "Triple income mod";
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
        this.transform.DOScale(0f, 0.2f);
        this.AdButton.transform.DOScale(0, 0.2f);
        MainUI.Instance.StartGame();
        MainMenuUI.Instance.slider.gameObject.SetActive(false);
        Background.Instance.Move();

    }

    public void Gohome()
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
        LevelMod.gameObject.SetActive(false);
        showlevel.gameObject.SetActive(false);
        this.transform.DOScale(1f, 0.2f);
        this.AdButton.transform.DOScale(1, 0.2f);
        Homebutton.gameObject.transform.DOScale(0, 0.2f);
        this.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(0f, 0f);
    }

    private void Update()
    {
        if (can)
        {
            time += Time.deltaTime;
            timetext.transform.GetChild(0).GetComponent<Text>().text = ((Mathf.Round(time * 1000)) / 1000).ToString();
        }
    }
    IEnumerator tips()
    {
        //switch (GameMods)
        //{ case "TimeMod":
        //        DeathModtip.transform.GetChild(0).GetComponent<Text>().text = "survive as long as possible ";
        //        break;
        //    case "DeathMod":
        //        DeathModtip.transform.GetChild(0).GetComponent<Text>().text = "Triple the money gain";
        //        break;

        //    default:
        //        break;
        //}
        DeathModtip.transform.DOScaleY(1, 0.2f);
        yield return new WaitForSeconds(2f);
        DeathModtip.transform.DOScaleY(0, 0.2f);
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.GameStart);
        Ready.transform.DOScale(1, 0.25f).OnComplete(OnComplete);
    }

    public void OnComplete()
    {
        Ready.transform.DOScale(0, 0.25f).OnComplete(Ongo);

    }
    public void Ongo()
    {
        go.transform.DOScale(1, 0.25f).OnComplete(Ongo2);
    }
    public void Ongo2()
    {
        go.transform.DOScale(0, 0.25f);
    }




}
