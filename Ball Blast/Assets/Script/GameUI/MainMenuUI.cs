// MainMenuUI  开始界面
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject endUI;   //结束的界面

    public GameObject spawner;   //小球生成点

    public GameObject gun1;  //初始炮1

    public GameObject gun2;  //初始炮2

    public GameObject gun3;

    public GameObject gun4;

    public Transform coin1;

    public Transform coin2;

    public Transform coin3;

    public Transform coin4;

    public Transform coins;


    public Text coinText;    //金币

    public Text Level;     //关卡

    public Slider slider;

    public Text slidertext;  //slidertext

    public GameObject NextLevelUi;  //过关ui组件

    public GameObject bu;

    public GameObject finger; //滑动手指    -714 -612

    public GameObject Gift;//礼物盒子

    public bool box;

    public Transform Canva;

    public List<int> gif = new List<int>();

    private static MainMenuUI instance;

    public int score { get; set; }   //分数

    public int coin { get; set; }  //钱

    public int level { get; set; } //等级

    public static MainMenuUI Instance
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
      
        Instance = this;
        DontDestroyOnLoad(this.transform);
        Canva = this.transform;
    }

    private void Start()
    {
 
        if (ES3.FileExists("SaveData.es3"))
        {
         gif = ES3.Load<List<int>>("gif");

        }
   
        LoadGifBox();
        StartCoroutine("Showboxtip");
        //添加奖励关卡
 
    }
    public IEnumerator Showboxtip() {
        yield return new WaitUntil(Getcoins);

        PlayerController.Instance.boxtips.SetActive(true);

        PlayerController.Instance.boxtips.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public bool Getcoins() {
        if (PlayerPrefs.GetInt("coin") > 299)
        {

            return true;
        }
        else
        {
            return false;
        }
    }




   

    private void Update()
    {
        if (GameMod.Instance.GameMods == "LevelMod")
        {
            if (this.slider.value == 1f)
            {
                if (MainUI.Instance.spawnner.activeSelf)
                {
                    MainUI.Instance.spawnner.SetActive(false); //停止生成小球
                }
                if (Game_Controller.Instance.ballbox.childCount == 0 && Game_Controller.isEnd == false && GameMod.Instance.Isplayerdie == false) //消灭了所有小球，且不是因玩家死亡而消灭的  过关条件
                {
                    Game_Controller.Instance.goToNxtLevel(); //过关
                   LoadGifBox();  //过关检测盒子

                    if (Random.Range(0, 100) > 70)
                    {
                        if (AdManager.Instance.interstitial.IsLoaded())
                        {
                            AdManager.Instance.interstitial.Show(); //展示插屏广告

                        }
                    }
                }
            }
        }

    }

    //当玩家第一次玩游戏的话
    public void IsFirstPlayGame()
    {
        if (PlayerPrefs.GetInt("Level") == 0 && PlayerPrefs.GetInt("coin") == 0)
        {
            //当前是第一次进入游戏
            gun1.GetComponent<Box>().Number = "1";
            gun1.tag = "gun1";
            gun2.GetComponent<Box>().Number = "1";
            gun2.tag = "gun1";
            gun3.GetComponent<Box>().Number = "39";
            gun3.tag = "gun39";
            gun4.GetComponent<Box>().Number = "39";
            gun4.tag = "gun39";

            PlayerPrefs.SetInt("life", 80);

            gif.Add(1);
            gif.Add(10);
            gif.Add(20);
        
        }
        else
        {
            // Debug.Log("Welcome back");
        }
    }
    private void OnEnable()
    {
        Level.text = " Level:" + (PlayerPrefs.GetInt("Level")).ToString();//读取当前关卡
        coinText.text = "X" + PlayerPrefs.GetInt("coin").ToString();
        Game_Controller.secondChance = true;
    }
    public int AddCoin() //加钱
    {
        int a = Random.Range(2, 10);
        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                PlayerprefController.AddIntValue("coin", a);  //读取coin+当前伤害  
                break;
            case "TimeMod":
                PlayerprefController.AddIntValue("coin", a);  //读取coin+当前伤害  
                break;
            case "DeathMod":
                PlayerprefController.AddIntValue("coin", (int)(a * 1.5f));  //死亡模式3倍金钱? 
                break;
            default:
                PlayerprefController.AddIntValue("coin", a);
                break;
        }
        coinText.text = "X  " + ((double)PlayerPrefs.GetInt("coin")).ToShortString();
        return a;
    }
    public void PauseButton() //暂停按钮
    {
        Game_Controller.isPaused = true;
    }
    public void OpenEndUI() //结束菜单
    {
        //this.gameObject.SetActive(false);          //关闭开始界面
        spawner.SetActive(false);                  //停止生成小球！（设定条件）
                                                   //Game_Controller.isEnd = true;            //游戏结束暂停动画F
        Game_Controller.isPaused = true;
        endUI.transform.DOScale(1f, 0.3f);   //打开ui
    }
    public void OpenSecondChanceUI() //续命F
    {
        Game_Controller.isPaused = true;      //所有运动动画物体暂停
                                              //  secondChanceUI.SetActive(true);
    }
    public void UpdateCurrentLevel(int level)  //更新关卡等级
    {
        Level.text = "Level: " + level;       //更新实时等级
    }
    public void UpdateCurrentCoin()   //更新金钱ui
    {
        coinText.text = "X" + PlayerPrefs.GetInt("coin").ToString();
    }
    public void CheckProgressBar() //添加监听
    {
        slider.value = SpawnBall.instance.Deadball / SpawnBall.instance.difficultNumber;
    }
    public void ShowNextLevelUi()
    {
        NextLevelUi.gameObject.SetActive(true);
        NextLevelUi.transform.GetChild(0).DOScaleY(1f, 0.8f);
        NextLevelUi.transform.GetChild(1).DOScaleY(1f, 1f);
        NextLevelUi.transform.DOShakeScale(0.4f);
    }
    public void SetProgressBar()  //设置进度条
    {
        this.slider.value = 0f; //进度条归零
        slider.onValueChanged.RemoveAllListeners();
        slider.gameObject.SetActive(false);
    }
    public void OpenGift() //点击盒子后
    {

        int b = PlayerPrefs.GetInt("Level");

        for (int i = 0; i < gif.Count; i++)  //如何正确的删除list列表中的某个元素
        {
            if (gif[i]==b)
            {
                gif.Remove(gif[i]);
            }
        }
        GameObject go = Instantiate(Game_Controller.Instance.giftopen, new Vector3( Gift.transform.position.x, Gift.transform.position.y+0.1f, Gift.transform.position.z), Quaternion.identity);
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.addlife);
      
        
        
        //飘字

        GameObject TextPrefab = Instantiate(BuffSystem.Instance.TextPrefab, new Vector3(Gift.transform.position.x+0.1f, Gift.transform.position.y + 0.1f, Gift.transform.position.z), Quaternion.identity);
        TextPrefab.transform.SetParent(Gift.transform.parent.transform);
        TextPrefab.transform.localScale = Vector3.one;

        TextPrefab.GetComponent<CoinNumber>().setnumber(15);

        //续15条命
        PlayerprefController.AddIntValue("life", 15);
        MainUI.Instance.UpdateLifeText();
        Gift.SetActive(false);
       
    }


    public void LoadGifBox() //载入礼物盒子
    {
        //载入条件 达到关卡 2 10 20 
        int a = PlayerPrefs.GetInt("Level");
        if (gif.Contains(a))
        {
            GameObject go = Instantiate(Game_Controller.Instance.giftshow, Gift.transform.position, Quaternion.identity);
            //显示盒子
            Gift.SetActive(true);
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ES3.Save<List<int>>("gif", gif);
        }
    }
    public void OnApplicationQuit()
    {
        ES3.Save<List<int>>("gif", gif);
    }

}
