// MainMenuUI  开始界面
using DG.Tweening;
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

   // public GameObject secondChanceUI;  //再续一秒你

    public Text coinText;    //金币

    public Text Level;     //关卡

    public Slider slider;

    public Text slidertext;  //slidertext

    public GameObject NextLevelUi;  //过关ui组件

    public GameObject bu;

    public GameObject finger; //滑动手指    -714 -612

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
                    if (AdManager.Instance.rewardBasedVideo.IsLoaded())
                    {
                        AdManager.Instance.rewardBasedVideo.Show(); //展示视屏广告
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
                PlayerprefController.AddIntValue("coin", a * 3);  //死亡模式3倍金钱? 
                break;
            default:
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
}
