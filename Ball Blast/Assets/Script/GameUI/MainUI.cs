// MainUI
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainUI : MonoBehaviour
{
    public GameObject spawnner;//生成点

    public GameObject sold;

    private static MainUI instance;
    public Text coinText;
    public Text lifeText;//生命值

    public Text message;
    public bool isunlock = false;
    public GameObject shakeloop;
    public GameObject buymenu;  //弹出来的消息框 messagebox
    public GameObject Mainui;
    public GameObject tool;
    public GameObject upmenu;
    public GameObject go;
    public GameObject box;
    public AudioSource audioSource;
    public static MainUI Instance
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
    public Transform weaponitem;
    public GameObject buyui;
    public float tweentime;
    public Transform levelbox;


    private void Start()
    {
        //  MainMenuUI.Instance.IsHidenOrShow();
        shakeloop = this.transform.GetChild(0).gameObject;

        UpdateCoinText(); //载入更新金币
                          //载入生命值信息

        audioSource = this.transform.GetComponent<AudioSource>();
        //载入横幅广告
        shakeloop.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.7f).SetLoops(-1, LoopType.Yoyo);
    }
    public void OnComplete(string name)
    {
        switch (name)
        {
            case "closetool":
                tool.SetActive(false);
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
            case "Opentool":
                tool.SetActive(true);
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
        }
    }

    IEnumerator WaitSomeTimeToFinishTween(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime);
        OnComplete(name);
    }

    private void OnDisable()
    {
        //隐藏横幅广告
    }
    private void Awake()
    {

        UiFade();

        Game_Controller.isPaused = true;   //进游戏不是直接开始
        instance = this;
    }

    public void UpdateLifeText() //更新生命值的文本
    {
        lifeText.text = "X " + PlayerPrefs.GetInt("life").ToString();
    }



    public void UpdateCoinText() //金币
    {
        coinText.text = "X " + PlayerPrefs.GetInt("coin").ToString();
    }
    public void PauseMod() //暂停创造模式
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);

        if (this.gameObject.transform.localScale.y > 0) //检测开始ui的开关状态  ui开关开着代表一定是在游戏的等待开始界面！
        {
            upmenu.SetActive(true);  //打开建筑方格子

            if (tool.activeSelf)  //如果是开着的   
            {
                WeponShop.Instance.WeaponPrice.transform.parent.gameObject.SetActive(false);
                tool.transform.DOScale(0, tweentime);  //关闭建筑菜单
                StartCoroutine(WaitSomeTimeToFinishTween("closetool", tweentime)); //调用协程等待设定的动画时间以后，在调用取消协程 
            }
            else  //如果是关着的
            {
                tool.SetActive(true);
                tool.transform.DOScale(1, tweentime);

                StartCoroutine(WaitSomeTimeToFinishTween("Opentool", tweentime));
            }
            spawnner.gameObject.SetActive(false);//停止出兵
            //Game_Controller.isPaused = true;
            //Game_Controller.isEnd = true;  //暂停移动和开火
        }
        else
        {
            upmenu.SetActive(true);
            //游戏没有结束，但是你可以点击到它 是在过关的时候
            if (tool.activeSelf)  //如果是开着的   
            {
                tool.transform.DOScale(0, tweentime);  //关闭建筑菜单
                StartCoroutine(WaitSomeTimeToFinishTween("closetool", tweentime)); //调用协程等待设定的动画时间以后，在调用取消协程 
            }
            else  //如果是关着的
            {
                Debug.LogError("1");
                tool.SetActive(true);
                tool.transform.DOScale(1, tweentime);

                StartCoroutine(WaitSomeTimeToFinishTween("Opentool", tweentime));
            }
        }
    }


    public void StartGame()//开始按钮 也应该按照游戏模式来区分
    {
        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                //关卡模式
                if (PlayerPrefs.GetInt("life") > 0)
                {
                    buyui.SetActive(false);
                    GameMod.Instance.AdButton.transform.DOScale(0, 0.2f);
                    StartCoroutine(GameMod.Instance.wait());
                    GameMod.Instance.Homebutton.transform.DOScale(0, 0.2f);
                    levelbox.gameObject.SetActive(false);
                    Game_Controller.isEnd = false;
                    //开始按钮
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                    BuffSystem.Instance.Setbuff();
                    BuffSystem.Instance.RefreshAllBox();
                    BuffSystem.Instance.restnumber();
                    Game_Controller.isPaused = false;  //可以移动开火
                    sold.transform.DOScaleY(0, 0.3f);                                      //隐藏出售按钮
                    weaponitem.DOScaleY(0, 0f);                                         //隐藏武器背包
                    this.transform.DOScaleY(0f, 0.3f);                                   //引导并没关闭而是调整了scal值
                    upmenu.SetActive(false);
                    MainMenuUI.Instance.slider.gameObject.SetActive(true);             //显示进度条
                    MainMenuUI.Instance.slidertext.text = "LV. " + PlayerPrefs.GetInt("Level");
                    spawnner.SetActive(true); //开始生成敌人
                    SpawnBall.instance.currentlevelball = 0;
                    GameMod.Instance.Isplayerdie = false;
                    MainMenuUI.Instance.slider.onValueChanged.AddListener(delegate   //添加监听
                    {
                        MainMenuUI.Instance.CheckProgressBar();
                    });
                    Background.Instance.Move();


                    if (MainMenuUI.Instance.Gift.activeSelf) //隐藏盒子
                    {
                        PlayerPrefs.SetInt("gif", 1);
                        MainMenuUI.Instance.Gift.SetActive(false);//如果礼物盒子还是开着的就隐藏盒子

                    }
                }
                else
                {
                    GameMod.Instance.nolife.transform.DOScale(1, 0.2f);
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.BuildFail);
                }

                break;

            case "TimeMod":
                //时间模式


                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                BuffSystem.Instance.Setbuff();
                BuffSystem.Instance.RefreshAllBox();
                BuffSystem.Instance.restnumber();
                Game_Controller.isPaused = false;  //可以移动开火

                buyui.SetActive(false);
                sold.transform.DOScaleY(0, 0.3f);                                      //隐藏出售按钮
                weaponitem.DOScaleY(0, 0f);                                         //隐藏武器背包
                this.transform.DOScaleY(0f, 0.3f);                                   //引导并没关闭而是调整了scal值
                upmenu.SetActive(false);
                MainMenuUI.Instance.SetProgressBar();
                spawnner.SetActive(true); //开始生成敌人

                break;
            case "DeathMod":
                //死亡模式

                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                BuffSystem.Instance.Setbuff();
                BuffSystem.Instance.RefreshAllBox();
                BuffSystem.Instance.restnumber();
                Game_Controller.isPaused = false;                                          //可以移动开火

                buyui.SetActive(false);
                sold.transform.DOScaleY(0, 0.3f);                                      //隐藏出售按钮
                weaponitem.DOScaleY(0, 0f);                                         //隐藏武器背包
                this.transform.DOScaleY(0f, 0.3f);                                   //引导并没关闭而是调整了scal值
                upmenu.SetActive(false);
                spawnner.SetActive(true);                                                 //开始生成敌人

                break;
            default:
                break;
        }
        //  BuffSystem.Instance.Setbuff();
        //if (Background.Instance.IsFinish)
        //{
        //    Background.Instance.Move();//开始移动背景
        //}

    }

    public void Yesbutton()
    {
        if (PlayerPrefs.GetInt("coin") > box.GetComponent<Box>().Price)
        {
            audioSource.PlayOneShot(AudioManager.Instance.Click); //click 
            int num = PlayerPrefs.GetInt("coin") - box.GetComponent<Box>().Price;
            Debug.Log(num);
            PlayerPrefs.SetInt("coin", num);
            UpdateCoinText();

            isunlock = true;

            if (this.gameObject.activeSelf)
            {
                StartCoroutine(checkLock()); //检查格子的状态
            }
            buymenu.SetActive(false);
        }
        else
        {
            //提示金钱不足
            audioSource.PlayOneShot(AudioManager.Instance.BuildFail);
            message.text = "You Have No Enough Money....";
        }

    }
    bool Isunlock()
    {
        return isunlock;
    }


    public void Nobuttom()
    {
        isunlock = false;
        audioSource.PlayOneShot(AudioManager.Instance.Click);
        buymenu.transform.DOScale(0, 0.1f);
        buymenu.transform.DOScale(0, 0.1f).OnComplete(() => buymenu.SetActive(false));

    }

    public void Openbuymenu()
    {
        audioSource.PlayOneShot(AudioManager.Instance.Click); //click
        if (buymenu.activeSelf)
        {
            buymenu.transform.DOScale(0, 0.1f).OnComplete(() => buymenu.SetActive(false));

        }
        else
        {
            buymenu.SetActive(true);
            buymenu.transform.DOScale(1, 0.1f);
            go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //获取当前ui下点击的
            box = go.transform.parent.gameObject;
            message.text = "Cost " + box.GetComponent<Box>().Price + " Unlock New Parts";
        }
    }
    IEnumerator checkLock()
    {
        yield return new WaitUntil(Isunlock); //直到某个数值为XX
        audioSource.PlayOneShot(AudioManager.Instance.BuyWeapon);
        go.SetActive(false);
        box.GetComponent<Box>().Number = "39"; //解锁了方格 
        box.gameObject.tag = "gun39"; //解锁了
    }

    public void UiFade()
    {
        this.transform.GetChild(0).GetComponent<Text>().DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

}
