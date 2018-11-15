
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class Game_Controller : MonoBehaviour
{
    public GameObject stoneExp; //石头被攻击特效

    public GameObject stonedie; //石头销毁特效

    public GameObject playerdieExp; //玩家死亡特效

    public GameObject Smoke;       //烟花特效

    public GameObject Rocket;    //火箭弹爆炸特效

    public GameObject watermelon; //西瓜爆炸特效

    public GameObject smoke;  //火焰特效

    public GameObject icecream; //冰淇凌特效

    public GameObject goldexp;//金币特效

    public GameObject BigRocket;

    public GameObject Homemade;

    public GameObject purple;

    public GameObject watergun;//水滴炮

    public GameObject medic; //医疗针

    public GameObject cheese; //奶酪

    public GameObject tomato;//西红柿

    public GameObject electromagnetic; //电磁

    public GameObject giftopen; //打开礼物的特效

    public GameObject giftshow;//显示礼物的特效

    public static int damage = 1;

    public static bool isPaused;

    public static bool isEnd;   //游戏是否结束

    public static bool secondChance;   //续命

    public static int layer;

    public DateTime time;

    public DateTime leavetime;                           //默认值为：1/1/0001 12:00:00 AM

    public TimeSpan ts;                                //离线多少秒

    GameObject go;

    public List<Box> bo = new List<Box>();//用来存储所有的box

    private static Game_Controller instance;

    private string PREFS_CURRENT_BOX = "BOX";

    public static Game_Controller Instance
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

    public int currentLevel;

    public bool Isplaying = false; //没有在游戏中

    public bool Cango = false;  //是否已经生成完了当前关卡所有的小球

    public Transform ballbox;

    public Camera main;

    public Image tips;

    public void Awake()
    {
       

        AddBox();
        //   LoadGameTime();
        instance = this;
    }
    public void AddBox()//获取所有盒子
    {

        go = GameObject.Find("player/upmenu").gameObject;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            bo.Add(go.transform.GetChild(i).GetComponent<Box>());
            //  Debug.Log(i);
        }

    }

    private void Start()
    {
        GameMod.Instance.Tips.transform.DOScale(2f, 0.8f).SetLoops(-1, LoopType.Yoyo); //提示圈

        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("ijH88VeTW7q29u9Utddqo6");  //key
            /* For detailed logging */
            /* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
   /* Mandatory - set your apple app ID
      NOTE: You should enter the number only and not the "ID" prefix */
   AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
   AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
            /* Mandatory - set your Android package name */
            AppsFlyer.setAppID("com.polyhouse.doomrails");  //bundle id
            /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
            AppsFlyer.init("ijH88VeTW7q29u9Utddqo6", "AppsFlyerTrackerCallbacks");  //app id
#endif


        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }

        currentLevel = DataManager.Instance.getCurrentLevel();  //读取当前关卡
                                                                // Debug.Log(currentLevel);
        MainMenuUI.Instance.UpdateCurrentLevel(currentLevel);
        Isplaying = true;
        // MainMenuUI.Instance.UpdateFillBar();
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            //logOpenGameEvent(1);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void ChangeCameraPosition()
    {
        main = Camera.main;

    }

    //  达到过关条件以后执行的方法
    public void goToNxtLevel()   //下一关
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Smoke); //播放过关音效
        GameObject go1 = Instantiate(Smoke, this.transform.position, Quaternion.identity);
        go1.transform.SetParent(this.transform);
        go1.transform.localPosition = new Vector3(UnityEngine.Random.Range(-400f, 400f), UnityEngine.Random.Range(50f, 850f));
        go1.transform.localScale = Vector3.one;

        GameObject go2 = Instantiate(Smoke, this.transform.position, Quaternion.identity);
        go2.transform.SetParent(this.transform);
        go2.transform.localPosition = new Vector3(UnityEngine.Random.Range(-400f, 400f), UnityEngine.Random.Range(50f, 850f));
        go2.transform.localScale = Vector3.one;


        GameObject go3 = Instantiate(Smoke, this.transform.position, Quaternion.identity);
        go3.transform.SetParent(this.transform);
        go3.transform.localPosition = new Vector3(UnityEngine.Random.Range(-400f, 400f), UnityEngine.Random.Range(50f,850f));
        go3.transform.localScale = Vector3.one;

        GameObject go4 = Instantiate(Smoke, this.transform.position, Quaternion.identity);
        go4.transform.SetParent(this.transform);
        go4.transform.localPosition = new Vector3(UnityEngine.Random.Range(-400f, 400f), UnityEngine.Random.Range(50f, 850f));
        go4.transform.localScale = Vector3.one;


        GameObject go5 = Instantiate(Smoke, this.transform.position, Quaternion.identity);
        go5.transform.SetParent(this.transform);
        go5.transform.localPosition = new Vector3(UnityEngine.Random.Range(-400f, 400f), UnityEngine.Random.Range(50f, 850f));
        go5.transform.localScale = Vector3.one;

        MainUI.Instance.spawnner.SetActive(false);
        MainUI.Instance.buyui.SetActive(true);
        currentLevel++;           //关卡++
        DataManager.Instance.setCurrentLevel(currentLevel);   //存储当前关卡数
        MainUI.Instance.levelbox.gameObject.SetActive(true);
        LevelBox.Instance.CheckTheGrid();
        MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.3f);
        MainUI.Instance.weaponitem.DOScaleY(1, 0f);                //显示武器菜单
        MainUI.Instance.sold.transform.DOScaleY(1, 0.3f);
        MainUI.Instance.upmenu.SetActive(true);
        MainMenuUI.Instance.spawner.SetActive(false);  //停止生成小球！（设定条件）
        MainMenuUI.Instance.SetProgressBar(); //移除监听
        SpawnBall.instance.Deadball = 0;
        MainMenuUI.Instance.slider.value = 0;
        MainMenuUI.Instance.Level.text = " Level:" + (PlayerPrefs.GetInt("Level")).ToString();//读取当前关卡
        go.transform.parent.transform.localPosition = new Vector3(0, -591f, 0); //设定玩家位置
        go.transform.parent.transform.GetComponent<BoxCollider2D>().offset = new Vector2(-50f, -70);
        GameMod.Instance.Homebutton.transform.DOScale(1, 0.2f);
        isEnd = true;                                 //游戏结束暂停动画
                                     //游戏暂停
        Isplaying = false;

    }

    public static void SetGameStats()
    {
        damage = PlayerPrefs.GetInt("damage");
    }


    public void OnApplicationQuit()
    {
        //    SetSaveBox(bo);  //存储小球信息
        //  SaveTime();//退出的时候存储一下退出时间

    }


    public void OnGameQuit()
    {
        //SetSaveBox(bo);

        Application.Quit();
    }





}
