
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class Game_Controller : MonoBehaviour
{
    public GameObject stoneExp; //ʯͷ��������Ч

    public GameObject stonedie; //ʯͷ������Ч

    public GameObject playerdieExp; //���������Ч

    public GameObject Smoke;       //�̻���Ч

    public GameObject Rocket;    //�������ը��Ч

    public GameObject watermelon; //���ϱ�ը��Ч

    public GameObject smoke;  //������Ч

    public GameObject icecream; //�������Ч

    public GameObject goldexp;//�����Ч

    public GameObject BigRocket;

    public GameObject Homemade;

    public GameObject purple;

    public GameObject watergun;//ˮ����

    public GameObject medic; //ҽ����

    public GameObject cheese; //����

    public GameObject tomato;//������

    public GameObject electromagnetic; //���

    public GameObject giftopen; //���������Ч

    public GameObject giftshow;//��ʾ�������Ч

    public static int damage = 1;

    public static bool isPaused;

    public static bool isEnd;   //��Ϸ�Ƿ����

    public static bool secondChance;   //����

    public static int layer;

    public DateTime time;

    public DateTime leavetime;                           //Ĭ��ֵΪ��1/1/0001 12:00:00 AM

    public TimeSpan ts;                                //���߶�����

    GameObject go;

    public List<Box> bo = new List<Box>();//�����洢���е�box

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

    public bool Isplaying = false; //û������Ϸ��

    public bool Cango = false;  //�Ƿ��Ѿ��������˵�ǰ�ؿ����е�С��

    public Transform ballbox;

    public Camera main;

    public Image tips;

    public void Awake()
    {
       

        AddBox();
        //   LoadGameTime();
        instance = this;
    }
    public void AddBox()//��ȡ���к���
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
        GameMod.Instance.Tips.transform.DOScale(2f, 0.8f).SetLoops(-1, LoopType.Yoyo); //��ʾȦ

        /* Mandatory - set your AppsFlyer��s Developer key. */
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

        currentLevel = DataManager.Instance.getCurrentLevel();  //��ȡ��ǰ�ؿ�
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

    //  �ﵽ���������Ժ�ִ�еķ���
    public void goToNxtLevel()   //��һ��
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Smoke); //���Ź�����Ч
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
        currentLevel++;           //�ؿ�++
        DataManager.Instance.setCurrentLevel(currentLevel);   //�洢��ǰ�ؿ���
        MainUI.Instance.levelbox.gameObject.SetActive(true);
        LevelBox.Instance.CheckTheGrid();
        MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.3f);
        MainUI.Instance.weaponitem.DOScaleY(1, 0f);                //��ʾ�����˵�
        MainUI.Instance.sold.transform.DOScaleY(1, 0.3f);
        MainUI.Instance.upmenu.SetActive(true);
        MainMenuUI.Instance.spawner.SetActive(false);  //ֹͣ����С�򣡣��趨������
        MainMenuUI.Instance.SetProgressBar(); //�Ƴ�����
        SpawnBall.instance.Deadball = 0;
        MainMenuUI.Instance.slider.value = 0;
        MainMenuUI.Instance.Level.text = " Level:" + (PlayerPrefs.GetInt("Level")).ToString();//��ȡ��ǰ�ؿ�
        go.transform.parent.transform.localPosition = new Vector3(0, -591f, 0); //�趨���λ��
        go.transform.parent.transform.GetComponent<BoxCollider2D>().offset = new Vector2(-50f, -70);
        GameMod.Instance.Homebutton.transform.DOScale(1, 0.2f);
        isEnd = true;                                 //��Ϸ������ͣ����
                                     //��Ϸ��ͣ
        Isplaying = false;

    }

    public static void SetGameStats()
    {
        damage = PlayerPrefs.GetInt("damage");
    }


    public void OnApplicationQuit()
    {
        //    SetSaveBox(bo);  //�洢С����Ϣ
        //  SaveTime();//�˳���ʱ��洢һ���˳�ʱ��

    }


    public void OnGameQuit()
    {
        //SetSaveBox(bo);

        Application.Quit();
    }





}
