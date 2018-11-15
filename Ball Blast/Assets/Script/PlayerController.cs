// PlayerController
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject GoldExp; //�����Ч
    private Vector3 currentPos;   //��ǰλ������

    private Vector2 mousePos;    //���������

    public Boundary boundary;  //�߿�

    public int coinadd;

    public AudioSource player;

    public GameObject playergameobject;

    public GameObject click;

    public GameObject clickposition;

    public GameObject TextPrefab;

    public GameObject line;

    public GameObject build; //����˵�

    public bool isopen = false; //����˵��Ƿ��  Ĭ�Ϲر�

    public GameObject nolife;

    public GameObject showsubs;

    public GameObject boxtips;//����Ȧ   

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

            Movement(); //�ƶ�
        }

        Boundary();  //���������ƶ���Χ

    }

    public void Movement() //�ƶ�
    {
        if (!Game_Controller.isPaused && !Game_Controller.isEnd)
        {
            if (Input.GetMouseButtonDown(0)) //һ֡
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

    public void Boundary() //���÷�Χ
    {
        this.transform.localPosition = new Vector3(Mathf.Clamp(this.transform.localPosition.x, this.boundary.xMin, this.boundary.xMax),
        this.transform.localPosition.y, this.transform.localPosition.z);   //������ֵ֮���ƶ�
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "coin")  //��Ҽ��˽��
        {   //���Ž����Ч
            player.PlayOneShot(AudioManager.Instance.Coin);

            GameObject go = Instantiate(GoldExp, col.transform.localPosition, Quaternion.identity);
            go.transform.SetParent(col.transform.parent.parent);
            go.transform.localPosition = col.transform.localPosition;
            go.transform.localScale = Vector3.one;

            switch (GameMod.Instance.GameMods)
            {
                case "LevelMod":
                    coinadd = MainMenuUI.Instance.AddCoin();  //��Ǯ
                    break;
                case "TimeMod":
                    coinadd = (int)(MainMenuUI.Instance.AddCoin() * 0.5f);  //��Ǯ
                    break;
                case "DeathMod":   //����ģʽ������Ǯ
                    coinadd = (int)(MainMenuUI.Instance.AddCoin() * 1f);
                    break;
                default:
                    break;
            }

            GameObject gameObject = Instantiate(this.TextPrefab, this.transform.localPosition, Quaternion.identity);
            gameObject.transform.SetParent(build.transform.parent.parent);//�ƶ�����������
            gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, -500f, this.transform.localPosition.z);
            gameObject.transform.localScale = Vector3.one;
            gameObject.GetComponent<CoinNumber>().setCoinadd(coinadd);

            MainMenuUI.Instance.UpdateCurrentCoin();

            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "ball" || col.gameObject.tag == "big ball") //�������
        {

            if (UnityEngine.Random.Range(1, 100) < BuffSystem.Instance.Framenumber)//���������ж�
            {
                GameMod.Instance.Isplayerdie = false; //���û��

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
                PlayerprefController.AddIntValue("life", -1); //�۳�һ������ֵ
                MainUI.Instance.UpdateLifeText();

                }
                
            
                GameMod.Instance.Isplayerdie = true;
                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Playerdie);
                GameObject go = Instantiate(Game_Controller.Instance.playerdieExp, this.transform.localPosition, Quaternion.identity); //������Ч
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

    public void OnOpenBuildMenu()  //�򿪽���˵�
    {
        if (MainUI.Instance.gameObject.activeSelf)  //�Ƿ�ʼ��Ϸ  û��ʼ��ϷΪtrue  ��ʼ��ϷΪfalse��
        {
            if (!isopen)
            {
                isopen = true;
                build.gameObject.SetActive(true); //�򿪲˵�
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
