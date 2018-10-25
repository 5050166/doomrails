using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBox : MonoBehaviour
{
    public GameObject Levelbox; //装格子的
    public GameObject[] Grid; //普通格子
    public Sprite EmptyGlodBox; //金币箱子
    public Sprite FullGlodBox; //装满金币的箱子
    public int levelnumber;
    public int unlockboxnumber;
    public int stage;
    public Text stageText;
    public int num;
    public List<int> OpenedBoxNumber = new List<int>();  //把这个存起来就ok了
    public Vector3 Grids;
    public GameObject content;
    public Text levelboxtext; //格子里面的等级文字（stage+数字） stage+i+1
    private static LevelBox instance;
    public static LevelBox Instance
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

    private void Start()  //在onenable后面执行 
    {
        //content = this.transform.GetChild(1).GetChild(0).gameObject;
        //Grids = this.transform.localPosition;
        //RefreshBoxLevelText();
    }


    public void RefreshBoxLevelText() //格子里面的文本
    {
        // int lev = DataManager.Instance.getCurrentLevel()/10;
        //if (lev== 10)
        //{
        //    stage = 0;
        //}
        for (int i = 0; i < 9; i++)
        {
            levelboxtext = content.transform.GetChild(i).GetChild(0).GetComponent<Text>();
            levelboxtext.text = (stage * 10 + (i + 1)).ToString();             //格子里的文本
        }
    }

    private void OnEnable()
    {
        content = this.transform.GetChild(1).GetChild(0).gameObject;
        Grids = this.transform.localPosition;
        // RefreshBoxLevelText();
        if (ES3.KeyExists("stage"))    /* 检测是否存在数据*/
        {
            //Debug.LogError("有这个元素");
            OpenedBoxNumber = ES3.Load<List<int>>("stage");
        }   
        CheckTheGrid();
        RefreshBoxLevelText();
    }

    public void CheckTheGrid() //载入格子
    {
        levelnumber = DataManager.Instance.getCurrentLevel(); //获取当前关卡等级 
        // levelnumber = 10;  
         // Debug.Log(9 % 10);
        stage = levelnumber / 10;

        Debug.Log(levelnumber % 10);   //9取模10=9  10取模10=0

        //格子
        if (levelnumber % 10 == 0) //解锁到第几个格子 只有整数关 和第0关 
        {
            // stage++;   //不显示0阶段  
            if (levelnumber != 0) //那就是整数关卡 10  
            {
                unlockboxnumber = 10;

                stage--;
                stageText.text = "STAGE " + stage;
            }
            else //levelnumber == 0
            {
                unlockboxnumber = 0;
                stage = 0;
                stageText.text = "STAGE " + stage;
            }
        }
        else //levelnumber % 10 !=0   1-9
        {
            unlockboxnumber = levelnumber % 10;
            stage = levelnumber / 10;
            stageText.text = "STAGE" + stage;
        }
        for (int i = 0; i < unlockboxnumber; i++)
        {
            Grid[i].transform.GetComponent<Button>().interactable = true;
            Grid[i].transform.GetChild(1).gameObject.SetActive(true);

            if (i == 9)  //一共10个格子 下表从0开始
            {
                Debug.Log(stage);

                // GetCoin();
                if (OpenedBoxNumber.Contains(stage))
                {
                    Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                    Grid[9].transform.GetChild(1).gameObject.SetActive(true);
                    Grid[9].transform.GetChild(2).gameObject.SetActive(false);
                    Grid[9].transform.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                    Grid[9].transform.GetChild(1).gameObject.SetActive(false);
                    Grid[9].transform.GetChild(2).gameObject.SetActive(true);
                    Grid[9].transform.GetComponent<Button>().interactable = true;
                }
            }
        }

    }

    public void CloseAllbox()
    {

        for (int i = 0; i < 10; i++)
        {

            Grid[i].transform.GetChild(1).gameObject.SetActive(false);

            Grid[i].transform.GetComponent<Button>().interactable = false;

            if (i == 9)
            {
                Grid[9].transform.GetChild(0).gameObject.SetActive(true);
                Grid[9].transform.GetChild(1).gameObject.SetActive(false);
                Grid[9].transform.GetChild(2).gameObject.SetActive(false);
                Grid[9].transform.GetComponent<Button>().interactable = false;
            }
        }


    }

    public void OpenAllBox()
    {
        for (int i = 0; i < 10; i++)
        {
            Grid[i].transform.GetComponent<Button>().interactable = true;
            Grid[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeStage()
    {
        this.transform.DOLocalMoveX(1100f, 0.1f).OnComplete(() => this.transform.DOLocalMove(Grids, 0f));

        stage++;
        RefreshBoxLevelText();


        int save = DataManager.Instance.getCurrentLevel() / 10;   //当等于10时   取十位数

        if (stage > 9)
        {
            stage = 0;
        }
        num = stage;

        stageText.text = "STAGE " + stage;

        if (stage > save)
        {
            CloseAllbox();
        }
        else if (stage < save)
        {
            OpenAllBox();
            Debug.Log(OpenedBoxNumber.Contains(num));
            if (OpenedBoxNumber.Contains(num))
            {
                Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                Grid[9].transform.GetChild(1).gameObject.SetActive(true);
                Grid[9].transform.GetChild(2).gameObject.SetActive(false);
                Grid[9].transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                Grid[9].transform.GetChild(1).gameObject.SetActive(false);
                Grid[9].transform.GetChild(2).gameObject.SetActive(true);
                Grid[9].transform.GetComponent<Button>().interactable = true;

            }
        }

        else if (stage == save)  //正是当前页面
        {
            Debug.Log("what");
            CloseAllbox();//先关闭所有格子
            int a = DataManager.Instance.getCurrentLevel() % 10;
            for (int i = 0; i < a; i++)
            {//打开当前页面格子

                Grid[i].transform.GetComponent<Button>().interactable = true;
                Grid[i].transform.GetChild(1).gameObject.SetActive(true);

                if (i == 9)
                {
                    if (OpenedBoxNumber.Contains(save))
                    {
                        Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                        Grid[9].transform.GetChild(1).gameObject.SetActive(true);
                        Grid[9].transform.GetChild(2).gameObject.SetActive(false);
                        Grid[9].transform.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        Grid[9].transform.GetChild(0).gameObject.SetActive(false);
                        Grid[9].transform.GetChild(1).gameObject.SetActive(false);
                        Grid[9].transform.GetChild(2).gameObject.SetActive(true);
                        Grid[9].transform.GetComponent<Button>().interactable = true;
                    }

                }
            }
        }
    }

    //金币箱子收钱,
    public void GetCoin()
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
        foreach (var item in OpenedBoxNumber)
        {
            Debug.LogError("哪几个stage的箱子开过: STAGE " + item);
        }
        GameObject go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        //根据stage来判断是那个箱子吧
        if (!OpenedBoxNumber.Contains(stage))
        {
            AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.SellWeapon);
            OpenedBoxNumber.Add(stage);
            ES3.Save<List<int>>("stage", OpenedBoxNumber);
            foreach (var item in OpenedBoxNumber)
            {
                Debug.LogError("哪几个stage的箱子开过: STAGE " + stage);
                Debug.LogError(OpenedBoxNumber.Count);
            }
            PlayerprefController.AddIntValue("coin", (num + 1) * 500);//加钱 如何记录这个箱子是否被点过 
            Grid[9].transform.GetChild(0).gameObject.SetActive(false);
            Grid[9].transform.GetChild(1).gameObject.SetActive(true);
            Grid[9].transform.GetChild(2).gameObject.SetActive(false);
            Grid[9].transform.GetComponent<Button>().interactable = false;
            MainUI.Instance.UpdateCoinText();

            GameObject coin = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
            coin.transform.SetParent(go.transform.parent.parent.parent);//设置金币的父物体
            coin.transform.position = go.transform.position;
            coin.transform.DOLocalMove(new Vector3(-522f, 735f, 0f), 0.6f).SetEase(Ease.InQuad); //设定移动版坐标
            coin.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Destroy(coin, 1f);

            GameObject coin1 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
            coin1.transform.SetParent(go.transform.parent.parent.parent);//设置金币的父物体
            coin1.transform.position = go.transform.position;
            coin1.transform.DOLocalMove(new Vector3(-522f, 735f, 0f), 0.4f).SetEase(Ease.InQuad); //设定移动版坐标
            coin1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Destroy(coin, 1f);

            GameObject coin2 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
            coin2.transform.SetParent(go.transform.parent.parent.parent);//设置金币的父物体
            coin2.transform.position = go.transform.position;
            coin2.transform.DOLocalMove(new Vector3(-522f, 735f, 0f), 0.3f).SetEase(Ease.OutQuad); //设定移动版坐标
            coin2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Destroy(coin, 1f);

            ChangeStage();
        }
    }

    private void OnApplicationPause(bool pause) //帧末尾的时候执行一次    编辑器一定不执行
    {
        if (pause)
        {
            ES3.Save<List<int>>("stage", OpenedBoxNumber); /*   存储已经解锁的箱子numer*/
        }
    }
    private void OnApplicationQuit()  //手机不一定执行
    {
        ES3.Save<List<int>>("stage", OpenedBoxNumber);
    }
}
