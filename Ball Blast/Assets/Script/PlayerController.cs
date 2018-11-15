// PlayerController
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject GoldExp; //金币特效
    private Vector3 currentPos;   //当前位置坐标

    private Vector2 mousePos;    //鼠标点击坐标

    public Boundary boundary;  //边框

    public int coinadd;

    public AudioSource player;

    public GameObject playergameobject;

    public GameObject click;

    public GameObject clickposition;

    public GameObject TextPrefab;

    public GameObject line;

    public GameObject build; //建造菜单

    public bool isopen = false; //建造菜单是否打开  默认关闭

    public GameObject nolife;

    public GameObject showsubs;

    public GameObject boxtips;//广告光圈   

    private static PlayerController instance;

    public static PlayerController Instance
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

        Game_Controller.isEnd = true;

        Debug.Log(Game_Controller.isEnd);
        Debug.Log(Game_Controller.isPaused);
        Debug.Log(PlayerPrefs.GetString("firstplay"));


        player = this.transform.GetComponent<AudioSource>();

        StartCoroutine(checkline());
    }

    public bool canwshowline()
    {

        if (PlayerPrefs.GetInt("Level") == 1 && PlayerPrefs.GetInt("first") == 0 && WeaponShowitem.Instance.Content.transform.GetChild(0).GetChild(0).name != "Image")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator checkline()
    {
        yield return new WaitUntil(canwshowline);
        line.gameObject.SetActive(true);
        Destroy(line.gameObject, 4f);
        PlayerPrefs.SetInt("first", 1);
    }

    public void close()
    {

        Destroy(click);

    }

    private void Update()
    {
        if (!Game_Controller.isPaused && !Game_Controller.isEnd)
        {

            Movement(); //移动
        }

        Boundary();  //设置左右移动范围

    }

    public void Movement() //移动
    {
        if (!Game_Controller.isPaused && !Game_Controller.isEnd)
        {
            if (Input.GetMouseButtonDown(0)) //一帧
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out mousePos);

                this.currentPos = this.transform.localPosition;

            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 a;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out a);

                Vector3 b = new Vector3(a.x, a.y, 0) - new Vector3(this.mousePos.x, this.mousePos.y, 0);
                b.z = 0f;
                Vector3 vector = this.currentPos + b;
                this.transform.localPosition = new Vector3(vector.x, this.transform.localPosition.y, this.transform.localPosition.z);

            }

        }
    }

    public void Boundary() //设置范围
    {
        this.transform.localPosition = new Vector3(Mathf.Clamp(this.transform.localPosition.x, this.boundary.xMin, this.boundary.xMax),
        this.transform.localPosition.y, this.transform.localPosition.z);   //在两个值之间移动
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "coin")  //玩家捡到了金币
        {   //播放金币音效
            player.PlayOneShot(AudioManager.Instance.Coin);

            GameObject go = Instantiate(GoldExp, col.transform.localPosition, Quaternion.identity);
            go.transform.SetParent(col.transform.parent.parent);
            go.transform.localPosition = col.transform.localPosition;
            go.transform.localScale = Vector3.one;

            switch (GameMod.Instance.GameMods)
            {
                case "LevelMod":
                    coinadd = MainMenuUI.Instance.AddCoin();  //加钱
                    break;
                case "TimeMod":
                    coinadd = (int)(MainMenuUI.Instance.AddCoin() * 0.5f);  //加钱
                    break;
                case "DeathMod":   //死亡模式三倍加钱
                    coinadd = (int)(MainMenuUI.Instance.AddCoin() * 1f);
                    break;
                default:
                    break;
            }

            GameObject gameObject = Instantiate(this.TextPrefab, this.transform.localPosition, Quaternion.identity);
            gameObject.transform.SetParent(build.transform.parent.parent);//移动到画布下面
            gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, -500f, this.transform.localPosition.z);
            gameObject.transform.localScale = Vector3.one;
            gameObject.GetComponent<CoinNumber>().setCoinadd(coinadd);

            MainMenuUI.Instance.UpdateCurrentCoin();

            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "ball" || col.gameObject.tag == "big ball") //玩家死亡
        {

            if (UnityEngine.Random.Range(1, 100) < BuffSystem.Instance.Framenumber)//进行死亡判定
            {
                GameMod.Instance.Isplayerdie = false; //玩家没死

                GameObject go = Instantiate(Prefabmanager.Instance.miss, this.transform.localPosition, Quaternion.identity);
                go.transform.localScale = Vector3.zero;
                Destroy(go, 1f);
                go.transform.SetParent(this.transform);
                go.transform.localPosition = this.transform.GetChild(0).transform.localPosition;
                go.transform.DOScale(1f, 0.5f);
            }
            else
            {
                if (PlayerPrefs.GetInt("removead")==0)
                {
                PlayerprefController.AddIntValue("life", -1); //扣除一点生命值
                MainUI.Instance.UpdateLifeText();

                }
                
            
                GameMod.Instance.Isplayerdie = true;
                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Playerdie);
                GameObject go = Instantiate(Game_Controller.Instance.playerdieExp, this.transform.localPosition, Quaternion.identity); //创建特效
                go.transform.SetParent(this.transform.parent);
                go.transform.localPosition = this.transform.localPosition;

                go.transform.localScale = this.transform.localScale * 0.7f;
                for (int i = 0; i < DataManager.Instance.bo.Count; i++)
                {
                    DataManager.Instance.bo[i].gameObject.AddComponent<Rigidbody2D>();
                    DataManager.Instance.bo[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
                GameMod.Instance.can = false;

                MainMenuUI.Instance.OpenEndUI();
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.GameFail);


            }
        }

    }

    public void OnOpenBuildMenu()  //打开建造菜单
    {
        if (MainUI.Instance.gameObject.activeSelf)  //是否开始游戏  没开始游戏为true  开始游戏为false；
        {
            if (!isopen)
            {
                isopen = true;
                build.gameObject.SetActive(true); //打开菜单
            }
            else
            {
                isopen = false;
                build.gameObject.SetActive(false);
            }
        }
    }

    public void Noenoughlife()
    {
        nolife.transform.DOScale(0, 0.2f);
    }

    public void showsub()
    {
        Noenoughlife();
        showsubs.transform.DOScale(1, 0.2f);
    }




}
